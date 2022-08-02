using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Unboxed.Scriptable
{
    [CreateAssetMenu(fileName = "PuzzleData", menuName = "ScriptableObjects/PuzzleData", order = 0)]
    public class PuzzleScriptable : ScriptableObject
    {
        public string puzzleName;
        public string hintText;
        public int width;
        public int height;
        public float gridGap;
        public bool oddStart;
        public List<GemsColor> colors;
        public List<PuzzleCondition> puzzleConditions;
        public List<PuzzleStarterPairing> starterPairings;
        public List<AssetReference> dots;
    }

    [System.Serializable]
    public class PuzzleCondition
    {
        public GemsColor gemsColor;
        public int amount;
    }

    [System.Serializable]
    public class PuzzleStarterPairing
    {
        public GemsColor gemsColor;
        public int[] pairingIndex;
    }

    //[System.Serializable]
    //public class PuzzleEvent
    //{
    //    public GemsColor gemsColor;
    //    public int triggerIndex;
    //    public int[] firstTargetIndexs;
    //    public int[] secondTargetIndexs;
    //    public GemsColor firstTargetLine;
    //    public GemsColor secondTargetLine;
    //}
}

