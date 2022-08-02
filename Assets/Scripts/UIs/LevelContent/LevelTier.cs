using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Unboxed.UI
{
    public class LevelTier : MonoBehaviour
    {
        private TextMeshProUGUI _tierText;
        private TextMeshProUGUI _clearedLevelText;

        public void InitLevelTier()
        {
            _tierText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
            _clearedLevelText = transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        }

        public void SetLevelTierText(string tier, int clearedLevel)
        {
            _tierText.text = tier;
            _clearedLevelText.text = $"{clearedLevel}/100";
        }
    }
}


