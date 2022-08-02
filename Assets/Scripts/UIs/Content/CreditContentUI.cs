using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;

namespace Unboxed.UI
{
    public class CreditContentUI : MonoBehaviour, IShowable
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}


