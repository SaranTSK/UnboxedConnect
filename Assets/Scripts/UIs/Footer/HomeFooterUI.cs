using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using Unboxed.Manager;
using Unboxed.Utility;

namespace Unboxed.UI
{
    public class HomeFooterUI : MonoBehaviour, IShowable
    {
        [SerializeField] private Transform _enterButton;
        [SerializeField] private Transform _singlePurchaseButton;
        [SerializeField] private Transform _miniPurchaseButton;
        [SerializeField] private Transform _fullPurchaseButton;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void CheckLevelPurchased(GemsColor gemsColor)
        {
            var isPurchase = GameManager.Instance.SaveData.purchasedPack[(int)gemsColor];

            _enterButton.gameObject.SetActive(isPurchase);
            _singlePurchaseButton.gameObject.SetActive(!isPurchase);
            _miniPurchaseButton.gameObject.SetActive(!isPurchase);
            _fullPurchaseButton.gameObject.SetActive(!isPurchase);
        }

        public void OnClickEnterButton()
        {
            Debug.Log($"Enter button: {GameManager.Instance.GameState}");
            switch(GameManager.Instance.GameState)
            {
                case GameState.HomeState:
                    GameManager.Instance.SetGameState(GameState.LevelState);
                    UIManager.Instance.ChangeContent(Content.LevelContent);
                    UIManager.Instance.GetHeaderComponent<HomeHeaderUI>(Header.HomeHeader).ShowLevel();
                    UIManager.Instance.GetContentComponenet<LevelContentUI>(Content.LevelContent).EnterLevelContentUI(LevelManager.Instance.GemsColor);
                    break;

                case GameState.LevelState:
                    GameManager.Instance.SetGameState(GameState.PuzzleState);
                    SceneLoaderManager.Instance.LoadScene(SceneName.PuzzleScene);
                    LevelManager.Instance.InitLevel();
                    break;
            }
        }

        public void OnClickSinglePackButton()
        {
            Debug.Log("Show single pack purchase");
        }

        public void OnClickMiniPackButton()
        {
            Debug.Log("Show mini pack purchase");
        }

        public void OnClickFullPackButton()
        {
            Debug.Log("Show full pack purchase");
        }
    }
}


