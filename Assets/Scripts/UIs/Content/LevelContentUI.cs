using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using Unboxed.Manager;
using Unboxed.Utility;

namespace Unboxed.UI
{
    public enum GameMode
    {
        Normal,
        Condition,
    }

    public class LevelContentUI : MonoBehaviour, IShowable
    {
        [SerializeField] private Transform _levelModePanel;
        [SerializeField] private Transform _levelNormalPanelParent;
        [SerializeField] private Transform _levelConditionPanelParent;
        [SerializeField] private Transform _levelPanel;
        [SerializeField] private Transform _levelLeftPanel;
        [SerializeField] private Transform _levelRightPanel;

        private int _tier;
        private int _level;
        private GemsColor _gemsColor;
        private GameMode _gameMode;

        private void SetGameMode(GameMode gameMode)
        {
            _gameMode = gameMode;
            LevelManager.Instance.SetGameMode(gameMode);
        }

        private void SetTier(int tier)
        {
            _tier = tier;
            LevelManager.Instance.SetTier(tier);
        }

        private void SetLevel(int level)
        {
            _level = level;
            LevelManager.Instance.SetLevel(level);
        }

        private void SetGemsColor(GemsColor gemsColor)
        {
            _gemsColor = gemsColor;
            LevelManager.Instance.SetGemsColor(gemsColor);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
            DeselectLevelButton(_level);
        }

        public void EnterLevelContentUI(GemsColor gemsColor)
        {
            SetGemsColor(gemsColor);
            SetGameMode(GameMode.Normal);
            SetTier(GetLastedTier());
            SetLevel(GetLastedLevel());

            Debug.Log($"Color: {_gemsColor} | Mode: {_gameMode} | Tier: {_tier} | Level: {_level}");

            if (_levelNormalPanelParent.childCount == 0)
            {
                CreateLevelNormalPanel(gemsColor);
            }

            if(_levelConditionPanelParent.childCount == 0)
            {
                CreateLevelConditionPanel(gemsColor);
            }

            LoadLevelNormalPanel(gemsColor);
            LoadLevelConditionPanel(gemsColor);

            InitGameModeButton();
            ShowLastedParentPanel();
            ShowLastedLevelPanel();
            ShowLastedLevelButton();
            ShowLeftAndRightButton();
            ShowEnterButton();
        }

        private void InitGameModeButton()
        {
            ShowGameModeButton(GameMode.Normal);
            HideGameModeButton(GameMode.Condition);
        }

        // ---------- LevelPanel Handerler ----------

        private void CreateLevelNormalPanel(GemsColor gemsColor)
        {
            var maxNormalLevel = GameManager.Instance.SaveData.lastedNormalPuzzleUnlocked[(int)gemsColor];

            for (int tier = 0; tier < Constant.MaxTier; tier++)
            {
                var levelPanel = Instantiate(_levelPanel, transform);
                levelPanel.name = $"LevelPanel_{tier}";
                levelPanel.SetParent(_levelNormalPanelParent);

                if (levelPanel.TryGetComponent(out LevelPanel panel))
                {
                    panel.InitLevelPanel(tier, maxNormalLevel);
                }

                SetPanelPosition(levelPanel, tier);
            }
        }

        private void LoadLevelNormalPanel(GemsColor gemsColor)
        {
            var maxNormalLevel = GameManager.Instance.SaveData.lastedNormalPuzzleUnlocked[(int)gemsColor];

            for (int tier = 0; tier < Constant.MaxTier; tier++)
            {
                if (_levelNormalPanelParent.GetChild(tier).TryGetComponent(out LevelPanel levelPanel))
                {
                    levelPanel.ChangeLevelData(tier, maxNormalLevel);
                }
            }
        }

        private void CreateLevelConditionPanel(GemsColor gemsColor)
        {
            var maxConditionLevel = GameManager.Instance.SaveData.lastedConditionPuzzleUnlocked[(int)gemsColor];

            for (int tier = 0; tier < Constant.MaxTier; tier++)
            {
                var levelPanel = Instantiate(_levelPanel, transform);
                levelPanel.name = $"LevelPanel_{tier}";
                levelPanel.SetParent(_levelConditionPanelParent);

                if (levelPanel.TryGetComponent(out LevelPanel panel))
                {
                    panel.InitLevelPanel(tier, maxConditionLevel);
                }

                SetPanelPosition(levelPanel, tier);
            }
        }

        private void LoadLevelConditionPanel(GemsColor gemsColor)
        {
            var maxConditionLevel = GameManager.Instance.SaveData.lastedConditionPuzzleUnlocked[(int)gemsColor];

            for (int tier = 0; tier < Constant.MaxTier; tier++)
            {
                if (_levelConditionPanelParent.GetChild(tier).TryGetComponent(out LevelPanel levelPanel))
                {
                    levelPanel.ChangeLevelData(tier, maxConditionLevel);
                }
            }
        }

