using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Pattern;
using Unboxed.UI;
using Unboxed.Utility;
using Unboxed.Puzzle;
using Unboxed.Scriptable;
using Unboxed.Player;
using Unboxed.Asset;

namespace Unboxed.Manager
{
    public class LevelManager : Singleton<LevelManager>
    {
        //public PuzzleScriptable _puzzle;
        public GemsColor GemsColor => _gameLevel.gemsColor;
        public GameMode GameMode => _gameLevel.gameMode;
        public int Level => _gameLevel.level;

        [SerializeField] private GameObject _generatorPref;
        [SerializeField] private GameObject _player;

        private GameLevel _gameLevel;
        private PlayerManager _playerManager;

        //private delegate void LoadLevelDelegate();

        protected override void Awake()
        {
            base.Awake();
            _playerManager = _player.GetComponent<PlayerManager>();
        }

        protected void Start()
        {
            InitLevelManager();

            //TODO: For TestScene
            //InitLevel();
        }

        private void InitLevelManager()
        {
            _gameLevel =  new GameLevel()
            {
                gemsColor = Constant.DefaultGemsColor,
                gameMode = Constant.DefaultGameMode,
                tier = Constant.DefaultTier,
                level = 11
            };
        }

        public void SetGemsColor(GemsColor gemsColor)
        {
            _gameLevel.gemsColor = gemsColor;
        }

        public void SetGameMode(GameMode gameMode)
        {
            _gameLevel.gameMode = gameMode;
        }

        public void SetTier(int tier)
        {
            _gameLevel.tier = tier;
        }

        public void SetLevel(int level)
        {
            _gameLevel.level = level;
        }

        public void SetGameLevel(GameLevel gameLevel)
        {
            SetGemsColor(gameLevel.gemsColor);
            SetGameMode(gameLevel.gameMode);
            SetTier(gameLevel.tier);
            SetLevel(gameLevel.level);
        }

        public void InitLevel()
        {
            var player = Instantiate(_player, transform);
            _playerManager = player.GetComponent<PlayerManager>();

            var generator = Instantiate(_generatorPref, transform);

            if (generator.TryGetComponent(out PuzzleGenerator puzzleGenerator))
            {
                PuzzleScriptable puzzle = GetPuzzleDataByGameLevel(_gameLevel);
                puzzleGenerator.InitPuzzleGenerator(puzzle);
                _playerManager.InitPlayer(puzzle);

                //TODO: Old controller
                //_playerManager.PuzzleController.BindPuzzleGenerator(puzzleGenerator);

                //TODO: New abstact controller
                _playerManager.AbstactPuzzleController.BindPuzzleGenerator(puzzleGenerator);

                //TODO: For TitleScene
                UIManager.Instance.GetHeaderComponent<PuzzleHeaderUI>(Header.PuzzleHeader).SetTitle(puzzle.puzzleName);
                UIManager.Instance.GetContentComponenet<PuzzleContentUI>(Content.PuzzleContent).InitConditionPanel(_gameLevel.gameMode);
                UIManager.Instance.GetContentComponenet<PuzzleContentUI>(Content.PuzzleContent).LoadContent(puzzle);

                StartCoroutine(puzzleGenerator.SpawningDots(1.5f));
                //puzzleGenerator.SpawningDots();
            }
        }

        public void OnComplete()
        {
            if (transform.GetChild(1).TryGetComponent(out PuzzleGenerator puzzleGenerator))
            {
                puzzleGenerator.OnComplete();
            }
        }

