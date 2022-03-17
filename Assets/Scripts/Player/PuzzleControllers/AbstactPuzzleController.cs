using System;
using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Puzzle;
using Unboxed.Scriptable;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.Player
{
    public abstract class AbstactPuzzleController : MonoBehaviour
    {
        protected GameObject _dotPlayerPref;
        protected PlayerManager _player;
        protected GameObject _currentDot;
        protected GameObject _firstDotPlayer;
        protected GameObject _secondDotPlayer;

        protected Dictionary<GemsColor, List<GameObject>> _firstDots;
        protected Dictionary<GemsColor, List<GameObject>> _secondDots;

        protected PuzzleGenerator _generator;

        public void BindPuzzleGenerator(PuzzleGenerator generator)
        {
            _generator = generator;
        }

        #region Polymorphism
        protected internal virtual void InitPuzzleController(List<GemsColor> gemsColors)
        {
            // For Init puzzle controller
        }

        protected internal virtual void UpdatePuzzleController(List<GemsColor> gemsColors)
        {
            // For Update puzzle controller
        }

        protected internal virtual void UpdatePlayerDotPosition()
        {
            // For Update player dot position
        }

        protected virtual void OnClick(GameObject dot)
        {
            // For OnClick puzzle controller
        }

        protected virtual void OnHold(GameObject dot)
        {
            // For OnHold puzzle controller
        }

        protected virtual void OnRelease()
        {
            // For OnRelease puzzle controller
        }

        protected virtual void OnRestart()
        {
            // For OnRestart puzzle controller
        }

        //TODO: Check current color and dict key color
        protected virtual void OnClickedEmptyDot(Dot dot)
        {
            // For OnClickedEmptyDot puzzle controller
        }

        //TODO: Check current color and dict key color
        protected virtual void OnClickedFilledDot(Dot dot)
        {
            // For OnClickedFilledDot puzzle controller
        }

        protected virtual void OnEnterDot(Dot dot)
        {
            // For OnEnterDot puzzle controller
        }

        #endregion

        #region SpecialDotsCondition
        protected virtual void OnClickEmptySpecialDot(Dot dot)
        {
            // For OnClickEmptySpecialDot puzzle controller
        }

        protected virtual void OnClickFilledSpecialDot(Dot dot)
        {
            // For OnClickFilledSpecialDot puzzle controller
        }

        protected virtual void OnEnterSpecialDot(Dot dot)
        {
            // For OnEnterSpecialDot puzzle controller
        }

        protected virtual void OnSpecialEventTrigger()
        {
            // For OnSpecialEventTrigger puzzle controller
        }

        protected bool IsHasSpecilaEventTrigger()
        {
            // For IsHasSpecilaEventTrigger puzzle controller
            return false;
        }

        protected virtual void OnEnterColorSlot(ColorSlot colorSlot)
        {
            // For OnEnterColorSlot puzzle controller
        }
        #endregion

        protected internal void BindPlayer(PlayerManager player, GameObject dotPlayerPref)
        {
            _player = player;
            _player.OnClickEvent += OnClick;
            _player.OnHoldEvent += OnHold;
            _player.OnReleaseEvent += OnRelease;
            _player.OnRestartEvent += OnRestart;
            _dotPlayerPref = dotPlayerPref;
        }

        protected internal List<GameObject> GetFirstDots(GemsColor gemsColor)
        {
            try { return _firstDots[gemsColor]; }
            catch { throw new Exception($"Missing first dots color {gemsColor}"); }
        }

        protected internal List<GameObject> GetSecondDots(GemsColor gemsColor)
        {
            try { return _secondDots[gemsColor]; }
            catch { throw new Exception($"Missing second dots color {gemsColor}"); }
        }

        protected internal bool IsDotRepeated(GameObject dot)
        {
            return _currentDot == dot;
        }

        protected void InitSingleDictionary(List<GemsColor> gemsColors)
        {
            _firstDots ??= new Dictionary<GemsColor, List<GameObject>>();

            InitDictionary(_firstDots, gemsColors);
        }

        protected void InitDualDictionary(List<GemsColor> gemsColors)
        {
            _firstDots ??= new Dictionary<GemsColor, List<GameObject>>();
            _secondDots ??= new Dictionary<GemsColor, List<GameObject>>();

            InitDictionary(_firstDots, gemsColors);
            InitDictionary(_secondDots, gemsColors);
        }

        protected void UpdateSingleDictionary(List<GemsColor> gemsColors)
        {
            UpdateDictionary(_firstDots, gemsColors);
        }

        protected void UpdateDualDictionary(List<GemsColor> gemsColors)
        {
            UpdateDictionary(_firstDots, gemsColors);
            UpdateDictionary(_secondDots, gemsColors);
        }

        #region DotPlayer
        protected void SpawnFirstPlayerDot(Transform transform)
        {
            _firstDotPlayer ??= Instantiate(_dotPlayerPref, transform);
        }

        protected void SpawnSecondDotPlayer(Transform transform)
        {
            _secondDotPlayer ??= Instantiate(_dotPlayerPref, transform);
        }

        protected void ShowDotPlayer(GameObject playerDot)
        {
            playerDot.GetComponent<DotPlayer>().Show();
        }

        protected void HideDotPlayer(GameObject playerDot)
        {
            playerDot.GetComponent<DotPlayer>().Hide();
        }

        protected void UpdateDotPlayerPosition(GameObject playerDot, Vector3 position)
        {
            playerDot?.GetComponent<DotPlayer>().UpdatePosition(position);
        }

        protected void RemoveSingleDotPlayer(GemsColor gemsColor)
        {
            RemoveDotPlayer(_firstDots[gemsColor]);
        }

        protected void RemoveDualDotPLayer(GemsColor gemsColor)
        {
            RemoveDotPlayer(_firstDots[gemsColor]);
            RemoveDotPlayer(_secondDots[gemsColor]);
        }
        #endregion

        #region DotCondition
        private bool IsDotEmpty(Dot dot)
        {
            return dot.State == Dot.DotState.Empty;
        }

        private bool IsDotCompleted(Dot dot)
        {
            return dot.State == Dot.DotState.Complete;
        }

        private bool IsDotHasColor(Dot dot)
        {
            return dot.GemsColor != GemsColor.Empty;
        }

        //private bool IsDotSpecialHasColor(Dot dot)
        //{
        //    return dot.GetComponent<DotSpecial>().GetLastedGemsColors() != GemsColor.Empty;
        //}

        private bool IsDotSpecialHasSlot(Dot dot)
        {
            if(dot.TryGetComponent(out DotSpecial dotSpecial))
            {
                return dotSpecial.GetEmptyFillSlotIndex() > -1;
            }
            else
            {
                return false;
            }
        }

        private bool IsDotMatchColor(Dot dot)
        {
            return dot.KeyGemsColor == _player.KeyGemsColor;
        }

        private bool IsPlayerCurrentGemsColorEmpty()
        {
            return _player.CurrentGemsColor == GemsColor.Empty;
        }

        private bool IsPlayerKeyGemsColorEmpty()
        {
            return _player.KeyGemsColor == GemsColor.Empty;
        }

        protected virtual bool IsDotCanClick(Dot dot)
        {
            Debug.Log($"{dot.name} color: {dot.GemsColor} state: {dot.State}");
            //return (IsDotHasColor(dot) || IsDotSpecialHasColor(dot)) && !IsDotCompleted(dot);
            return IsDotHasColor(dot) && !IsDotCompleted(dot);
        }

        protected virtual bool IsDotCanEnter(Dot dot)
        {
            return !IsPlayerCurrentGemsColorEmpty() && !IsPlayerKeyGemsColorEmpty() && IsDotEmpty(dot) && (!IsDotHasColor(dot) || IsDotMatchColor(dot) || IsDotSpecialHasSlot(dot));
        }

        protected virtual bool IsDotCanRelease(GemsColor gemsColor)
        {
            return !IsPlayerKeyGemsColorEmpty() && !IsFirstDotsEmpty(gemsColor);
        }

        protected bool IsDotsHaveToClear(List<GameObject> dots)
        {
            return dots.Count < 2;
        }
        #endregion

        #region DotsCondition
        protected void ResetSingleDictionary()
        {
            ResetDictionary(_firstDots);
        }

        protected void ResetDualDictionary()
        {
            ResetDictionary(_firstDots);
            ResetDictionary(_secondDots);
        }

        private void ResetDots(List<GameObject> dots)
        {
            for (int i = 0; i < dots.Count; i++)
            {
                if (dots[i].TryGetComponent(out Dot dot))
                {
                    if (!dot.IsSpecial)
                    {
                        dot.OnExit();
                        dot.ResetDotColorByIndex(0);
                    }
                    else
                    {
                        DotSpecial dotSpecial = dot.GetComponent<DotSpecial>();
                        int index = dotSpecial.GetFillSlotIndexByGemsColor(_player.KeyGemsColor);

                        if (index > -1)
                        {
                            dot.OnExit();
                            dotSpecial.ResetDotFillColorByIndex(index);
                        }
                    }
                }
            }
            dots.Clear();
        }

        protected void ResetSingleDots(GemsColor gemsColor)
        {
            ResetDots(_firstDots[gemsColor]);
        }

        protected void ResetDualDots(GemsColor gemsColor)
        {
            ResetDots(_firstDots[gemsColor]);
            ResetDots(_secondDots[gemsColor]);
        }

        private void ResetRangeDots(List<GameObject> dots, int start, int end)
        {
            //TODO: Reset with range
            for (int i = start; i < end; i++)
            {
                if (dots[i].TryGetComponent(out Dot dot))
                {
                    if (!dot.IsSpecial)
                    {
                        dot.OnExit();
                        dot.ResetDotColorByIndex(0);
                    }
                    else
                    {
                        DotSpecial dotSpecial = dot.GetComponent<DotSpecial>();
                        int index = dotSpecial.GetFillSlotIndexByGemsColor(_player.KeyGemsColor);

                        if(index > -1)
                        {
                            dot.OnExit();
                            dotSpecial.ResetDotFillColorByIndex(index);
                        }
                    }
                }
            }
            dots.RemoveRange(start, dots.Count - (start));
        }

        protected void ResetRangeSingleDots(GemsColor gemsColor, int start, int end)
        {
            ResetRangeDots(_firstDots[gemsColor], start, end);
        }

        protected void ResetRangeDualDots(GemsColor gemsColor, int start, int end)
        {
            ResetRangeDots(_firstDots[gemsColor], start, end);
            ResetRangeDots(_secondDots[gemsColor], start, end);
        }

        private void DotsComplete(Dictionary<GemsColor, List<GameObject>> dotsDict)
        {
            foreach (var dots in dotsDict)
            {
                foreach (var dot in dots.Value)
                {
                    if (dot.TryGetComponent(out Dot puzzleDot))
                    {
                        puzzleDot.OnComplete();
                    }
                }
            }
        }

        protected void SingleDotsComplete()
        {
            DotsComplete(_firstDots);
        }

        protected void DualDotsComplete()
        {
            DotsComplete(_firstDots);
            DotsComplete(_secondDots);
        }

        protected bool IsFirstDotsEmpty(GemsColor gemsColor)
        {
            return GetFirstDots(gemsColor).Count == 0;
        }

        protected bool IsSecondDotsEmpty(GemsColor gemsColor)
        {
            return GetSecondDots(gemsColor).Count == 0;
        }

        protected int GetDotsLastedIndex(List<GameObject> dots)
        {
            return dots.Count > 0 ? dots.Count - 1 : 0;
        }
        #endregion

        #region PuzzleCondition
        protected bool IsDotLinked(GameObject prevDot, GameObject newDot)
        {
            Grid grid = _generator.Grid;

            int prevX = UnboxedUtility.GetIntIndexFromName(prevDot.name, 1);
            int prevY = UnboxedUtility.GetIntIndexFromName(prevDot.name, 2);

            int newX = UnboxedUtility.GetIntIndexFromName(newDot.name, 1);
            int newY = UnboxedUtility.GetIntIndexFromName(newDot.name, 2);
            int newIndex = UnboxedUtility.GetDotArrayIndex(newX, newY, grid);

            return (newIndex == UnboxedUtility.GetLowerLeftDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasLowerLeft(prevX, prevY, grid)) ||
                (newIndex == UnboxedUtility.GetMiddleLeftDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasMiddleLeft(prevX)) ||
                (newIndex == UnboxedUtility.GetUpperLeftDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasUpperLeft(prevX, prevY, grid)) ||
                (newIndex == UnboxedUtility.GetUpperRightDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasUpperRight(prevX, prevY, grid)) ||
                (newIndex == UnboxedUtility.GetMiddleRightDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasMiddleRight(prevX, grid)) ||
                (newIndex == UnboxedUtility.GetLowerRightDotArrayIndex(prevX, prevY, grid) && UnboxedUtility.IsHasLowerRight(prevX, prevY, grid));
        }

        protected void OnDotPairedHanderler()
        {
            if (IsDotPaired(_player.KeyGemsColor))
            {
                OnPuzzleCompleteHanderler();
            }
            else
            {
                Debug.LogWarning($"{_player.CurrentGemsColor} not paired");
            }
        }

        protected void OnPuzzleCompleteHanderler()
        {
            bool isAllPaired = IsAllDotPaired();
            bool isAllFilled = IsAllDotFilled();

            if (isAllPaired && isAllFilled)
            {
                Debug.Log("Puzzle complete!");
                StartCoroutine(OnCompleted());
            }
            else if (isAllPaired && !isAllFilled)
            {
                Debug.Log("Some dots aren't filled!");
            }
            else
            {
                Debug.Log("Dot paired!");
            }
            _player.SetPlayerState(PlayerState.Release);
        }

        protected virtual IEnumerator OnCompleted()
        {
            yield return null;
        }

        private PuzzleStarterPairing GetStarterParingByGemsColor(GemsColor gemsColor)
        {
            try
            {
                List<PuzzleStarterPairing> parings = _generator.StarterParings;

                foreach (PuzzleStarterPairing pairing in parings)
                {
                    if (gemsColor == pairing.gemsColor)
                    {
                        return pairing;
                    }
                }

                throw new Exception($"All starter parings is not defined!");
            }
            catch
            {
                throw new Exception($"All starter parings is not defined!");
            }
        }

        protected bool IsStarterDotInit(GemsColor gemsColor)
        {
            PuzzleStarterPairing pairing = GetStarterParingByGemsColor(gemsColor);
            //Debug.LogWarning($"Paring {pairing.gemsColor} lenght : {pairing.pairingIndex.Length}");

            for (int i = 0; i < pairing.pairingIndex.Length; i++)
            {
                //Debug.LogWarning($"Index : {pairing.pairingIndex[i]}");
                Dot dot = _generator.GetDotByIndex(pairing.pairingIndex[i]);
                //Debug.LogWarning($"Dot {dot.gameObject.name} Index : {pairing.pairingIndex[i]} | State : {dot.State}");
                if (dot.State == Dot.DotState.Filled)
                {
                    //Debug.LogWarning($"Init {gemsColor} : true");
                    return true;
                }
            }

            Debug.LogWarning($"Init {gemsColor} : false");
            return false;
        }

        protected bool IsDotPaired(GemsColor gemsColor)
        {
            PuzzleStarterPairing pairing = GetStarterParingByGemsColor(gemsColor);

            for (int i = 0; i < pairing.pairingIndex.Length; i++)
            {
                Dot dot = _generator.GetDotByIndex(pairing.pairingIndex[i]);
                if (dot.State != Dot.DotState.Filled)
                {
                    return false;
                }
            }

            return true;
        }

        protected bool IsAllDotPaired()
        {
            bool isAllPaired = true;

            List<PuzzleStarterPairing> parings = _generator.StarterParings;

            foreach (PuzzleStarterPairing pairing in parings)
            {
                if (!IsDotPaired(pairing.gemsColor))
                {
                    isAllPaired = false;
                }
            }

            return isAllPaired;
        }

        protected bool IsAllDotFilled()
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
                            isAllFilled = false;
                        }
                    }
                }
            }

            return isAllFilled;
        }
        #endregion

        #region Helper
        //private List<GameObject> GetCurrentDots(GemsColor gemsColor, bool isInSecondDots)
        //{
        //    return isInSecondDots ? GetSecondDots(gemsColor) : GetFirstDots(gemsColor);
        //}
        protected void SetCurrentDot(GameObject dot)
        {
            _currentDot = dot;
        }

        private void InitDictionary(Dictionary<GemsColor, List<GameObject>> dots, List<GemsColor> gemsColors)
        {
            foreach (var gemsColor in gemsColors)
            {
                dots.Add(gemsColor, new List<GameObject>());
                Debug.LogWarning($"Add Puzzle {gemsColor}");
            }
        }

        private void UpdateDictionary(Dictionary<GemsColor, List<GameObject>> dots, List<GemsColor> gemsColors)
        {
            foreach (var gemsColor in gemsColors)
            {
                if (!dots.ContainsKey(gemsColor))
                {
                    dots.Add(gemsColor, new List<GameObject>());
                }
                else
                {
                    dots[gemsColor].Clear();
                }
            }
        }

        private void RemoveDotPlayer(List<GameObject> dots)
        {
            if (dots.Count > 1)
            {
                dots.RemoveAt(GetDotsLastedIndex(dots));
            }
        }

        private void ResetDictionary(Dictionary<GemsColor, List<GameObject>> dots)
        {
            foreach (var dotsKey in dots)
            {
                foreach (var dot in dotsKey.Value)
                {
                    dot.GetComponent<Dot>().OnExit();
                }
                dotsKey.Value.Clear();
            }
        }
        #endregion
    }
}


