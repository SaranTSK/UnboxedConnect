using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;

namespace Unboxed.UI
{
    public enum Footer
    {
        HomeFooter,
        NoFooter
    }

    public class FooterUI : MonoBehaviour
    {
        internal HomeFooterUI HomeFooter => _homeFooterUI?.GetComponent<HomeFooterUI>();

        [SerializeField] private Transform _homeFooterUI;

        private Transform _currentFooter;

        public void InitFooterUI()
        {
            _currentFooter = null;
        }

        public void ShowNewFooter(Footer footer)
        {
            if (GetFooterUI(footer).TryGetComponent(out IShowable newContent))
            {
                newContent.Show();
                _currentFooter = GetFooterUI(footer);
            }
            else
            {
                Debug.LogWarning("Missing footer to show!");
            }
        }

        public void HideCurrentFooter()
        {
            if (_currentFooter != null)
            {
                if (_currentFooter.TryGetComponent(out IShowable currentContent))
                {
                    currentContent.Hide();
                    _currentFooter = null;
                }
                else
                {
                    Debug.LogWarning("Missing footer to hide!");
                }
            }
        }

        private Transform GetFooterUI(Footer footer)
        {
            var footerTransform = footer switch
            {
                Footer.HomeFooter => _homeFooterUI,
                _ => null
            };

            return footerTransform;
        }
    }
}


