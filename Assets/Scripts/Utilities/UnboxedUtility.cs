using System;
using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using UnityEngine;

namespace Unboxed.Utility
{
    public static class UnboxedUtility
    {
        public static Vector3 GetMousePosition(Vector3 position)
        {
            return Camera.main.ScreenToWorldPoint(position);
        }

        public static int GetIntIndexFromName(string name, int value)
        {
            string[] splits = name.Split('_');

            if (int.TryParse(splits[value], out int index))
            {
                return index;
            }
            else
            {
                throw new Exception("Missing cast");
            }
        }

        public static int GetDotArrayIndex(int x, int y, Grid grid)
        {
            return (x * grid.Height) + y;
        }

        public static int GetUpperRightDotArrayIndex(int x, int y, Grid grid)
        {
            if (grid.OddStart)
            {
                x = (y % 2 != 0) ? x : x + 1;
            }
            else
            {
                x = (y % 2 != 0) ? x + 1 : x;
            }

            y = y - 1;
            
            return (x * grid.Height) + y;
        }

        public static int GetMiddleRightDotArrayIndex(int x, int y, Grid grid)
        {
            x += 1;

            return (x * grid.Height) + y;
        }

        public static int GetLowerRightDotArrayIndex(int x, int y, Grid grid)
        {
            if (grid.OddStart)
            {
                x = (y % 2 != 0) ? x : x + 1;
            }
            else
            {
                x = (y % 2 != 0) ? x + 1 : x;
            }

            y = y + 1;

            return (x * grid.Height) + y;
        }

        public static int GetUpperLeftDotArrayIndex(int x, int y, Grid grid)
        {
            if (grid.OddStart)
            {
                x = (y % 2 != 0) ? x - 1 : x;
            }
            else
            {
                x = (y % 2 != 0) ? x : x - 1;
            }

            y = y - 1;

            return (x * grid.Height) + y;
        }

        public static int GetMiddleLeftDotArrayIndex(int x, int y, Grid grid)
        {
            x -= 1;

            return (x * grid.Height) + y;
        }

        public static int GetLowerLeftDotArrayIndex(int x, int y, Grid grid)
        {
            if (grid.OddStart)
            {
                x = (y % 2 != 0) ? x - 1 : x;
            }
            else
            {
                x = (y % 2 != 0) ? x : x - 1;
            }

            y = y + 1;

            return (x * grid.Height) + y;
        }

        public static bool IsHasUpperRight(int x, int y, Grid grid)
        {
            bool result;

            if (grid.OddStart)
            {
                result = ((x + 1 < grid.Width && y - 1 >= 0) || (x + 1 == grid.Width && y - 1 >= 0 && y % 2 != 0)) ? true : false;
            }
            else
            {
                result = ((x + 1 < grid.Width && y - 1 >= 0) || (x + 1 == grid.Width && y - 1 >= 0 && y % 2 == 0)) ? true : false;
            }

            return result;
        }

        public static bool IsHasMiddleRight(int x, Grid grid)
        {
            return (x + 1 < grid.Width) ? true : false;
        }

        public static bool IsHasLowerRight(int x, int y, Grid grid)
        {
            bool result;

            if (grid.OddStart)
            {
                result = ((x + 1 < grid.Width && y + 1 < grid.Height) || (x + 1 == grid.Width && y + 1 < grid.Height && y % 2 != 0)) ? true : false;
            }
            else
            {
                result = ((x + 1 < grid.Width && y + 1 < grid.Height) || (x + 1 == grid.Width && y + 1 < grid.Height && y % 2 == 0)) ? true : false;
            }

            return result;
        }

        public static bool IsHasUpperLeft(int x, int y, Grid grid)
        {
            bool result;

            if (grid.OddStart)
            {
                result = ((x > 0 && y - 1 >= 0) || (x == 0 && y - 1 >= 0 && y % 2 == 0)) ? true : false;
            }
            else
            {
                result = ((x > 0 && y - 1 >= 0) || (x == 0 && y - 1 >= 0 && y % 2 != 0)) ? true : false;
            }

            return result;
        }

        public static bool IsHasMiddleLeft(int x)
        {
            return (x > 0) ? true : false;
        }

        public static bool IsHasLowerLeft(int x, int y, Grid grid)
        {
            bool result;

            if (grid.OddStart)
            {
                result = ((x > 0 && y + 1 < grid.Height) || (x == 0 && y + 1 < grid.Height && y % 2 == 0)) ? true : false;
            }
            else
            {
                result = ((x > 0 && y + 1 < grid.Height) || (x == 0 && y + 1 < grid.Height && y % 2 != 0)) ? true : false;
            }

            return result;
        }

        public static Color GetColorByGemsColor(GemsColor gemsColor)
        {
            var color = gemsColor switch
            {
                GemsColor.Empty => new Color(255, 255, 255),
                GemsColor.Red => new Color(204, 0, 0),
                GemsColor.Orange => new Color(255, 124, 25),
                GemsColor.Yellow => new Color(255, 232, 47),
                GemsColor.Green => new Color(62, 226, 0),
                GemsColor.Turquoise => new Color(30, 255, 233),
                GemsColor.Navy => new Color(0, 56, 217),
                GemsColor.Violet => new Color(161, 84, 245),
                GemsColor.Pink => new Color(255, 114, 249),
                GemsColor.Black => new Color(48, 48, 48),
                GemsColor.White => new Color(253, 248, 213),
                _ => new Color(0, 0, 0)
            };

            return color;
        }

        public static Color GetZeroToOneRangeColor(GemsColor gemsColor)
        {
            var color = GetColorByGemsColor(gemsColor);

            color.r /= 256f;
            color.g /= 256f;
            color.b /= 256f;

            return color;
        }
    }
}

