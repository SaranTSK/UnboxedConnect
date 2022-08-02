using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;

namespace Unboxed.UI
{
    public enum Content
    {
        TitleContent,
        HomeContent,
        LevelContent,
        PuzzleContent,
        CreditContent,
    }

    public class ContentUI : MonoBehaviour
    {
        internal TitleContentUI TitleContent => _titleContentUI?.GetComponent<TitleContentUI>();
        internal HomeContentUI HomeContent => _homeContentUI?.GetComponent<HomeContentUI>();
        internal LevelContentUI LevelContent => _levelContentUI?.GetComponent<LevelContentUI>();
        internal PuzzleContentUI PuzzleContent => _puzzleContentUI?.GetComponent<PuzzleContentUI>();
        internal CreditContentUI CreditContent => _creditContentUI?.GetComponent<CreditContentUI>();

        [SerializeField] private Transform _titleContentUI;
        [SerializeField] private Transform _homeContentUI;
        [SerializeField] private Transform _levelContentUI;
        [SerializeField] private Transform _puzzleContentUI;
        [SerializeField] private Transform _creditContentUI;

        private Transform _currentContent;

        public void InitContentUI()
        {
            _currentContent = _titleContentUI;
        }

        public void ShowNewContent(Content content)
        {
            if (GetContentUI(content).TryGetComponent(out IShowable newContent))
            {
                newContent.Show();
                _currentContent = GetContentUI(content);
            }
            else
            {
                Debug.LogWarning("Missing content to show!");
            }
        }

        public void HideCurrentContent()
        {
            if (_currentContent != null)
            {
                if (_currentContent.TryGetComponent(out IShowable currentContent))
                {
                    currentContent.Hide();
                    _currentContent = null;
                }
                else
                {
                    Debug.LogWarning("Missing content to hide!");
                }
            }
        }

        public Transform GetContentUI(Content content)
        {
            var contentTransform = content switch
            {
                Content.TitleContent => _titleContentUI,
                Content.HomeContent => _homeContentUI,
                Content.LevelContent => _levelContentUI,
                Content.PuzzleContent => _puzzleContentUI,
                Content.CreditContent => _creditContentUI,
                _ => null
            };

            return contentTransform;
        }
    }
}


