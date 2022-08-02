using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using Unboxed.Manager;
using Unboxed.Utility;

namespace Unboxed.Puzzle
{
    public class LinePlayer : MonoBehaviour, ISelectable, IShowable
    {
        public bool IsSelected => _isSelected;

        private LineRenderer _line;
        private bool _isSelected;
        private bool _isComplete;

        private void Awake()
        {
            _line = GetComponent<LineRenderer>();
        }

        public void InitLinePlayer(GemsColor gemsColor)
        {
            var color = UnboxedUtility.GetZeroToOneRangeColor(gemsColor);

            if (_line.TryGetComponent(out LineRenderer line))
            {
                line.startColor = color;
                line.endColor = color;
            }

            Hide();
        }

        public void Select()
        {
            _isSelected = true;
        }

        public void Deselect()
        {
            _isSelected = false;
        }

        public void Complete()
        {
            _isComplete = true;
        }

        public bool IsLineHaveToDraw()
        {
            //Debug.LogError($"Line: {name} | Selected: {_isSelected} | Complete: {_isComplete}");
            return _isSelected && !_isComplete;
        }

        public void DrawLine(List<GameObject> dots)
        {
            if(_line.TryGetComponent(out LineRenderer line))
            {
                line.positionCount = dots.Count;

                foreach (var dot in dots)
                {
                    int index = dots.IndexOf(dot);

                    line.SetPosition(index, dots[index].transform.position);
                }
            }
        }

        public void Show()
        {
            transform?.gameObject.SetActive(true);
        }

        public void Hide()
        {
            transform?.gameObject.SetActive(false);
        }

        public void ResetLine()
        {
            if (_line.TryGetComponent(out LineRenderer line))
            {
                line.positionCount = 1;
                line.SetPosition(0, Vector3.zero);
            }
            _isComplete = false;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}