        private bool IsPanelHasToActive(int tier)
        {
            return tier == _tier;
        }

        private void SetPanelPosition(Transform levelPanel, int tier)
        {
            if (tier < _tier)
            {
                levelPanel.localPosition = new Vector3(-Constant.CanvasWidthRef, levelPanel.localPosition.y);
            }
            else if (tier > _tier)
            {
                levelPanel.localPosition = new Vector3(Constant.CanvasWidthRef, levelPanel.localPosition.y);
            }
            else
            {
                levelPanel.localPosition = Vector2.zero;
            }

            levelPanel.gameObject.SetActive(IsPanelHasToActive(tier));
        }

        private void ShowLastedParentPanel()
        {
            HideParentPanel(GameMode.Condition);
            ShowParentPanel(GameMode.Normal);
        }

        private void ShowLastedLevelPanel()
        {
            for (int tier = 0; tier < Constant.MaxTier; tier++)
            {
                SetPanelPosition(_levelNormalPanelParent.GetChild(tier), tier);
                SetPanelPosition(_levelConditionPanelParent.GetChild(tier), tier);
            }
        }

        private Transform GetParentPanel(GameMode gameMode)
        {
            var parentPanel = gameMode switch
            {
                GameMode.Normal => _levelNormalPanelParent,
                GameMode.Condition => _levelConditionPanelParent,
                _ => null
            };

            return parentPanel;
        }

        private Transform GetLevelPanelByTier(Transform panel, int tier)
        {
            return panel.GetChild(tier);
        }

        // ---------- Left&Right Button Handerler ----------

        public void OnClickLeftButton()
        {
            if (_tier > 0)
            {
                var parent = GetParentPanel(_gameMode);
                GetNearestLeftPanel(parent, _tier).gameObject.SetActive(true);
                MovingPanelOutRight(_tier);
                MovingPanelIn(_tier - 1);
                DeselectLevelButton(_level);

                SetTier(_tier - 1);

                ShowLastedLevelButton();
                ShowLeftAndRightButton();
                ShowEnterButton();
            }
        }

        public void OnClickRightButton()
        {
            if (_tier < 6)
            {
                var parent = GetParentPanel(_gameMode);
                GetNearestRightPanel(parent, _tier).gameObject.SetActive(true);
                MovingPanelOutLeft(_tier);
                MovingPanelIn(_tier + 1);
                DeselectLevelButton(_level);

                SetTier(_tier + 1);

                ShowLastedLevelButton();
                ShowLeftAndRightButton();
                ShowEnterButton();
            }
        }

        private void MovingPanelOutLeft(int targetPanel)
        {
            AnimationUtility.MovingUIToLeft(_levelNormalPanelParent.GetChild(targetPanel));
            AnimationUtility.MovingUIToLeft(_levelConditionPanelParent.GetChild(targetPanel));

            StartCoroutine(_levelNormalPanelParent.GetChild(targetPanel).GetComponent<LevelPanel>().HidePanel());
            StartCoroutine(_levelConditionPanelParent.GetChild(targetPanel).GetComponent<LevelPanel>().HidePanel());

        }

        private void MovingPanelOutRight(int targetPanel)
        {
            AnimationUtility.MovingUIToRight(_levelNormalPanelParent.GetChild(targetPanel));
            AnimationUtility.MovingUIToRight(_levelConditionPanelParent.GetChild(targetPanel));

            StartCoroutine(_levelNormalPanelParent.GetChild(targetPanel).GetComponent<LevelPanel>().HidePanel());
            StartCoroutine(_levelConditionPanelParent.GetChild(targetPanel).GetComponent<LevelPanel>().HidePanel());
        }

        private void MovingPanelIn(int targetPanel)
        {
            AnimationUtility.MovingUIToCenter(_levelNormalPanelParent.GetChild(targetPanel));
            AnimationUtility.MovingUIToCenter(_levelConditionPanelParent.GetChild(targetPanel));
        }

        private Transform GetNearestLeftPanel(Transform parent, int tier)
        {
            return parent.GetChild(tier - 1);
        }

        private Transform GetNearestRightPanel(Transform parent, int tier)
        {
            return parent.GetChild(tier + 1);
        }

        private void ShowLeftAndRightButton()
        {
            _levelLeftPanel.gameObject.SetActive(!IsMinTier());
            _levelRightPanel.gameObject.SetActive(!IsMaxTier());
        }

        private bool IsMinTier()
        {
            return _tier == 0;
        }

