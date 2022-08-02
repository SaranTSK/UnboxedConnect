using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Manager;
using Unboxed.Interface;

namespace Unboxed.UI
{
    public class TitleContentUI : MonoBehaviour, IShowable
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void OnClickPlayButton()
        {
            SceneLoaderManager.Instance.LoadScene(SceneName.HomeScene);
        }
    }
}


