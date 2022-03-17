using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Player;
using Unboxed.Puzzle;
using UnityEngine;

namespace Unboxed
{
    public class ColorSlot : MonoBehaviour
    {
        public GemsColor GemsColor => _gemsColor;

        private GemsColor _gemsColor;

        public void SetGemsColor(GemsColor gemsColor)
        {
            _gemsColor = gemsColor;
        }

        //public void SetKeyGemsColor(GemsColor gemsColor)
        //{
        //    _keyGemsColor = gemsColor;
        //}

        public bool IsEmptyColor()
        {
            return _gemsColor == GemsColor.Empty;
        }

        public int GetSlotIndex()
        {
            int index = int.Parse(name.Split('_').GetValue(1).ToString());
            return index;
        }

        public void OnColorSelected(PlayerManager player)
        {
            player.SetCurrentGemsColor(_gemsColor);
            player.SetKeyGemsColor(_gemsColor);
            player.SetShowColorPicker(false);
            //Debug.LogWarning($"Set player color: {_gemsColor}");
        }

        public DotSpecial GetDotSpacial()
        {
            return GetComponentInParent<DotSpecial>();
        }
}
}

