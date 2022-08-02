using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using Unboxed.Manager;

namespace Unboxed.UI
{
    public enum Header
    {
        TitleHeader,
        HomeHeader,
        PuzzleHeader
    }

    public class HeaderUI : MonoBehaviour
    {
        internal TitleHeaderUI TitleHeader => _titleHeaderUI?.GetComponent<TitleHeaderUI>();
        internal HomeHeaderUI HomeHeader => _homeHeaderUI?.GetComponent<HomeHeaderUI>();
        internal PuzzleHeaderUI PuzzleHeader => _puzzleHeaderUI?.GetComponent<PuzzleHeaderUI>();

        [SerializeField] private Transform _titleHeaderUI;
        [SerializeField] private Transform _homeHeaderUI;
        [SerializeField] private Transform _puzzleHeaderUI;

        private Transform _currentHeader;

        public void InitHeaderUI()
        {
            _currentHeader = _titleHeaderUI;
        }

        public void ShowNewHeader(Header header)
        {
            if (GetHeaderUI(header).TryGetComponent(out IShowable newHeader))
            {
                newHeader.Show();
                _currentHeader = GetHeaderUI(header);
            }
            else
            {
                Debug.LogWarning("Missing header to show!");
            }
        }

        public void HideCurrentHeader()
        {
            if (_currentHeader != null)
            {
                if (_currentHeader.TryGetComponent(out IShowable currentHeader))
                {
                    currentHeader.Hide();
                    _currentHeader = null;
                }
                else
                {
                    Debug.LogWarning("Missing header to hide!");
                }
            }
        }

        public Transform GetHeaderUI(Header header)
        {
            var headerTransform = header switch
            {
                Header.TitleHeader => _titleHeaderUI,
                Header.HomeHeader => _homeHeaderUI,
                Header.PuzzleHeader => _puzzleHeaderUI,
                _ => null
            };

            return headerTransform;
        }

        public void OnClickCreditButton()
        {
            switch (GameManager.Instance.GameState)
            {
                case GameState.TitleState:
                    if (_currentHeader.TryGetComponent(out TitleHeaderUI titleHeader))
                    {
                        titleHeader.ShowCredit();
                        UIManager.Instance.ChangeContent(Content.CreditContent);
                    }
                    break;

                case GameState.HomeState:
                    if (_currentHeader.TryGetComponent(out HomeHeaderUI homeHeader))
                    {
                        homeHeader.ShowCredit();
                        UIManager.Instance.ChangeContent(Content.CreditContent);
                        UIManager.Instance.ChangeFooter(Footer.NoFooter);
                    }
                    break;
            }
        }

        public void OnClickHomeButton()
        {
            switch (GameManager.Instance.GameState)
            {
                case GameState.TitleState:
                    if (_currentHeader.TryGetComponent(out TitleHeaderUI titleHeader))
                    {
                        titleHeader.ShowTitle();
                        UIManager.Instance.ChangeContent(Content.TitleContent);
                    }
                    break;

                case GameState.HomeState:
                case GameState.LevelState:
                    if (_currentHeader.TryGetComponent(out HomeHeaderUI homeHeader))
                    {
                        homeHeader.ShowHome();
                        GameManager.Instance.SetGameState(GameState.HomeState);
                        UIManager.Instance.ChangeContent(Content.HomeContent);
                        UIManager.Instance.ChangeFooter(Footer.HomeFooter);
                    }
                    break;

                case GameState.PuzzleState:
                //case GameState.CompleteState: //TODO: For debug
                    LevelManager.Instance.ExitLevel();
                    SceneLoaderManager.Instance.LoadScene(SceneName.HomeScene);
                    //TODO: Destroy puzzle generator before loading
                    //UIManager.Instance.ChangeContent(Content.LevelContent);
                    break;
            }
        }

        public void OnClickMuteButton()
        {
            if (_currentHeader != null)
            {
                if (_currentHeader.TryGetComponent(out IMuteable currentHeader))
                {
                    currentHeader.Unmute();
                    SoundManager.Instance.UnmuteSound();
                }
                else
                {
                    Debug.LogWarning("Missing header to mute!");
                }
            }
        }

        public void OnClickUnmuteButton()
        {
            if (_currentHeader != null)
            {
                if (_currentHeader.TryGetComponent(out IMuteable currentHeader))
                {
                    currentHeader.Mute();
                    SoundManager.Instance.MuteSound();
                }
                else
                {
                    Debug.LogWarning("Missing header to unmute!");
                }
            }
        }

        public void CheckSoundMuted()
        {
            if (_currentHeader.TryGetComponent(out IMuteable currentHeader))
            {
                if (SoundManager.Instance.IsMute)
                {
                    currentHeader.Mute();
                }
                else
                {
                    currentHeader.Unmute();
                }
            }
            else
            {
                Debug.LogWarning("Missing header to check!");
            }
        }
    }
}


