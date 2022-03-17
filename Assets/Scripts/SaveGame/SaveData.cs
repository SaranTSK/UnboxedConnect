using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Manager;
using Unboxed.Utility;

namespace Unboxed.SaveGame
{
    [System.Serializable]
    public class SaveData
    {
        public bool[] purchasedPack;
        public int[] lastedNormalPuzzleUnlocked;
        public int[] lastedConditionPuzzleUnlocked;

        public SaveData()
        {
            purchasedPack = new bool[Constant.MaxGemsColor];
            lastedNormalPuzzleUnlocked = new int[Constant.MaxGemsColor];
            lastedConditionPuzzleUnlocked = new int[Constant.MaxGemsColor];

            purchasedPack[(int)GemsColor.Empty] = true;
            //purchasedPack[(int)GemsColor.Yellow] = true;
            lastedNormalPuzzleUnlocked[(int)GemsColor.Empty] = 1;
            //lastedNormalPuzzleUnlocked[(int)GemsColor.Yellow] = 57;
        }

        public SaveData(SaveData saveData)
        {
            purchasedPack = saveData.purchasedPack;
            lastedNormalPuzzleUnlocked = saveData.lastedNormalPuzzleUnlocked;
            lastedConditionPuzzleUnlocked = saveData.lastedConditionPuzzleUnlocked;
        }
    }
}


