using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Unboxed.Interface;
using Unboxed.Manager;
using Unboxed.Utility;

namespace Unboxed.UI
{
    public class LevelButton : MonoBehaviour, IPointerClickHandler, ISelectable
    {
        private TextMeshProUGUI _levelText;
        private Image _levelImage;

        private Sprite _levelSprite;
        private Sprite _lockSprite;

        private int _level;
        private bool _isLocked;
        private bool _isSelected = false;

        public void InitLevelButton(int level, bool isLocked, Sprite levelSprite, Sprite lockSprite)
        {
            _level = level;
            _isLocked = isLocked;
            _levelSprite = levelSprite;
            _lockSprite = lockSprite;

            _levelText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
            _levelText.text = $"{level}";

            _levelImage = gameObject.GetComponent<Image>();
            UpdateButtonColor();

            ShowLevelButton();
        }

        private void ShowLevelButton()
        {
            _levelText.gameObject.SetActive(!_isLocked);
            _levelImage.sprite = _isLocked ? _lockSprite : _levelSprite;
            UpdateButtonColor();
        }

        public void LockLevelButton()
        {
            _isLocked = true;
            ShowLevelButton();
        }

        public void UnlockLevelButton()
        {
            _isLocked = false;
            ShowLevelButton();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            //TODO: Check clicked level is can play?
            Debug.Log($"{name} Select: {_isSelected} | Lock: {_isLocked}");
            if(!_isSelected && !_isLocked)
            {
                UIManager.Instance.GetContentComponenet<LevelContentUI>(Content.LevelContent).SwitchLevelButton(_level);
                Select();
            }
        }

        public void Select()
        {
            _isSelected = true;
            _levelImage.color = Color.cyan;
            LevelManager.Instance.SetLevel(_level);
        }

        public void Deselect()
        {
            _isSelected = false;
            _levelImage.color = Color.white;
        }

        private void UpdateButtonColor()
        {
            _levelImage.color = _isLocked ? Color.gray : Color.white;
        }
    }
}


