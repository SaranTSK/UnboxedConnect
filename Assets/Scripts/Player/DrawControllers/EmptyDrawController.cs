using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Puzzle;
using UnityEngine;

namespace Unboxed.Player
{
    public class EmptyDrawController : AbstactDrawController
    {
        protected internal override void InitDrawController(List<GemsColor> gemsColors)
        {
            // For Init draw controller
            Debug.Log($"Init EmptyDrawController");
            InitSingleDictionary(gemsColors);
            InitSingleLinePlayer();
        }

        protected internal override void UpdateDrawController(List<GemsColor> gemsColors)
        {
            // For Update draw controller
            Debug.Log($"Update EmptyDrawController");
            UpdateSingleDictionary(gemsColors);
            InitSingleLinePlayer();
        }

        protected override void OnClick(GameObject dot)
        {
            // For OnClick draw controller
            Debug.Log($"Click EmptyDrawController");
            if(IsLineCanClick(dot.GetComponent<Dot>()))
            {
                GetFirstLines(_player.KeyGemsColor).Select();
            }
        }

        protected override void OnHold(GameObject dot)
        {
            // For OnHold draw controller
            Debug.Log($"Hold EmptyDrawController");

            // For draw line when picked color
            if (dot.TryGetComponent(out ColorSlot colorSlot))
            {
                GetFirstLines(colorSlot.GemsColor).Select();
            }
        }

        protected override void OnRelease()
        {
            // For OnRelease draw controller
            Debug.Log($"Release EmptyDrawController");
            if (!IsPlayerKeyGemsColorEmpty())
            {
                List<GameObject> dots = _player.AbstactPuzzleController.GetFirstDots(_player.KeyGemsColor);
                LinePlayer line = GetFirstLines(_player.KeyGemsColor);

                line.Deselect();

                if (IsLineCanReset(dots))
                {
                    //TODO: Reset LinePlayer
                    Debug.LogWarning($"Reset line {_player.KeyGemsColor} lenght {dots.Count}");
                    line.ResetLine();
                }
                else
                {
                    //TODO: Draw LinePlayer
                    line.DrawLine(dots);
                }
            }
        }

        protected override void OnRestart()
        {
            // For OnRestart draw controller
            Debug.Log($"Restart EmptyDrawController");
            foreach(var lines in _firstLines)
            {
                lines.Value.ResetLine();
                lines.Value.Hide();
            }
        }

        protected override void OnDrawLine()
        {
            //For OnDrawLine draw controller
            if(!IsPlayerKeyGemsColorEmpty())
            {

                List<GameObject> dots = _player.AbstactPuzzleController.GetFirstDots(_player.KeyGemsColor);
                LinePlayer line = GetFirstLines(_player.KeyGemsColor);

                if (IsLineCanDraw(dots, line))
                {
                    line.Show();
                    line.DrawLine(dots);
                }
                else
                {
                    line.Hide();
                }
            }
        }

        protected override void OnComplete()
        {
            //For OnComplete draw controller
            foreach (var lines in _firstLines)
            {
                lines.Value.ResetLine();
                lines.Value.Complete();
                lines.Value.Hide();
            }
        }
    }
}

