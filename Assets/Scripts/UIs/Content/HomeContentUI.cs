using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using Unboxed.Utility;
using Unboxed.Manager;

namespace Unboxed.UI
{
    public class HomeContentUI : MonoBehaviour, IShowable
    {
        private Transform _currentGemsLevel;

        public void Show()
        {
            if(GameManager.Instance.PreviousGameState == GameState.TitleState)
            {
                ChangeGemsSelected(Constant.DefaultGemsColor);
            }
            
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void ChangeGemsSelected(GemsColor gemsColor)
        {
            if(_currentGemsLevel != null)
            {
                if (_currentGemsLevel.TryGetComponent(out GemsButton gemsButton))
                {
                    gemsButton.Deselect();
                    _currentGemsLevel = transform.GetChild((int)gemsColor);
                }
            }
            else
            {
                Debug.LogWarning("Missing current gems level!");

                _currentGemsLevel = transform.GetChild((int)Constant.DefaultGemsColor);

                if (_currentGemsLevel.TryGetComponent(out GemsButton gemsButton))
                {
                    gemsButton.Select();
                    _currentGemsLevel = transform.GetChild((int)Constant.DefaultGemsColor);
                }
            }
            
        }
    }
}


