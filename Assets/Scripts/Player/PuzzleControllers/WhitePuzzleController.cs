using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;

namespace Unboxed.Player
{
    public class WhitePuzzleController : AbstactPuzzleController
    {
        protected internal override void InitPuzzleController(List<GemsColor> gemsColors)
        {
            // For Init puzzle controller
            Debug.Log($"Init WhitePuzzleController");
        }

        protected internal override void UpdatePuzzleController(List<GemsColor> gemsColors)
        {
            // For Update puzzle controller
            Debug.Log($"Update WhitePuzzleController");
        }

        protected internal override void UpdatePlayerDotPosition()
        {
            // For Update override dot position
            Debug.Log($"Update player WhitePuzzleController");
        }

        protected override void OnClick(GameObject dot)
        {
            // For OnClick puzzle controller
            Debug.Log($"Click WhitePuzzleController");
        }

        protected override void OnHold(GameObject dot)
        {
            // For OnHold puzzle controller
            Debug.Log($"Hold WhitePuzzleController");
        }

        protected override void OnRelease()
        {
            // For OnRelease puzzle controller
            Debug.Log($"Release WhitePuzzleController");
        }

        protected override void OnRestart()
        {
            // For OnRestart puzzle controller
            Debug.Log($"Restart WhitePuzzleController");
        }
    }
}


