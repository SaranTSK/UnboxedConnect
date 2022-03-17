using System.Collections;
using System.Collections.Generic;
using Unboxed.Interface;
using Unboxed.Manager;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Unboxed.UI
{
    public class LevelModeButton : MonoBehaviour, IPointerClickHandler, ISelectable
    {
        [SerializeField] private GameMode _gameMode;

        private bool _isSelected = false;

        public void InitLevelModeButton(GameMode gameMode)
        {
            if (_gameMode == gameMode)
            {
                Select();
            }
            else
            {
                Deselect();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if(!_isSelected)
            {
                Select();
                UIManager.Instance.GetContentComponenet<LevelContentUI>(Content.LevelContent).SwitchGameModePanel(_gameMode);
            }
        }

        public void Select()
        {
            if(TryGetComponent(out Image image))
            {
                image.color = Color.white;
            }
            _isSelected = true;
        }

        public void Deselect()
        {
            if (TryGetComponent(out Image image))
            {
                image.color = Color.gray;
            }
            _isSelected = false;
        }
    }
}

