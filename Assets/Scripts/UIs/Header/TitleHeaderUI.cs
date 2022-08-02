using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using Unboxed.Manager;

namespace Unboxed.UI
{
    public class TitleHeaderUI : MonoBehaviour, IShowable, IMuteable
    {
        [SerializeField] private Transform _homeIcon;
        [SerializeField] private Transform _creditIcon;
        [SerializeField] private Transform _muteIcon;
        [SerializeField] private Transform _unmuteIcon;

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

        public void ShowTitle()
        {
            _homeIcon.gameObject.SetActive(false);
            _creditIcon.gameObject.SetActive(true);
        }

        public void ShowCredit()
        {
            _homeIcon.gameObject.SetActive(true);
            _creditIcon.gameObject.SetActive(false);
        }
    }
}

