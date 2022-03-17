using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Utility;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;


namespace Unboxed.Player
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerManager _player;
        private PlayerInputActions _actions;

        private Vector3 _interactPosition;

        private void Awake()
        {
            _actions = new PlayerInputActions();

            Debug.Log($"Device: {_actions}");
        }

        private void OnEnable()
        {
            _actions.Enable();
        }

        private void Start()
        {
            InitPlayerController();
        }

        internal void BindPlayer(PlayerManager player)
        {
            _player = player;
        }

        internal void InitPlayerController()
        {
            _actions.Player.Click.performed += ctx => Click(ctx);
            _actions.Player.Hold.performed += ctx => Hold(ctx);
            _actions.Player.Release.performed += ctx => Release(ctx);
            _actions.Player.Point.performed += ctx => Point(ctx);

            //_actions.Touch.TouchPress.started += ctx => TouchStart(ctx);
            //_actions.Touch.TouchPress.canceled += ctx => TouchEnd(ctx);
        }

        public void EnebleInput()
        {
            _actions.Player.Enable();
        }

        public void DisableInput()
        {
            _actions.Player.Disable();
        }

        public void Click(InputAction.CallbackContext context)
        {
            //Debug.Log($"{context.control.device} Click {context.performed}");

            //TODO: For debug
            if (_player.State == PlayerState.Idle && GameManager.Instance.GameState == GameState.PuzzleState)
            {
                _player.SetPlayerState(PlayerState.Click);
                Debug.Log(_player.State);
            }
        }

        public void Hold(InputAction.CallbackContext context)
        {
            //Debug.Log("Hold " + context.performed);

            //TODO: For debug
            if ((_player.State == PlayerState.Hold || _player.State == PlayerState.Click) && GameManager.Instance.GameState == GameState.PuzzleState)
            {
                _player.SetPlayerState(PlayerState.Hold);
                Debug.Log(_player.State);
            }
        }

        public void Release(InputAction.CallbackContext context)
        {
            //Debug.Log("Release " + context.performed);

            //TODO: For debug
            if ((_player.State == PlayerState.Hold || _player.State == PlayerState.Click) && GameManager.Instance.GameState == GameState.PuzzleState)
            {
                _player.SetPlayerState(PlayerState.Release);
                Debug.Log(_player.State);
            }
        }

        private void Point(InputAction.CallbackContext context)
        {
            //Debug.Log("Position " + context.ReadValue<Vector2>());

            Vector2 inputVector = context.ReadValue<Vector2>();
            _interactPosition = new Vector3(inputVector.x, inputVector.y, 0);
        }

        public Vector3 GetInterctPosition()
        {
            return _interactPosition;
        }

        public Vector3 GetMousePosition()
        {
            return UnboxedUtility.GetMousePosition(_interactPosition);
        }

        private void TouchStart(InputAction.CallbackContext context)
        {
            Debug.Log($"{context.control.device} Touch start: {_actions.Touch.TouchPosition.ReadValue<Vector2>()}");
        }

        private void TouchEnd(InputAction.CallbackContext context)
        {
            Debug.Log($"{context.control.device} Touch end: {_actions.Touch.TouchPosition.ReadValue<Vector2>()}");
        }
    }
}

