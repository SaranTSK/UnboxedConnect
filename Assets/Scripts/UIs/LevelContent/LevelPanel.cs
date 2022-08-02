using System.Collections;
using System.Collections.Generic;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.UI
{
    public class LevelPanel : MonoBehaviour
    {
        [SerializeField] private Transform _levelTierTransform;
        [SerializeField] private Transform _leftLevelTransform;
        [SerializeField] private Transform _rightLevelTransform;

        [SerializeField] private Sprite _levelSprite;
        [SerializeField] private Sprite _lockSprite;

        private LevelTier _levelTier;
        private LevelGrid _levelGrid;

        public IEnumerator HidePanel()
        {
            yield return new WaitForSecondsRealtime(1f);
            gameObject.SetActive(false);
        }

        public void InitLevelPanel(int tier, int clearedLevel)
        {
            if (_levelTierTransform.TryGetComponent(out _levelTier))
            {
                _levelTier.InitLevelTier();
                _levelTier.SetLevelTierText(ConvertIntToTierString(tier), clearedLevel);
            }

            if (_leftLevelTransform.TryGetComponent(out _levelGrid))
            {
                _levelGrid.InitLevelPanel();
                _levelGrid.InitLevelButton(tier, clearedLevel, _levelSprite, _lockSprite);
            }

            if (_rightLevelTransform.TryGetComponent(out _levelGrid))
            {
                _levelGrid.InitLevelPanel();
                _levelGrid.InitLevelButton(tier, clearedLevel, _levelSprite, _lockSprite);
            }
        }

        public void ChangeLevelData(int tier, int clearedLevel)
        {
            if (_levelTierTransform.TryGetComponent(out _levelTier))
            {
                _levelTier.SetLevelTierText(ConvertIntToTierString(tier), clearedLevel);
            }

            if (_leftLevelTransform.TryGetComponent(out _levelGrid))
            {
                _levelGrid.ChangeLevelButtonAppearance(tier, clearedLevel);
            }

            if (_rightLevelTransform.TryGetComponent(out _levelGrid))
            {
                _levelGrid.ChangeLevelButtonAppearance(tier, clearedLevel);
            }
        }

        public Transform GetLevelGrid(LevelGrid.PanelSide panelSide)
        {
            var levelGrid = panelSide switch
            {
                LevelGrid.PanelSide.Left => _leftLevelTransform,
                LevelGrid.PanelSide.Right => _rightLevelTransform,
                _ => null
            };

            return levelGrid;
        }

        private string ConvertIntToTierString(int intTier)
        {
            string stringTier = intTier switch
            {
                0 => "E",
                1 => "D",
                2 => "C",
                3 => "B",
                4 => "A",
                5 => "S",
                _ => ""
            };

            return stringTier;
        }
    }
}