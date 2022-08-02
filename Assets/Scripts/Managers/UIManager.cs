using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Pattern;
using Unboxed.UI;

namespace Unboxed.Manager
{
    public enum MainUI
    {
        Header,
        Content,
        Footer
    }

    public class UIManager : Singleton<UIManager>
    {
        [SerializeField] private Transform _headerUI;
        [SerializeField] private Transform _contentUI;
        [SerializeField] private Transform _footerUI;

        protected override void Awake()
        {
            base.Awake();
        }

        protected void Start()
        {
            InitUIManager();
        }

        private void InitUIManager()
        {
            if (_headerUI.TryGetComponent(out HeaderUI headerUI))
            {
                headerUI.InitHeaderUI();
            }
            else
            {
                Debug.LogWarning("Missing HeaderUI!");
            }

            if (_contentUI.TryGetComponent(out ContentUI contentUI))
            {
                contentUI.InitContentUI();
            }
            else
            {
                Debug.LogWarning("Missing ContentUI!");
            }

            if (_footerUI.TryGetComponent(out FooterUI footerUI))
            {
                footerUI.InitFooterUI();
            }
            else
            {
                Debug.LogWarning("Missing FooterUI!");
            }
        }

        public void ChangeContent(Content content)
        {
            if (_contentUI.TryGetComponent(out ContentUI contentUI))
            {
                contentUI.HideCurrentContent();
                contentUI.ShowNewContent(content);
            }
        }

        public void ChangeHeader(Header header)
        {
            if(_headerUI.TryGetComponent(out HeaderUI headerUI))
            {
                headerUI.HideCurrentHeader();
                headerUI.ShowNewHeader(header);
                headerUI.CheckSoundMuted();
            }
        }

        public void ChangeFooter(Footer footer)
        {
            if (_footerUI.TryGetComponent(out FooterUI footerUI))
            {
                if (footer != Footer.NoFooter)
                {
                    footerUI.HideCurrentFooter();
                    footerUI.ShowNewFooter(footer);
                }
                else
                {
                    footerUI.HideCurrentFooter();
                }
            }
        }

        private T GetMainUIComponent<T>(MainUI mainUI) where T : Component
        {
            T component = mainUI switch
            {
                MainUI.Header => _headerUI.GetComponent<HeaderUI>() as T,
                MainUI.Content => _contentUI.GetComponent<ContentUI>() as T,
                MainUI.Footer => _footerUI.GetComponent<FooterUI>() as T,
                _ => default
            };

            return component;
        }

        public T GetHeaderComponent<T>(Header header) where T : Component
        {
            HeaderUI headerUI = GetMainUIComponent<HeaderUI>(MainUI.Header);

            T component = header switch
            {
                Header.TitleHeader => headerUI.TitleHeader as T,
                Header.HomeHeader => headerUI.HomeHeader as T,
                Header.PuzzleHeader => headerUI.PuzzleHeader as T,
                _ => default,
            };

            return component;
        }

        public T GetContentComponenet<T>(Content content) where T : Component
        {
            ContentUI contentUI = GetMainUIComponent<ContentUI>(MainUI.Content);

            T component = content switch
            {
                Content.TitleContent => contentUI.TitleContent as T,
                Content.HomeContent => contentUI.HomeContent as T,
                Content.LevelContent => contentUI.LevelContent as T,
                Content.PuzzleContent => contentUI.PuzzleContent as T,
                Content.CreditContent => contentUI.CreditContent as T,
                _ => default
            };

            return component;
        }

        public T GetFooterComponenet<T>(Footer footer) where T : Component
        {
            FooterUI footerUI = GetMainUIComponent<FooterUI>(MainUI.Footer);

            T component = footer switch
            {
                Footer.HomeFooter => footerUI.HomeFooter as T,
                _ => default
            };

            return component;
        }
    }
}


