using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;

namespace Unboxed.Player
{
    public class GreenDrawController : AbstactDrawController
    {
        protected internal override void InitDrawController(List<GemsColor> gemsColors)
        {
            // For Init draw controller
            Debug.Log($"Init GreenDrawController");
        }

        protected internal override void UpdateDrawController(List<GemsColor> gemsColors)
        {
            // For Update draw controller
            Debug.Log($"Update GreenDrawController");
        }

        protected override void OnClick(GameObject dot)
        {
            // For OnClick draw controller
            Debug.Log($"Click GreenDrawController");
        }

        protected override void OnHold(GameObject dot)
        {
            // For OnHold draw controller
            Debug.Log($"Hold GreenDrawController");
        }

        protected override void OnRelease()
        {
            // For OnRelease draw controller
            Debug.Log($"Release GreenDrawController");
        }

        protected override void OnRestart()
        {
            // For OnRestart draw controller
            Debug.Log($"Restart GreenDrawController");
        }
    }
}

