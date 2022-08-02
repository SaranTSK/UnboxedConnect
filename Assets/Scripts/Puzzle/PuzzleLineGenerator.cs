using System.Collections;
using System.Collections.Generic;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.Puzzle
{
    public class PuzzleLineGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject _linePref;

        private Grid _grid;
        private List<Vector3> _linePositions;
        private List<GameObject> _dots;
        private List<LineRenderer> _lines;

        public void InitLineGenerator(Grid grid)
        {
            _grid = grid;
            _linePositions = new List<Vector3>();
            _dots = new List<GameObject>();
            _lines = new List<LineRenderer>();
        }

        public void AddLinePosition(Vector3 position)
        {
            _linePositions.Add(position);
        }

        public void AddDot(GameObject dot)
        {
            _dots.Add(dot);
        }

        public void DrawLine()
        {
            for(int x = 0; x < _grid.Width; x++)
            {
                for(int y = 0; y < _grid.Height; y++)
                {
                    //Debug.Log($"Round: {x} and {y}");
                    int index = UnboxedUtility.GetDotArrayIndex(x, y, _grid);

                    if(_dots[index].CompareTag("Dot"))
                    {
                        DrawMiddleLine(x, y);
                        DrawUpperLine(x, y);
                        DrawLowerLine(x, y);
                    }
                }
            }
        }

        private void DrawMiddleLine(int x, int y)
        {
            if(UnboxedUtility.IsHasMiddleRight(x, _grid))
            {
                int currentIndex = UnboxedUtility.GetDotArrayIndex(x, y, _grid);
                int targetIndex = UnboxedUtility.GetMiddleRightDotArrayIndex(x, y, _grid);

                DrawLine(_dots[currentIndex], _dots[targetIndex]);
            }
        }

        private void DrawUpperLine(int x, int y)
        {
            if(UnboxedUtility.IsHasUpperRight(x, y, _grid))
            {
                int currentIndex = UnboxedUtility.GetDotArrayIndex(x, y, _grid);
                int targetIndex = UnboxedUtility.GetUpperRightDotArrayIndex(x, y, _grid);

                DrawLine(_dots[currentIndex], _dots[targetIndex]);
            }
            
        }

        private void DrawLowerLine(int x, int y)
        {
            if(UnboxedUtility.IsHasLowerRight(x, y, _grid))
            {
                int currentIndex = UnboxedUtility.GetDotArrayIndex(x, y, _grid);
                int targetIndex = UnboxedUtility.GetLowerRightDotArrayIndex(x, y, _grid);

                DrawLine(_dots[currentIndex], _dots[targetIndex]);
            }
            
        }

        private void DrawLine(GameObject startDot, GameObject endDot)
        {
            if (endDot.CompareTag("Dot"))
            {
                var startPos = startDot.transform.position;
                var endPos = endDot.transform.position;


                GameObject line = Instantiate(_linePref);
                line.name = $"Line_{_dots.IndexOf(startDot)}_{_dots.IndexOf(endDot)}";

                LineRenderer lineRenderer = line.GetComponent<LineRenderer>();
                lineRenderer.transform.SetParent(transform);
                _lines.Add(lineRenderer);

                startPos = new Vector3(startPos.x, startPos.y, 1);
                endPos = new Vector3(endPos.x, endPos.y, 1);

                lineRenderer.SetPosition(0, startPos);
                lineRenderer.SetPosition(1, endPos);
            }
        }

        public void OnComplete()
        {
            foreach(var line in _lines)
            {
                int startDot = UnboxedUtility.GetIntIndexFromName(line.name, 1);
                int endDot = UnboxedUtility.GetIntIndexFromName(line.name, 2);

                Color startColor = UnboxedUtility.GetZeroToOneRangeColor(_dots[startDot].GetComponent<Dot>().GemsColor);
                Color endColor = UnboxedUtility.GetZeroToOneRangeColor(_dots[endDot].GetComponent<Dot>().GemsColor);

                ChangeLineColor(line, startColor, endColor);
            }
        }

        private void ChangeLineColor(LineRenderer line, Color startColor, Color endcolor)
        {
            //Color fromStartColor = line.startColor;
            //Color fromEndColor = line.endColor;

            line.startColor = startColor;
            line.endColor = endcolor;
        }
    }
}

