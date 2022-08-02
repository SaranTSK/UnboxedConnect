using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Unboxed.Manager;
using Unboxed.Interface;

namespace Unboxed.UI
{
    public class GemsButton : MonoBehaviour, IPointerClickHandler, ISelectable
    {
        [SerializeField] private GemsColor _gemsColor;

        private bool _isSelected = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            Debug.Log($"{name} Game Object Clicked!");

            if (!_isSelected)
            {
                Select();

                UIManager.Instance.GetContentComponenet<HomeContentUI>(Content.HomeContent).ChangeGemsSelected(_gemsColor);
                UIManager.Instance.GetHeaderComponent<HomeHeaderUI>(Header.HomeHeader).SetHeaderText($"{_gemsColor} Gems Level");
                UIManager.Instance.GetFooterComponenet<HomeFooterUI>(Footer.HomeFooter).CheckLevelPurchased(_gemsColor);
            }
        }

        public void Select()
        {
            _isSelected = true;
            ScaleUp();
            LevelManager.Instance.SetGemsColor(_gemsColor);
        }

        public void Deselect()
        {
            _isSelected = false;
            ScaleDown();
        }

        private void ScaleUp()
        {
            transform.LeanScale(new Vector3(1.5f, 1.5f, 1f), 0.5f).setEaseInOutQuad();
        }

        private void ScaleDown()
        {
            transform.LeanScale(Vector3.one, 0.5f).setEaseInOutQuad();
        }
    }
}


