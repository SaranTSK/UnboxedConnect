using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;

namespace Unboxed.Player
{
    public class BlackPuzzleController : AbstactPuzzleController
    {
        protected internal override void InitPuzzleController(List<GemsColor> gemsColors)
        {
            // For Init puzzle controller
            Debug.Log($"Init BlackPuzzleController");
        }

        protected internal override void UpdatePuzzleController(List<GemsColor> gemsColors)
        {
            // For Update puzzle controller
            Debug.Log($"Update BlackPuzzleController");
        }

        protected internal override void UpdatePlayerDotPosition()
        {
            // For Update override dot position
            Debug.Log($"Update player BlackPuzzleController");
        }

        protected override void OnClick(GameObject dot)
        {
            // For OnClick puzzle controller
            Debug.Log($"Click BlackPuzzleController");
        }

        protected override void OnHold(GameObject dot)
        {
            // For OnHold puzzle controller
            Debug.Log($"Hold BlackPuzzleController");
        }

        protected override void OnRelease()
        {
            // For OnRelease puzzle controller
            Debug.Log($"Release BlackPuzzleController");
        }

        protected override void OnRestart()
        {
            // For OnRestart puzzle controller
            Debug.Log($"Restart BlackPuzzleController");
        }
    }
}


