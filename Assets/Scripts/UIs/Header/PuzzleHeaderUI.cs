using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using TMPro;
using Unboxed.Manager;

namespace Unboxed.UI
{
    public class PuzzleHeaderUI : MonoBehaviour, IShowable, IMuteable
    {
        [SerializeField] private Transform _muteIcon;
        [SerializeField] private Transform _unmuteIcon;
        [SerializeField] private TextMeshProUGUI _titleText;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Mute()
        {
            _muteIcon.gameObject.SetActive(true);
            _unmuteIcon.gameObject.SetActive(false);
        }

        public void Unmute()
        {
            _muteIcon.gameObject.SetActive(false);
            _unmuteIcon.gameObject.SetActive(true);
        }

        public void OnClickRestart()
        {
            //TODO: Restart current puzzle
            if(GameManager.Instance.GameState == GameState.PuzzleState)
            {
                LevelManager.Instance.RestartLevel();
            }
        }

        public void SetTitle(string text)
        {
            _titleText.text = text;
        }

        public void ShowTitle()
        {
            _titleText.gameObject.SetActive(true);
        }

        public void HideTitle()
        {
            _titleText.gameObject.SetActive(false);
        }
    }
}


