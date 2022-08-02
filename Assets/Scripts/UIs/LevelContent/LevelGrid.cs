using System.Collections;
using System.Collections.Generic;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.UI
{
    public class LevelGrid : MonoBehaviour
    {
        public enum PanelSide
        {
            Left,
            Right
        }

        [SerializeField] private PanelSide _panelSide;

        private Transform[] _rows;
        private List<Transform> _levels;

        public void InitLevelPanel()
        {
            _rows = new Transform[transform.childCount];
            _levels = new List<Transform>();

            for (int row = 0; row < transform.childCount; row++)
            {
                _rows[row] = transform.GetChild(row);

                for (int column = 0; column < _rows[row].childCount; column++)
                {
                    var level = _rows[row].GetChild(column);
                    level.gameObject.AddComponent<LevelButton>();
                    _levels.Add(level);
                }
            }
        }

        public void InitLevelButton(int tier, int clearedLevel, Sprite levelSprite, Sprite lockSprite)
        {
            for (int level = 0; level < _levels.Count; level++)
            {
                if (_levels[level].TryGetComponent(out LevelButton _levelButton))
                {
                    if(IsTierLocked(clearedLevel, tier))
                    {
                        _levelButton.InitLevelButton(GetLevel(level), true, levelSprite, lockSprite);
                    }
                    else
                    {
                        _levelButton.InitLevelButton(GetLevel(level), IsLevelLocked(GetLevel(level), clearedLevel, tier), levelSprite, lockSprite);
                    }
                }
            }
        }

        public void ChangeLevelButtonAppearance(int tier, int clearedLevel)
        {
            for (int level = 0; level < _levels.Count; level++)
            {
                if (_levels[level].TryGetComponent(out LevelButton _levelButton))
                {
                    if (IsLevelLocked(GetLevel(level), clearedLevel, tier) || IsTierLocked(clearedLevel, tier))
                    {
                        _levelButton.LockLevelButton();
                    }
                    else
                    {
                        _levelButton.UnlockLevelButton();
                    }
                }
            }
        }

        public void ResetLevelButton()
        {
            for (int level = 0; level < _levels.Count; level++)
            {
                if (_levels[level].TryGetComponent(out LevelButton _levelButton))
                {
                    _levelButton.Deselect();
                }
            }
        }

        public LevelButton GetLevelButton(int level)
        {
            int index = (level < 1) ? 0 : level - 1;
            return _levels[index].GetComponent<LevelButton>();
        }

        private bool IsTierLocked(int clearedLevel, int tier)
        {
            //Debug.Log($"Result: {clearedLevel} {tier * Constant.MaxLevelPerTier} is {clearedLevel < tier * Constant.MaxLevelPerTier}");
            return clearedLevel <= tier * Constant.MaxLevelPerTier;
        }

        private bool IsLevelLocked(int level, int clearedLevel, int tier)
        {
            //Debug.Log($"Level {level} | Tier {tier} | Cleared {clearedLevel} is {level > clearedLevel - (tier * Constant.MaxLevelPerTier)}");
            return level > clearedLevel - (tier * Constant.MaxLevelPerTier);
        }

        private int GetLevel(int level)
        {
            return (_panelSide == PanelSide.Left) ? level + 1 : level + Constant.MaxLevelPerPanel + 1;
        }

    }
}

    
