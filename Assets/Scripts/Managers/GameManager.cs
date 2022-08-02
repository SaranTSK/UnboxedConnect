using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Pattern;
using Unboxed.SaveGame;

namespace Unboxed.Manager
{
    public enum GameState
    {
        TitleState,
        HomeState,
        LevelState,
        PuzzleState,
        CompleteState,
        PopupState
    }

    public enum GemsColor
    {
        Empty,
        Red,
        Orange,
        Yellow,
        Green,
        Turquoise,
        Navy,
        Violet,
        Pink,
        Black,
        White
    }

    public class GameManager : Singleton<GameManager>
    {
        public GameState GameState => _gameState;
        public GameState PreviousGameState => _previousGameState;
        public SaveData SaveData => _savedata;

        private GameState _gameState;
        private GameState _previousGameState;
        private SaveData _savedata;

        protected override void Awake()
        {
            base.Awake();
        }

        private void Start()
        {
            //TODO: For TitleScene
            SceneLoaderManager.Instance.LoadScene(SceneName.TitleScene);

            CreateSaveData();
        }

        public void SetGameState(GameState gameState)
        {
            _previousGameState = _gameState;
            _gameState = gameState;
        }

        public void SavePurchasedPack(GemsColor gemsColor)
        {
            _savedata.purchasedPack[(int)gemsColor] = true;
            SaveGameData();
        }

        public void SaveLastedNormalPuzzle(GemsColor gemsColor, int puzzleIndex)
        {
            _savedata.lastedNormalPuzzleUnlocked[(int)gemsColor] = puzzleIndex;
            SaveGameData();
        }

        public void SaveLastedConditionPuzzle(GemsColor gemsColor, int puzzleIndex)
        {
            _savedata.lastedConditionPuzzleUnlocked[(int)gemsColor] = puzzleIndex;
            SaveGameData();
        }

        private void CreateSaveData()
        {
            if(SaveManager.LoadPlayerData() == null)
            {
                Debug.Log("Create new save data");
                _savedata = new SaveData();
                SaveGameData();
            }
            else
            {
                _savedata = SaveManager.LoadPlayerData();
            }
        }

        private void SaveGameData()
        {
            SaveManager.SavePlayerData(_savedata);
        }
    }
}


