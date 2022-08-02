using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;

namespace Unboxed.Player
{
    public class TurquoisePuzzleController : AbstactPuzzleController
    {
        protected internal override void InitPuzzleController(List<GemsColor> gemsColors)
        {
            // For Init puzzle controller
            Debug.Log($"Init TurquoisePuzzleController");
        }

        protected internal override void UpdatePuzzleController(List<GemsColor> gemsColors)
        {
            // For Update puzzle controller
            Debug.Log($"Update TurquoisePuzzleController");
        }

        protected internal override void UpdatePlayerDotPosition()
        {
            // For Update override dot position
            Debug.Log($"Update player TurquoisePuzzleController");
        }

        protected override void OnClick(GameObject dot)
        {
            // For OnClick puzzle controller
            Debug.Log($"Click TurquoisePuzzleController");
        }

        protected override void OnHold(GameObject dot)
        {
            // For OnHold puzzle controller
            Debug.Log($"Hold TurquoisePuzzleController");
        }

        protected override void OnRelease()
        {
            // For OnRelease puzzle controller
            Debug.Log($"Release TurquoisePuzzleController");
        }

        protected override void OnRestart()
        {
            // For OnRestart puzzle controller
            Debug.Log($"Restart TurquoisePuzzleController");
        }
    }
}


