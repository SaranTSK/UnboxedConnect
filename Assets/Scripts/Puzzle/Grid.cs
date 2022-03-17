using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unboxed
{
    public class Grid
    {
        public GameObject[,] GridArray => _gridArray;
        public int Width => _gridArray.GetLength(0);
        public int Height => _gridArray.GetLength(1);
        public bool OddStart => _isOddStart;

        private int _width;
        private int _height;
        private float _gridGap;
        private bool _isOddStart;

        private Vector3 _startPos;

        private GameObject[,] _gridArray;

        public void Initailize(int width, int height, float gridGap, bool isOddStart, Vector3 startPos)
        {
            _width = width;
            _height = height;
            _gridGap = gridGap;
            _isOddStart = isOddStart;

            _gridArray = new GameObject[width, height];
            _startPos = startPos + CalculateStartPos();
        }

        //TODO: Re-calculate center position when change width and height
        public Vector3 GetCellWorldPosition(int x, int y)
        {
            Vector2 position = new Vector2(x, y);
            return CalculateWorldPos(position);
        }

        private Vector3 CalculateStartPos()
        {
            float pixelPerUnit = 256f;
            float spriteWidth = 256f / pixelPerUnit;
            float spriteHeight = 256f / pixelPerUnit;

            spriteWidth += spriteWidth * _gridGap;
            spriteHeight += spriteHeight * _gridGap;

            //TODO: Re-calculate position to center
            float offsetX = (_width % 2 == 0) ? -spriteWidth / 2 : 0;
            float offsetY = (_height % 2 == 0) ? spriteHeight / 2 : 0;

            //Debug.Log($"Offset: {offsetX} and Y: {offsetY}");

            float x = -spriteWidth * (_width / 2) + offsetX;
            float y = spriteHeight * 0.75f * (_height / 2) - offsetY;

            return new Vector3(x, y, 0);
        }

        //private float CalculateOffsetX(float spriteWidth)
        //{
        //    float offset = 0;

        //    if (_width % 2 == 0 && _isOddStart)
        //    {
        //        Debug.Log("Offset 1");
        //        offset = spriteWidth / 2;
        //    }
        //    else if (_width % 2 == 0 && !_isOddStart)
        //    {
        //        Debug.Log("Offset 2");
        //        offset = spriteWidth / 2;
        //    }
        //    else if (_width % 2 != 0 && _isOddStart)
        //    {
        //        Debug.Log("Offset 3");
        //        offset = -spriteWidth * 0.75f;
        //    }
        //    else if (_width % 2 != 0 && _height % 2 == 0 && !_isOddStart)
        //    {
        //        Debug.Log("Offset 4");
        //        offset = -spriteWidth / 2;
        //    }
        //    else if (_width % 2 != 0 && _height % 2 == 0 && _isOddStart)
        //    {
        //        Debug.Log("Offset 5");
        //        offset = -spriteWidth / 2;
        //    }
        //    else
        //    {
        //        Debug.Log("Offset 0");
        //    }

        //    return offset;
        //}

        private Vector3 CalculateWorldPos(Vector2 position)
        {
            float offset = 0;

            float pixelPerUnit = 256f;
            float spriteWidth = 225f / pixelPerUnit;
            float spriteHeight = 256f / pixelPerUnit;

            spriteWidth += spriteWidth * _gridGap;
            spriteHeight += spriteHeight * _gridGap;

            if (_isOddStart)
            {
                if (position.y % 2 == 0)
                { offset = spriteWidth / 2; }
            }
            else
            {
                if (position.y % 2 != 0)
                { offset = spriteWidth / 2; }
            }


            float x = _startPos.x + position.x * spriteWidth + offset;
            float y = _startPos.y - position.y * spriteHeight * 0.75f;

            return new Vector3(x, y, 0);
        }
    }
}

