using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unboxed.Utility
{
    public static class AnimationUtility
    {
        public static void MovingUIToLeft(Transform transform)
        {
            transform.LeanMoveLocal(new Vector2(-Constant.CanvasWidthRef, transform.localPosition.y), 1).setEaseInOutQuad();
        }

        public static void MovingUIToRight(Transform transform)
        {
            transform.LeanMoveLocal(new Vector2(Constant.CanvasWidthRef, transform.localPosition.y), 1).setEaseInOutQuad();
        }

        public static void MovingUIToCenter(Transform transform)
        {
            transform.LeanMoveLocal(new Vector2(0, transform.localPosition.y), 1).setEaseInOutQuad();
        }

        public static void MovingGameObjectToLeft(Transform transform)
        {
            transform.LeanMoveLocal(new Vector2(-Screen.width / 100, transform.localPosition.y), 1).setEaseInOutQuad();
        }

        public static void MovingGameObjectToRight(Transform transform)
        {
            transform.LeanMoveLocal(new Vector2(Screen.width / 100, transform.localPosition.y), 1).setEaseInOutQuad();
        }

        public static void MovingGameObjectToCenter(Transform transform)
        {
            transform.LeanMoveLocal(new Vector2(0, transform.localPosition.y), 1).setEaseInOutQuad();
        }
    }
}


