using System;
using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Puzzle;
using UnityEngine;

namespace Unboxed.Player
{
    public abstract class AbstactDrawController : MonoBehaviour
    {
        protected LinePlayer _linePlayer;
        protected PlayerManager _player;
        protected Dictionary<GemsColor, LinePlayer> _firstLines;
        protected Dictionary<GemsColor, LinePlayer> _secondLines;

        #region Polymorphism
        protected internal virtual void InitDrawController(List<GemsColor> gemsColors)
        {
            // For Init draw controller
        }

        protected internal virtual void UpdateDrawController(List<GemsColor> gemsColors)
        {
            // For Update draw controller
        }

        protected virtual void OnClick(GameObject dot)
        {
            // For OnClick draw controller
        }

        protected virtual void OnHold(GameObject dot)
        {
            // For OnHold draw controller
        }

        protected virtual void OnRelease()
        {
            // For OnRelease draw controller
        }

        protected virtual void OnRestart()
        {
            // For OnRestart draw controller
        }

        protected virtual void OnDrawLine()
        {
            //For OnDrawLine draw controller
        }

        protected virtual void OnComplete()
        {
            //For OnComplete draw controller
        }
        #endregion

        protected virtual void FixedUpdate()
        {
            if (GameManager.Instance.GameState == GameState.PuzzleState)
            {
                OnDrawLine();
            }
            else if (GameManager.Instance.GameState == GameState.CompleteState)
            {
                OnComplete();
            }
        }

        protected internal LinePlayer GetFirstLines(GemsColor gemsColor)
        {
            try { return _firstLines[gemsColor]; }
            catch { throw new Exception($"Missing first lines color {gemsColor}"); }
        }

        protected internal LinePlayer GetSecondLines(GemsColor gemsColor)
        {
            try { return _secondLines[gemsColor]; }
            catch { throw new Exception($"Missing second lines color {gemsColor}"); }
        }

        protected internal void BindPlayer(PlayerManager player, LinePlayer linePlayer)
        {
            _player = player;
            _player.OnClickEvent += OnClick;
            _player.OnHoldEvent += OnHold;
            _player.OnReleaseEvent += OnRelease;
            _player.OnRestartEvent += OnRestart;
            _linePlayer = linePlayer;
        }

        protected void InitSingleDictionary(List<GemsColor> gemsColors)
        {
            _firstLines ??= new Dictionary<GemsColor, LinePlayer>();

            InitDictionary(_firstLines, gemsColors);
        }

        protected void InitDualDictionary(List<GemsColor> gemsColors)
        {
            _firstLines ??= new Dictionary<GemsColor, LinePlayer>();
            _secondLines ??= new Dictionary<GemsColor, LinePlayer>();

            InitDictionary(_firstLines, gemsColors);
            InitDictionary(_secondLines, gemsColors);
        }

        protected void InitSingleLinePlayer()
        {
            InitLinePlayer(_firstLines);
        }

        protected void InitDualLinePlayer()
        {
            InitLinePlayer(_firstLines);
            InitLinePlayer(_secondLines);
        }

        protected void UpdateSingleDictionary(List<GemsColor> gemsColors)
        {
            UpdateDictionary(_firstLines, gemsColors);
        }

        protected void UpdateDualDictionary(List<GemsColor> gemsColors)
        {
            UpdateDictionary(_firstLines, gemsColors);
            UpdateDictionary(_secondLines, gemsColors);
        }

        #region LineCondition
        private bool IsDotHasColor(Dot dot)
        {
            return dot.GemsColor != GemsColor.Empty;
        }

        private bool IsDotCompleted(Dot dot)
        {
            return dot.State == Dot.DotState.Complete;
        }

        protected bool IsPlayerKeyGemsColorEmpty()
        {
            return _player.KeyGemsColor == GemsColor.Empty;
        }

        private bool IsDotsEmpty(List<GameObject> dots)
        {
            return dots.Count == 0;
        }

        protected bool IsLineCanClick(Dot dot)
        {
            return IsDotHasColor(dot) && !IsDotCompleted(dot); ;
        }

        protected bool IsLineCanDraw(List<GameObject> dots, LinePlayer line)
        {
            //Debug.LogError($"Dots empty: {IsDotsEmpty(dots)} and line have to draw: {line.IsLineHaveToDraw()}");
            return !IsDotsEmpty(dots) && line.IsLineHaveToDraw();
        }

        protected bool IsLineCanReset(List<GameObject> dots)
        {
            return dots.Count < 2;
        }

        #endregion

        #region Helper
        //private LinePlayer GetCurrentLines(GemsColor gemsColor, bool isInSecondDots)
        //{
        //    return isInSecondDots ? GetFirstLines(gemsColor) : GetSecondLines(gemsColor);
        //}

        private void InitDictionary(Dictionary<GemsColor, LinePlayer> lines, List<GemsColor> gemsColors)
        {
            foreach (var gemsColor in gemsColors)
            {
                lines.Add(gemsColor, Instantiate(_linePlayer, _player.transform));
                lines[gemsColor].name = $"{gemsColor}PlayerLine";
                Debug.LogWarning($"Add Line {gemsColor}");
            }
        }

        private void InitLinePlayer(Dictionary<GemsColor, LinePlayer> lines)
        {
            foreach (var line in lines)
            {
                line.Value.InitLinePlayer(line.Key);
            }
        }

        private void UpdateDictionary(Dictionary<GemsColor, LinePlayer> lines, List<GemsColor> gemsColors)
        {
            foreach (var gemsColor in gemsColors)
            {
                if (!lines.ContainsKey(gemsColor))
                {
                    lines.Add(gemsColor, Instantiate(_linePlayer, _player.transform));
                    lines[gemsColor].name = $"{gemsColor}PlayerLine";
                }
                else
                {
                    lines[gemsColor].ResetLine();
                }
            }
        }
        #endregion
    }
}


