using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;

namespace Unboxed.Puzzle
{
    public class DotSpecial : Dot
    {
        [SerializeField] private GameObject _dotBackground;
        [SerializeField] private GameObject _dotFrame;
        [SerializeField] private List<GameObject> _dotFills;
        [SerializeField] private GameObject _colorPicker;

        protected override void Start()
        {
            base.Start();
            //TODO: Set BG and Frame Color
            SetSpriteColor(_dotBackground, _gemsColor);
            SetSpriteColor(_dotFrame, _gemsColor);
            HideColorPickerParent();
        }

        protected override void EnterDot()
        {
            if (IsEnter())
            {
                //TODO: Enter condition
                Debug.Log($"Enter starter dot {name}");

                if (_filledSlot + 1 == _maxFilledSlot)
                {
                    _dotState = DotState.Filled;
                    _filledSlot = _maxFilledSlot;
                }
                else
                {
                    _filledSlot++;
                }
                //PlayFillAnimation();
            }
        }

        protected override void ExitDot()
        {
            if (IsExit())
            {
                //TODO: Exit condition
                Debug.Log($"Exit starter dot {name}");

                _dotState = DotState.Empty;
                _filledSlot--;
                //ResetDotFillColorByIndex(_filledSlot);
                
                //ResetDotColor();
                //PlayClearAniamtion();
            }
        }

        protected override void CompleteDot()
        {
            if (IsComplete())
            {
                //TODO: Complete condition
                _dotState = DotState.Complete;
                //PlayCompleteAnimation();
            }
        }

        protected override void PlayFillAnimation()
        {
            Debug.Log($"{name} trigger enter");
            //_animator.SetTrigger("Enter");
        }

        protected override void PlayClearAniamtion()
        {
            Debug.Log($"{name} trigger exit");
            //_animator.SetTrigger("Exit");
        }

        protected override void PlayCompleteAnimation()
        {
            Debug.Log($"{name} trigger complete");
            //_animator.SetTrigger("Complete");
        }

        //TODO: 0. Public
        public bool IsColorRepeated(GemsColor gemsColor)
        {
            for (int i = 0; i < _gemsColors.Count; i++)
            {
                if (gemsColor == _gemsColors[i])
                {
                    return true;
                }
            }
            return false;
        }

        public int GetEmptyFillSlotIndex()
        {
            for (int i = 0; i < _gemsColors.Count; i++)
            {
                if (_gemsColors[i] == GemsColor.Empty)
                {
                    return i;
                }
            }
            return -1;
        }

        public int GetFillSlotIndexByGemsColor(GemsColor gemsColor)
        {
            for (int i = 0; i < _gemsColors.Count; i++)
            {
                if (_gemsColors[i] == gemsColor)
                {
                    return i;
                }
            }
            return -1;
        }

        //TODO: 1. Reset _dotFills
        public void ResetDotFillColorByIndex(int index)
        {
            ResetDotColorByIndex(index);
            SetSpriteColor(_dotFills[index], _starterColor);
        }

        public void ResetAllFilledColor()
        {
            for (int i = 0; i < _dotFills.Count; i++)
            {
                OnExit();
                ResetDotFillColorByIndex(i);
            }
        }


        //TODO: 2. Fill _dotFills
        public void SetDotFillColor(int index, GemsColor gemsColor)
        {
            Debug.LogWarning($"{name} count: {_gemsColors.Count} and index: {index}");
            SetSpriteColor(_dotFills[index], gemsColor);

            if(_colorPicker.transform.GetChild(index).TryGetComponent(out ColorSlot colorSlot))
            {
                colorSlot.SetGemsColor(gemsColor);
            }
        }
        //TODO: 3. Animation _dotFills

        //TODO: 4. ColorPicker
        //private void ShowColorPicker()
        //{
        //    if(_maxFilledSlot > 1 && IsHasManyColors())
        //    {
        //        ShowColorPickerParent(true);
        //    }
        //}

        //private bool IsHasManyColors()
        //{
        //    int colorCount = 0;

        //    for(int i = 0; i < _gemsColors.Count; i++)
        //    {
        //        Debug.LogWarning($"{name} color {_gemsColors[i]} = Empty: {_gemsColors[i] != GemsColor.Empty}");
        //        if (_gemsColors[i] != GemsColor.Empty)
        //        {
        //            colorCount++;
        //        }
        //    }
        //    Debug.LogWarning($"{name} count: {colorCount}");

        //    return colorCount > 1;
        //}

        public void ShowColorPickerParent()
        {
            _colorPicker.SetActive(true);
            SetColorPickerSlotColor();
        }

        public void HideColorPickerParent()
        {
            _colorPicker.SetActive(false);
        }

        private void SetColorPickerSlotColor()
        {
            for(int slot = 0; slot < _colorPicker.transform.childCount; slot++)
            {
                Transform colorSlot = _colorPicker.transform.GetChild(slot);
                SpriteRenderer spriteRenderer = colorSlot.GetComponent<SpriteRenderer>();

                spriteRenderer.color = _dotFills[slot].GetComponent<SpriteRenderer>().color;
            }
        }
    }
}