        //TODO: Using levelLoader instead!
        public IEnumerator LoadLevel()
        {
            _playerManager.PlayerController.DisableInput();
            _playerManager.SetPlayerState(PlayerState.Idle);

            if (IsHaveNextLevel())
            {
                var generator = Instantiate(_generatorPref, transform);
                generator.transform.localPosition = new Vector3(Screen.width / 100, 0, 0);

                if (generator.TryGetComponent(out PuzzleGenerator puzzleGenerator))
                {
                    var gameLevel = new GameLevel()
                    {
                        gemsColor = _gameLevel.gemsColor,
                        gameMode = _gameLevel.gameMode,
                        tier = _gameLevel.tier,
                        level = _gameLevel.level + 1
                    };

                    PuzzleScriptable puzzle = GetPuzzleDataByGameLevel(gameLevel);
                    puzzleGenerator.InitPuzzleGenerator(puzzle);
                    //TODO: Old controller
                    //_playerManager.PuzzleController.BindPuzzleGenerator(puzzleGenerator);

                    //TODO: New abstact controller
                    _playerManager.AbstactPuzzleController.BindPuzzleGenerator(puzzleGenerator);

                    UIManager.Instance.GetHeaderComponent<PuzzleHeaderUI>(Header.PuzzleHeader).SetTitle(puzzle.puzzleName);
                    UIManager.Instance.GetHeaderComponent<PuzzleHeaderUI>(Header.PuzzleHeader).HideTitle();
                    UIManager.Instance.GetContentComponenet<PuzzleContentUI>(Content.PuzzleContent).LoadContent(puzzle);
                    UIManager.Instance.GetContentComponenet<PuzzleContentUI>(Content.PuzzleContent).Hide();

                    SetGameLevel(gameLevel);
                    ChangeLevel();
                    StartCoroutine(puzzleGenerator.SpawningDots(1f));
                }

                yield return new WaitForSecondsRealtime(1f);

                UIManager.Instance.GetHeaderComponent<PuzzleHeaderUI>(Header.PuzzleHeader).ShowTitle();
                UIManager.Instance.GetContentComponenet<PuzzleContentUI>(Content.PuzzleContent).Show();

                DestroyPreviousLevel();
                LoadPlayer();
            }
            else
            {
                //TODO: Play unlock tier condition or animation
                Debug.LogWarning("Reaching max level!!!");
                yield return new WaitForSecondsRealtime(1f);
                SceneLoaderManager.Instance.LoadScene(SceneName.HomeScene);
                ExitLevel();
            }
        }

        public void RestartLevel()
        {
            _playerManager.Restart();
        }

        public void ExitLevel()
        {
            _playerManager.Exit();
            DestroyPreviousLevel();
        }

        public void SavegameData()
        {
            if (IsLevelHasData())
            {
                switch (_gameLevel.gameMode)
                {
                    case GameMode.Normal:
                        if(_gameLevel.level + 1 > GameManager.Instance.SaveData.lastedNormalPuzzleUnlocked[(int)_gameLevel.gemsColor])
                        {
                            GameManager.Instance.SaveLastedNormalPuzzle(_gameLevel.gemsColor, _gameLevel.level + 1);
                        }
                        break;
                    case GameMode.Condition:
                        if(_gameLevel.level + 1 > GameManager.Instance.SaveData.lastedConditionPuzzleUnlocked[(int)_gameLevel.gemsColor])
                        {
                            GameManager.Instance.SaveLastedConditionPuzzle(_gameLevel.gemsColor, _gameLevel.level + 1);
                        }
                        break;
                }
            }
        }

        private bool IsLevelHasData()
        {
            return _gameLevel.level < DataAsset.Instance.GetAssetsLenghtByGemstone(_gameLevel.gemsColor);
        }

        private bool IsHaveNextLevel()
        {
            return _gameLevel.level + 1 < Constant.MaxLevelPerTier && _gameLevel.level < GetDataLenghtByGemstone(_gameLevel);
        }

        private void ChangeLevel()
        {
            AnimationUtility.MovingGameObjectToLeft(transform.GetChild(1));
            AnimationUtility.MovingGameObjectToCenter(transform.GetChild(2));
        }

        private void DestroyPreviousLevel()
        {
            if (transform.GetChild(1).TryGetComponent(out PuzzleGenerator puzzleGenerator))
            {
                puzzleGenerator.ExitGenerator();
                puzzleGenerator.Destroy();
            }
        }

        private void LoadPlayer()
        {
            PuzzleScriptable puzzle = GetPuzzleDataByGameLevel(_gameLevel);
            _playerManager.UpdatePlayer(puzzle);

            GameManager.Instance.SetGameState(GameState.PuzzleState);
        }

        private PuzzleScriptable GetPuzzleDataByGameLevel(GameLevel gameLevel)
        {
            var level = gameLevel.level + (gameLevel.tier * Constant.MaxLevelPerTier);
            return DataAsset.Instance.GetAssetByGemsColor(gameLevel.gemsColor, level - 1);
        }

        private int GetDataLenghtByGemstone(GameLevel gameLevel)
        {
            return DataAsset.Instance.GetAssetsLenghtByGemstone(gameLevel.gemsColor);
        }
    }

    public class GameLevel
    {
        public GemsColor gemsColor;
        public GameMode gameMode;
        public int tier;
        public int level;
    }
}


