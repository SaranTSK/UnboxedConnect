using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Puzzle;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.Player
{
    public class PuzzleController : MonoBehaviour
    {
        //public GameObject CurrentDot => _currentDot;

        private GameObject _dotPlayerPref;
        private PlayerManager _player;
        private GameObject _currentDot;
        private GameObject _dotPlayer;
        private PuzzleGenerator _generator;

        private Dictionary<GemsColor, List<GameObject>> _dots;

        public void BindPuzzleGenerator(PuzzleGenerator generator)
        {
            _generator = generator;
        }

        // TODO: Change to parent class
        internal void BindPlayer(PlayerManager player, GameObject dotPlayerPref)
        {
            _player = player;
            _player.OnClickEvent += OnClick;
            _player.OnHoldEvent += OnHold;
            _player.OnReleaseEvent += OnRelease;
            _player.OnRestartEvent += OnRestart;
            _dotPlayerPref = dotPlayerPref;
            //_player.OnExitEvent += OnExit;
        }

        // TODO: Change to parent class
        internal void InitPuzzleController(List<GemsColor> gemsColors)
        {
            _dots ??= new Dictionary<GemsColor, List<GameObject>>();

            foreach (var gemsColor in gemsColors)
            {
                _dots.Add(gemsColor, new List<GameObject>());
            }
        }

        // TODO: Change to parent class
        internal void UpdatePuzzleController(List<GemsColor> gemsColors)
        {
            foreach (var gemsColor in gemsColors)
            {
                if (!_dots.ContainsKey(gemsColor))
                {
                    _dots.Add(gemsColor, new List<GameObject>());
                }
                else
                {
                    _dots[gemsColor].Clear();
                }
            }
        }

        internal bool IsDotRepeated(GameObject dot)
        {
            return _currentDot == dot;
        }

        internal List<GameObject> GetDots(GemsColor gemsColor)
        {
            return _dots[gemsColor];
        }

        // TODO: Change to parent class
        internal void OnClick(GameObject dot)
        {
            _currentDot = dot;
            _dotPlayer ??= Instantiate(_dotPlayerPref, _player.transform);
            _dotPlayer?.GetComponent<DotPlayer>().Show();
            _dotPlayer?.GetComponent<DotPlayer>().UpdatePosition(_player.PlayerController.GetMousePosition());

            //TODO: For debug
            //CheckDotFilled();

            if (dot.TryGetComponent(out Dot selectedDot))
            {
                Debug.Log($"Click {selectedDot.name} state: {selectedDot.State} | color: {selectedDot.GemsColor} | player color {_player.CurrentGemsColor}");
                //Debug.Log($"{dot.name} color: {selectedDot.GemsColor}");
                if (selectedDot.GemsColor != GemsColor.Empty && selectedDot.State != Dot.DotState.Complete)
                {
                    _player.SetCurrentGemsColor(selectedDot.GemsColor);
                    switch(selectedDot.State)
                    {
                        case Dot.DotState.Empty:
                            CheckSelectedDot(dot);
                            break;

                        case Dot.DotState.Filled:
                            ClearFilledDots(dot);
                            break;
                    }

                }
            }
        }

        // TODO: Change to parent class
        internal void OnHold(GameObject dot)
        {
            //TODO: For debug
            //CheckDotFilled();

            if (dot.TryGetComponent(out Dot selectedDot) && _player.CurrentGemsColor != GemsColor.Empty)
            {
                _currentDot = dot;
                var dots = GetCurrentDots();

                Debug.Log($"Hold {selectedDot.name} state: {selectedDot.State} | color: {selectedDot.GemsColor} | player color {_player.CurrentGemsColor}");

                if (selectedDot.State == Dot.DotState.Empty && IsNearCurrentDot() && (selectedDot.GemsColor == GemsColor.Empty || selectedDot.GemsColor == _player.CurrentGemsColor))
                {
                    dots.Insert(GetDotsLastedIndex(dots), dot);
                    selectedDot.OnEnter();
                    selectedDot.SetGemsColor(_player.CurrentGemsColor);

                    CheckDotsPaired();
                }
                else
                {
                    Debug.LogWarning("Position don't match!");
                }
            }
        }

        //TODO: For debug
        //private void CheckDotFilled()
        //{
        //    Grid grid = LevelManager.Instance.PuzzleGenerator.Grid;

        //    for (int x = 0; x < grid.Width; x++)
        //    {
        //        for (int y = 0; y < grid.Height; y++)
        //        {
        //            if (grid.GridArray[x, y].TryGetComponent(out Dot dot))
        //            {
        //                if (dot.State == Dot.DotState.Filled)
        //                {
        //                    Debug.LogWarning($"{dot.name} is filled!");
        //                }
        //            }
        //        }
        //    }
        //}

        // TODO: Change to parent class
        internal void UpdatePlayerDotPosition()
        {
            _dotPlayer?.GetComponent<DotPlayer>().UpdatePosition(_player.PlayerController.GetMousePosition());
        }

        // TODO: Change to parent class
        internal void OnRelease()
        {
            _dotPlayer?.GetComponent<DotPlayer>().Hide();

            if (_player.CurrentGemsColor != GemsColor.Empty && GetCurrentDots().Count > 0)
            {
                Debug.LogWarning($"Result Before {_player.CurrentGemsColor}: {GetCurrentDots().Count}");
                RemovePlayerDot();
                ClearEmptyDots();
                Debug.LogWarning($"Result After {_player.CurrentGemsColor}: {GetCurrentDots().Count}");

                _currentDot = null;
            }
        }

        // TODO: Change to parent class
        internal void OnRestart()
        {
            foreach(var dots in _dots)
            {
                foreach(var dot in dots.Value)
                {
                    dot.GetComponent<Dot>().OnExit();
                }
                dots.Value.Clear();
            }
        }

        //internal void OnExit()
        //{
        //    //OnRestart();
        //    _dots.Clear();
        //}

        private void CheckSelectedDot(GameObject dot)
        {
            if (IsPairInit(_player.CurrentGemsColor))
                { ResetDots(); }

            var dots = GetCurrentDots();

            dots.Add(dot);
            dots.Add(_dotPlayer);
            dot.GetComponent<Dot>().OnEnter();
        }

        private void ResetDots()
        {
            var dots = GetCurrentDots();

            for (int i = 0; i < dots.Count; i++)
            {
                if (dots[i].TryGetComponent(out Dot selectedDot))
                {
                    selectedDot.OnExit();
                    //selectedDot.ResetDotColor();
                }
            }
            dots.Clear();
        }

        private void CheckDotsPaired()
        {
            if (IsDotPaired(_player.CurrentGemsColor))
            {
                CheckPuzzleComplete();
            }
            else
            {
                Debug.LogWarning($"{_player.CurrentGemsColor} not paired");
            }
        }

        private void CheckPuzzleComplete()
        {
            bool isAllPaired = IsAllPaired();
            bool isAllFilled = IsAllFilled();

            if (isAllPaired && isAllFilled)
            {
                Debug.Log("Puzzle complete!");
                StartCoroutine(OnComplete());
            }
            else if(isAllPaired && !isAllFilled)
            {
                Debug.Log("Some dots aren't filled!");
            }
            else
            {
                Debug.Log("Dot paired!");
            }
            _player.SetPlayerState(PlayerState.Release);
        }

        private void RemovePlayerDot()
        {
            var dots = GetCurrentDots();

            if (dots.Count > 1)
            {
                dots.RemoveAt(GetDotsLastedIndex(dots));
            }
        }

        private void ClearFilledDots(GameObject dot)
        {
            var dots = GetCurrentDots();
            var index = dots.IndexOf(dot);
            var isStarter = dot.GetComponent<Dot>().IsStarter;

            Debug.Log($"Before {index} and count: {dots.Count}");
            if(index != GetDotsLastedIndex(dots) && !isStarter)
            {
                for (int i = index + 1; i < dots.Count; i++)
                {
                    if (dots[i].TryGetComponent(out Dot selectedDot))
                    {
                        selectedDot.OnExit();
                        //selectedDot.ResetDotColor();
                    }
                }
                dots.RemoveRange(index + 1, dots.Count - (index + 1));
                dots.Add(_dotPlayer);
            }
            else if(isStarter)
            {
                ResetDots();
                dots.Add(dot);
                dots.Add(_dotPlayer);
                dot.GetComponent<Dot>().OnEnter();
            }
            else
            {
                dots.Add(_dotPlayer);
            }

            //Debug.Log($"After {index} and count: {dots.Count}");
        }

        private IEnumerator OnComplete()
        {
            foreach(var dots in _dots)
            {
                foreach(var dot in dots.Value)
                {
                    if(dot.TryGetComponent(out Dot puzzleDot))
                    {
                        puzzleDot.OnComplete();
                    }
                }
            }

            LevelManager.Instance.OnComplete();
            LevelManager.Instance.SavegameData();
            GameManager.Instance.SetGameState(GameState.CompleteState);

            yield return new WaitForSecondsRealtime(1f);

            StartCoroutine(LevelManager.Instance.LoadLevel());
        }

        private void ClearEmptyDots()
        {
            var dots = GetCurrentDots();

            if (dots.Count < 2)
            {
                if (dots[0].TryGetComponent(out Dot selectedDot))
                {
                    selectedDot.OnExit();
                    //selectedDot.ResetDotColor();
                }

                dots.Clear();
            }
        }

        private int GetDotsLastedIndex(List<GameObject> dots)
        {
            return dots.Count - 1;
        }

        private List<GameObject> GetCurrentDots()
        {
            return (_player.CurrentGemsColor == GemsColor.Empty) ? null : _dots[_player.CurrentGemsColor];
        }

        private bool IsNearCurrentDot()
        {
            Grid grid = _generator.Grid;

            //if (grid == null) { Debug.LogWarning("Checker grid is null!"); }

            var dots = GetCurrentDots();
            GameObject previousDot = dots[GetDotsLastedIndex(dots) - 1];

            int prevX = UnboxedUtility.GetIntIndexFromName(previousDot.name, 1);
            int prevY = UnboxedUtility.GetIntIndexFromName(previousDot.name, 2);
            int prevIndex = UnboxedUtility.GetDotArrayIndex(prevX, prevY, grid);

            int currentX = UnboxedUtility.GetIntIndexFromName(_currentDot.name, 1);
            int currentY = UnboxedUtility.GetIntIndexFromName(_currentDot.name, 2);
            int currentIndex = UnboxedUtility.GetDotArrayIndex(currentX, currentY, grid);

            //Debug.Log($"Prev name: {previousDot.name} index: {prevIndex} || Current name: {CurrentDot.name} index: {currentIndex} | " +
            //    $"LowerL: {UnboxedUtility.GetLowerLeftDotArrayIndex(prevX, prevY, grid)} | " +
            //    $"MiddleL: {UnboxedUtility.GetMiddleLeftDotArrayIndex(prevX, prevY, grid)} | " +
            //    $"UpperL: {UnboxedUtility.GetUpperLeftDotArrayIndex(prevX, prevY, grid)} | " +
            //    $"UpperR: {UnboxedUtility.GetUpperRightDotArrayIndex(prevX, prevY, grid)} | " +
            //    $"MiddleR: {UnboxedUtility.GetMiddleRightDotArrayIndex(prevX, prevY, grid)} | " +
            //    $"LowerR: {UnboxedUtility.GetLowerRightDotArrayIndex(prevX, prevY, grid)}");

            //Debug.Log($"Has LowerL: {UnboxedUtility.IsHasLowerLeft(prevX, prevY, grid)} | " +
            //    $"Has MiddleL: {UnboxedUtility.IsHasMiddleLeft(prevX)} | " +
            //    $"Has UpperL: {UnboxedUtility.IsHasUpperLeft(prevX, prevY, grid)} | " +
            //    $"Has UpperR: {UnboxedUtility.IsHasUpperRight(prevX, prevY, grid)} | " +
            //    $"Has MiddleR: {UnboxedUtility.IsHasMiddleRight(prevX, grid)} | " +
            //    $"Has LowerR: {UnboxedUtility.IsHasLowerRight(prevX, prevY, grid)}");

            return (currentIndex == UnboxedUtility.GetLowerLeftDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasLowerLeft(prevX, prevY, grid)) ||
                (currentIndex == UnboxedUtility.GetMiddleLeftDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasMiddleLeft(prevX)) ||
                (currentIndex == UnboxedUtility.GetUpperLeftDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasUpperLeft(prevX, prevY, grid)) ||
                (currentIndex == UnboxedUtility.GetUpperRightDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasUpperRight(prevX, prevY, grid)) ||
                (currentIndex == UnboxedUtility.GetMiddleRightDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasMiddleRight(prevX, grid)) ||
                (currentIndex == UnboxedUtility.GetLowerRightDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasLowerRight(prevX, prevY, grid));
        }

        private bool IsDotPaired(GemsColor gemsColor)
        {
            var pairs = _generator.DotPairing;

            foreach(var pair in pairs[gemsColor])
            {
                if(pair.TryGetComponent(out Dot dot))
                {
                    if (dot.State != Dot.DotState.Filled)
                        return false;
                }
            }

            return true;
        }

        private bool IsPairInit(GemsColor gemsColor)
        {
            var pairs = _generator.DotPairing;

            foreach (var pair in pairs[gemsColor])
            {
                if (pair.TryGetComponent(out Dot dot))
                {
                    if (dot.State == Dot.DotState.Filled)
                        return true;
                }
            }
            return false;
        }

        private bool IsAllPaired()
        {
            var pairs = _generator.DotPairing;

            bool isAllPaired = true;

            foreach (var pair in pairs)
            {
                if (!IsDotPaired(pair.Key))
                {
                    //Debug.LogWarning($"{pair.Key} is not paired");
                    isAllPaired = false;
                }
            }

            return isAllPaired;
        }

        private bool IsAllFilled()
        {
            Grid grid = _generator.Grid;

            bool isAllFilled = true;

            for (int x = 0; x < grid.Width; x++)
            {
                for (int y = 0; y < grid.Height; y++)
                {
                    if (grid.GridArray[x, y].TryGetComponent(out Dot dot))
                    {
                        if (dot.State != Dot.DotState.Filled)
                        {
                            //Debug.Log($"{dot.name} is not filled!");
                            isAllFilled = false;
                        }
                    }
                }
            }

            return isAllFilled;
        }


    }
}

