using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;

namespace Unboxed.Puzzle
{
    public class DotStarter : Dot
    {
        protected override void Start()
        {
            base.Start();
            SetSpriteColor(gameObject, _gemsColor);
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

                SetSpriteColor(gameObject, _gemsColor);
                PlayFillAnimation();
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
                ResetDotColor();
                SetSpriteColor(gameObject, _gemsColor);
                PlayClearAniamtion();
            }
        }

        protected override void CompleteDot()
        {
            if (IsComplete())
            {
                //TODO: Complete condition
                _dotState = DotState.Complete;
                PlayCompleteAnimation();
            }
        }

        protected override void PlayFillAnimation()
        {
            Debug.Log($"{name} trigger enter");
            _animator.SetTrigger("Enter");
        }

        protected override void PlayClearAniamtion()
        {
            Debug.Log($"{name} trigger exit");
            _animator.SetTrigger("Exit");
        }

        protected override void PlayCompleteAnimation()
        {
            Debug.Log($"{name} trigger complete");
            _animator.SetTrigger("Complete");
        }
    }
}


