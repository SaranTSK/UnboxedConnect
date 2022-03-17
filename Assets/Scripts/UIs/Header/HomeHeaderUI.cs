using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using TMPro;
using Unboxed.Utility;

namespace Unboxed.UI
{
    public class HomeHeaderUI : MonoBehaviour, IShowable, IMuteable
    {
        [SerializeField] private Transform _homeIcon;
        [SerializeField] private Transform _creditIcon;
        [SerializeField] private Transform _centerTitle;
        [SerializeField] private TextMeshProUGUI _headerText;
        [SerializeField] private Transform _muteIcon;
        [SerializeField] private Transform _unmuteIcon;

        public void Show()
        {
            SetHeaderText($"{Constant.DefaultGemsColor} Gems Level");
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

        public void ShowHome()
        {
            _homeIcon.gameObject.SetActive(false);
            _creditIcon.gameObject.SetActive(true);
            _centerTitle.gameObject.SetActive(true);
        }

        public void ShowCredit()
        {
            _homeIcon.gameObject.SetActive(true);
            _creditIcon.gameObject.SetActive(false);
            _centerTitle.gameObject.SetActive(false);
        }

        public void ShowLevel()
        {
            _homeIcon.gameObject.SetActive(true);
            _creditIcon.gameObject.SetActive(false);
            _centerTitle.gameObject.SetActive(true);
        }

        public void SetHeaderText(string text)
        {
            if(_headerText.TryGetComponent(out TextMeshProUGUI headerText))
            {
                headerText.text = text;
            }
        }
    }
}


