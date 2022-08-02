using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Pattern;
using Unboxed.UI;
using UnityEngine.SceneManagement;

namespace Unboxed.Manager
{
    public enum SceneName
    {
        TitleScene,
        HomeScene,
        PuzzleScene,
        LoadingScene
    }

    public class SceneLoaderManager : Singleton<SceneLoaderManager>
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public void LoadScene(SceneName sceneName)
        {
            StartCoroutine(LoadingScene(sceneName));
        }

        private IEnumerator LoadingScene(SceneName sceneName)
        {
            SceneManager.LoadScene((int)SceneName.LoadingScene);
            yield return new WaitForSecondsRealtime(2f);
            SceneManager.LoadScene((int)sceneName);
            LoadUIScene(sceneName);
        }

        private void LoadUIScene(SceneName sceneName)
        {
            switch(sceneName)
            {
                case SceneName.TitleScene: LoadTitleScene(); break;
                case SceneName.HomeScene: LoadHomeScene(); break;
                case SceneName.PuzzleScene: LoadPuzzleScene(); break;
            }
        }

        private void LoadTitleScene()
        {
            UIManager.Instance.ChangeHeader(Header.TitleHeader);
            UIManager.Instance.ChangeContent(Content.TitleContent);
            UIManager.Instance.ChangeFooter(Footer.NoFooter);

            GameManager.Instance.SetGameState(GameState.TitleState);
        }

        //TODO: Fix this!
        private void LoadHomeScene()
        {
            //var content = (GameManager.Instance.GameState == GameState.PuzzleState) ? Content.LevelContent : Content.HomeContent;
            //var state = (GameManager.Instance.GameState == GameState.PuzzleState) ? GameState.LevelState : GameState.HomeState;

            UIManager.Instance.ChangeHeader(Header.HomeHeader);
            UIManager.Instance.ChangeFooter(Footer.HomeFooter);

            switch(GameManager.Instance.GameState)
            {
                case GameState.TitleState:
                    UIManager.Instance.GetHeaderComponent<HomeHeaderUI>(Header.HomeHeader).ShowHome();
                    UIManager.Instance.ChangeContent(Content.HomeContent);
                    GameManager.Instance.SetGameState(GameState.HomeState);
                    break;

                case GameState.PuzzleState:
                case GameState.CompleteState:
                    UIManager.Instance.GetHeaderComponent<HomeHeaderUI>(Header.HomeHeader).ShowLevel();
                    UIManager.Instance.GetContentComponenet<LevelContentUI>(Content.LevelContent).EnterLevelContentUI(LevelManager.Instance.GemsColor);
                    UIManager.Instance.ChangeContent(Content.LevelContent);
                    GameManager.Instance.SetGameState(GameState.LevelState);
                    break;
            }
        }

        private void LoadPuzzleScene()
        {
            UIManager.Instance.ChangeHeader(Header.PuzzleHeader);
            UIManager.Instance.ChangeContent(Content.PuzzleContent);
            UIManager.Instance.ChangeFooter(Footer.NoFooter);

            GameManager.Instance.SetGameState(GameState.PuzzleState);
        }
    }
}


