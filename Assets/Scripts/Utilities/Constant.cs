using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Manager;
using Unboxed.UI;

namespace Unboxed.Utility
{
    public static class Constant
    {
        public static GemsColor DefaultGemsColor = GemsColor.Empty;
        public static GameMode DefaultGameMode = GameMode.Normal;
        public static int DefaultTier = 0;
        public static int DefaultLevel = -1;

        public static int MaxGameMode = 2;
        public static int MaxGemsColor = 11;
        public static int MaxTier = 6;

        public static int MaxLevelPerPanel = 25;
        public static int MaxLevelPerTier = 50;

        public static int CanvasWidthRef = 3840;
        public static int CanvasHeightRef = 2160;
    }
}


