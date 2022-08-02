using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Puzzle;
using Unboxed.Scriptable;
using UnityEngine;

namespace Unboxed.Player
{
    public enum PlayerState
    {
        Idle,
        Click,
        Hold,
        //ColorPicking,
        Release
    }

    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject _dotPlayerPref;
        [SerializeField] private LinePlayer _linePlayer;

        internal PlayerState State => _state;
        internal GemsColor CurrentGemsColor => _curretGemsColor;
        internal GemsColor KeyGemsColor => _keyGemsColor;
        internal bool IsShowColorPicker => _isShowColorPicker;
        internal DrawController DrawController => _drawController;
        internal PuzzleController PuzzleController => _puzzleController;
        internal PlayerController PlayerController => _playerController;

        internal AbstactDrawController AbstactDrawController => _abstactDrawController;
        internal AbstactPuzzleController AbstactPuzzleController => _abstactPuzzleController;

        internal event OnClick OnClickEvent;
        internal event OnHold OnHoldEvent;
        internal event OnRelease OnReleaseEvent;
        internal event OnRestart OnRestartEvent;
        //internal event OnExit OnExitEvent;

        internal delegate void OnClick(GameObject gameObject);
        internal delegate void OnHold(GameObject gameObject);
        internal delegate void OnRelease();
        internal delegate void OnRestart();
        //internal delegate void OnExit();

        private DrawController _drawController;
        private PuzzleController _puzzleController;
        private PlayerController _playerController;

        private AbstactPuzzleController _abstactPuzzleController;
        private AbstactDrawController _abstactDrawController;

        private GemsColor _curretGemsColor;
        private GemsColor _keyGemsColor;
        private PlayerState _state;
        private bool _isShowColorPicker;

        private void Awake()
        {
            _drawController = GetComponent<DrawController>();
            _puzzleController = GetComponent<PuzzleController>();
            _playerController = GetComponent<PlayerController>();
        }

        private void Start()
        {
            _playerController.BindPlayer(this);
            _playerController.InitPlayerController();
        }

        private void Update()
        {
            CheckPlayerState();
        }

        public void SetCurrentGemsColor(GemsColor gemsColor)
        {
            _curretGemsColor = gemsColor;
        }

        public void SetKeyGemsColor(GemsColor gemsColor)
        {
            _keyGemsColor = gemsColor;
        }

        public void SetPlayerState(PlayerState state)
        {
            _state = state;
        }

        public void SetShowColorPicker(bool isShow)
        {
            _isShowColorPicker = isShow;
        }

        public void InitPlayer(PuzzleScriptable puzzle)
        {
            //TODO: For debug
            //_puzzleData = LevelManager.Instance.puzzle;

            AddPuzzleController(GemsColor.Empty);
            _abstactPuzzleController.BindPlayer(this, _dotPlayerPref);
            _abstactPuzzleController.InitPuzzleController(puzzle.colors);

            AddDrawController(GemsColor.Empty);
            _abstactDrawController.BindPlayer(this, _linePlayer);
            _abstactDrawController.InitDrawController(puzzle.colors);
        }

        public void UpdatePlayer(PuzzleScriptable puzzle)
        {
            _abstactPuzzleController.UpdatePuzzleController(puzzle.colors);
            _abstactDrawController.UpdateDrawController(puzzle.colors);
            _playerController.EnebleInput();
        }

        public void Restart()
        {
            OnRestartEvent?.Invoke();
        }

        public void Exit()
        {
            //OnExitEvent?.Invoke();
            Destroy(gameObject);
        }

        private void CheckPlayerState()
        {
            if(GameManager.Instance.GameState != GameState.PuzzleState)
            { return; }

            switch (_state)
            {
                case PlayerState.Click:
                    CheckRaycast();
                    break;

                case PlayerState.Hold:
                    _abstactPuzzleController.UpdatePlayerDotPosition();
                    CheckRaycast();
                    break;

                case PlayerState.Release:
                    OnReleaseEvent?.Invoke();

                    SetCurrentGemsColor(GemsColor.Empty);
                    SetKeyGemsColor(GemsColor.Empty);
                    SetPlayerState(PlayerState.Idle);

                    Debug.Log(_state);
                    break;
            }
        }