        private bool IsMaxTier()
        {
            return _tier == Constant.MaxTier - 1;
        }

        // ---------- GameModeButton Handerler ----------

        public void SwitchGameModePanel(GameMode gameMode)
        {
            if (gameMode != _gameMode)
            {
                HideParentPanel(_gameMode);
                HideGameModeButton(_gameMode);
                DeselectLevelButton(_level);

                ShowParentPanel(gameMode);
                ShowGameModeButton(gameMode);

                SetGameMode(gameMode);
                ShowLastedLevelButton();
                ShowEnterButton();
            }
        }

        private void ShowParentPanel(GameMode gameMode)
        {
            GetParentPanel(gameMode).gameObject.SetActive(true);
            GetLevelPanelByTier(GetParentPanel(gameMode), _tier).gameObject.SetActive(true);
        }

        private void HideParentPanel(GameMode gameMode)
        {
            GetParentPanel(gameMode).gameObject.SetActive(false);
            GetLevelPanelByTier(GetParentPanel(gameMode), _tier).gameObject.SetActive(false);
        }

        private void ShowGameModeButton(GameMode gameMode)
        {
            _levelModePanel.GetChild((int)gameMode).GetComponent<LevelModeButton>().Select();
        }

        private void HideGameModeButton(GameMode gameMode)
        {
            _levelModePanel.GetChild((int)gameMode).GetComponent<LevelModeButton>().Deselect();
        }

        // ---------- LevelButton Handerler ----------

        public void SwitchLevelButton(int level)
        {
            DeselectLevelButton(_level);
            SelectLevelButton(level);
        }

        private void ShowLastedLevelButton()
        {
            if(GetLastedLevel() > 0)
            {
                var level = GetLastedLevel();
                SelectLevelButton(level);
            }
            else
            {
                SetLevel(0);
            }
        }

        private void SelectLevelButton(int level)
        {
            var levelPerSide = GetLevelPerSide(level);

            GetLevelGridByLevel(level).GetLevelButton(levelPerSide).Select();
            SetLevel(level);
        }

        private void DeselectLevelButton(int level)
        {
            if(level > 0)
            {
                var levelPerSide = GetLevelPerSide(level);
                GetLevelGridByLevel(level).GetLevelButton(levelPerSide).Deselect();
            }
        }

        // ---------- Get() Handerler ----------

        private int GetLastedTier()
        {
            var lastedTier = GetLastedLevelByGameMode(_gameMode);

            return lastedTier / Constant.MaxLevelPerTier;
        }

        private int GetLastedLevel()
        {
            var lastedLevel = GetLastedLevelByGameMode(_gameMode);

            return ((_tier + 1) * Constant.MaxLevelPerTier < lastedLevel) ? Constant.MaxLevelPerTier : lastedLevel - (_tier * Constant.MaxLevelPerTier);
        }

        private LevelGrid GetLevelGridByLevel(int level)
        {
            var panel = GetParentPanel(_gameMode);
            var levelPanel = panel.GetChild(_tier).GetComponent<LevelPanel>();
            var levelGrid = (level < Constant.MaxLevelPerPanel) ? levelPanel.GetLevelGrid(LevelGrid.PanelSide.Left) : levelPanel.GetLevelGrid(LevelGrid.PanelSide.Right);

            return levelGrid.GetComponent<LevelGrid>();
        }

        private int GetLevelPerSide(int level)
        {
            return (level < Constant.MaxLevelPerPanel) ? level : level - Constant.MaxLevelPerPanel;
        }

        private int GetLastedLevelByGameMode(GameMode gameMode)
        {
            var lastedLevel = gameMode switch
            {
                GameMode.Normal => GameManager.Instance.SaveData.lastedNormalPuzzleUnlocked[(int)_gemsColor],
                GameMode.Condition => GameManager.Instance.SaveData.lastedConditionPuzzleUnlocked[(int)_gemsColor],
                _ => 0,
            };

            return lastedLevel;
        }

        // ---------- EnterButton Handerler ----------

        private void ShowEnterButton()
        {
            var homeFooter = UIManager.Instance.GetFooterComponenet<HomeFooterUI>(Footer.HomeFooter);

            if(IsTierLocked())
            {
                homeFooter.Hide();
            }
            else
            {
                homeFooter.Show();
            }
        }

        private bool IsTierLocked()
        {
            var lastedLevel = GetLastedLevelByGameMode(_gameMode);

            Debug.Log($"Result: {_gameMode} {lastedLevel} {_tier * Constant.MaxLevelPerTier} {lastedLevel > 0}");

            return lastedLevel <= _tier * Constant.MaxLevelPerTier;
        }
    }
}

