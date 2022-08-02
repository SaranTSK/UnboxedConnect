using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Pattern;
using Unboxed.Scriptable;
using Unboxed.UI;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.Asset
{
    public class DataAsset : Singleton<DataAsset>
    {
        [SerializeField] private AssetMode[] _assets;

        private Dictionary<GemsColor, PuzzleScriptable[]> _dataAssets;

        private void Start()
        {
            InitDataAsset();
        }

        public void InitDataAsset()
        {
            _dataAssets = new Dictionary<GemsColor, PuzzleScriptable[]>();

            for(int color = 0; color < Constant.MaxGemsColor; color++)
            {
                _dataAssets.Add((GemsColor)color, _assets[color].puzzles);

                LoadDataAsset((GemsColor)color);

                //Debug.Log($"Data {(GemsColor)color} count: {_dataAssets[(GemsColor)color].Length}");
            }
        }

        private void LoadDataAsset(GemsColor gemsColor)
        {
            for(int index = 0; index < _assets[(int)gemsColor].puzzles.Length; index++)
            {
                _dataAssets[gemsColor][index] = _assets[(int)gemsColor].puzzles[index];
            }
        }

        public PuzzleScriptable GetAssetByGemsColor(GemsColor gemsColor, int level)
        {
            return _dataAssets[gemsColor][level];
        }

        public int GetAssetsLenghtByGemstone(GemsColor gemsColor)
        {
            return _dataAssets[gemsColor].Length;
        }
    }

    [System.Serializable]
    public class AssetMode
    {
        public GemsColor gemsColor;
        public PuzzleScriptable[] puzzles;
    }
}