        private void CheckRaycast()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(_playerController.GetInterctPosition());

            if (Physics.Raycast(ray, out hit))
            {
                //if(hit.collider.tag != "Dot" || hit.collider.tag != "ColorSlot")
                //{ return; }

                if(hit.collider.tag == "Dot" && !_isShowColorPicker)
                {
                    CheckDotRaycast(hit);
                }

                if(hit.collider.tag == "ColorSlot" && _isShowColorPicker)
                {
                    //TODO: Add color slot condition
                    CheckColorSlotRaycast(hit);
                }

            }
        }

        private void CheckDotRaycast(RaycastHit hit)
        {
            //Check dot repeated
            if (_abstactPuzzleController.IsDotRepeated(hit.collider.gameObject))
            { return; }

            //Check player state when action with dot
            if (hit.collider.TryGetComponent(out Dot dot))
            {
                switch (_state)
                {
                    case PlayerState.Click:
                        Debug.LogWarning("New Dot!");
                        OnClickEvent?.Invoke(dot.gameObject);
                        break;

                    case PlayerState.Hold:
                        OnHoldEvent?.Invoke(dot.gameObject);
                        break;
                }
            }
        }

        private void CheckColorSlotRaycast(RaycastHit hit)
        {
            if (hit.collider.TryGetComponent(out ColorSlot colorSlot))
            {
                switch (_state)
                {
                    case PlayerState.Hold:
                        OnHoldEvent?.Invoke(colorSlot.gameObject);
                        break;
                }
            }
        }

        private void AddPuzzleController(GemsColor gemsColor)
        {
            AbstactPuzzleController controller = gemsColor switch
            {
                GemsColor.Empty => gameObject.AddComponent<EmptyPuzzleController>(),
                GemsColor.Red => gameObject.AddComponent<RedPuzzleController>(),
                GemsColor.Orange => gameObject.AddComponent<OrangePuzzleController>(),
                GemsColor.Yellow => gameObject.AddComponent<YellowPuzzleController>(),
                GemsColor.Green => gameObject.AddComponent<GreenPuzzleController>(),
                GemsColor.Turquoise => gameObject.AddComponent<TurquoisePuzzleController>(),
                GemsColor.Navy => gameObject.AddComponent<NavyPuzzleController>(),
                GemsColor.Violet => gameObject.AddComponent<VioletPuzzleController>(),
                GemsColor.Pink => gameObject.AddComponent<PinkPuzzleController>(),
                GemsColor.Black => gameObject.AddComponent<BlackPuzzleController>(),
                GemsColor.White => gameObject.AddComponent<WhitePuzzleController>(),
                _ => throw new System.Exception("Out of color")
            };

            _abstactPuzzleController = controller.GetComponent<AbstactPuzzleController>();
        }

        private void AddDrawController(GemsColor gemsColor)
        {
            AbstactDrawController controller = gemsColor switch
            {
                GemsColor.Empty => gameObject.AddComponent<EmptyDrawController>(),
                GemsColor.Red => gameObject.AddComponent<RedDrawController>(),
                GemsColor.Orange => gameObject.AddComponent<OrangeDrawController>(),
                GemsColor.Yellow => gameObject.AddComponent<YellowDrawController>(),
                GemsColor.Green => gameObject.AddComponent<GreenDrawController>(),
                GemsColor.Turquoise => gameObject.AddComponent<TurquoiseDrawController>(),
                GemsColor.Navy => gameObject.AddComponent<NavyDrawController>(),
                GemsColor.Violet => gameObject.AddComponent<VioletDrawController>(),
                GemsColor.Pink => gameObject.AddComponent<PinkDrawController>(),
                GemsColor.Black => gameObject.AddComponent<BlackDrawController>(),
                GemsColor.White => gameObject.AddComponent<WhiteDrawController>(),
                _ => throw new System.Exception("Out of color")
            };

            _abstactDrawController = controller.GetComponent<AbstactDrawController>();
        }

    }
}

