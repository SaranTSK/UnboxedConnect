using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Scriptable;
using Unboxed.Utility;
using Unboxed.Interface;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Unboxed.Puzzle
{
    public class PuzzleGenerator : MonoBehaviour, IShowable
    {
        public Grid Grid => _grid;
        public Dictionary<GemsColor, GameObject[]> DotPairing => _dotPairing;
        public List<PuzzleStarterPairing> StarterParings => _starterParings;

        [SerializeField] private PuzzleScriptable _puzzleData;
        [SerializeField] private List<GameObject> _completePref;

        private PuzzleLineGenerator _lineGenerator; 
        private Grid _grid;

        private Dictionary<GemsColor, GameObject[]> _dotPairing;
        private List<PuzzleStarterPairing> _starterParings;

        private void Awake()
        {
            _lineGenerator = GetComponent<PuzzleLineGenerator>();
        }

        private IEnumerator LoadAndWaitUntilComplete()
        {
            //TODO: For debug
            //AsyncOperationHandle<GameObject> handle = Addressables.LoadAssetAsync();

            yield return AssetLoader.CreateAssetAddToList(_puzzleData.dots, _completePref);
            yield return new WaitUntil(() => IsLoaded());

            GridGenerate();
            //InitDotPairing();
        }

        private bool IsLoaded()
        {
            return _completePref.Count == _puzzleData.dots.Count;
        }

        //public void ResetGenerator()
        //{
        //    _puzzleData = null;
        //    _grid = null;
        //    _completePref.Clear();
        //}

        public void InitPuzzleGenerator(PuzzleScriptable puzzle)
        {
            //Debug.Log($"Load puzzle {puzzle.name} is {puzzle != null}");

            _puzzleData = puzzle;
            _grid = new Grid();
            _grid.Initailize(puzzle.width, puzzle.height, puzzle.gridGap, puzzle.oddStart, transform.localPosition);
            _lineGenerator.InitLineGenerator(_grid);
            _starterParings = puzzle.starterPairings;

            _dotPairing = new Dictionary<GemsColor, GameObject[]>();

            foreach (var gemsColor in puzzle.colors)
            {
                _dotPairing.Add(gemsColor, new GameObject[2]);
            }

            StartCoroutine(LoadAndWaitUntilComplete());
        }

        //TODO: Fixed begin spawn time
        public IEnumerator SpawningDots(float delayBegin)
        {
            yield return new WaitUntil(() => IsLoaded());
            yield return new WaitForSecondsRealtime(delayBegin);

            float delaySpawn = (_completePref.Count < 5) ? 0.25f : 0.5f / _completePref.Count;

            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GridArray[x, y].TryGetComponent(out Dot dot))
                    {
                        dot.OnSpawn();
                        yield return new WaitForSecondsRealtime(delaySpawn);
                    }
                }
            }
        }

        public void ExitGenerator()
        {
            ClearAssets();
            _dotPairing.Clear();
        }

        public void OnComplete()
        {
            _lineGenerator.OnComplete();
        }

        private void ClearAssets()
        {
            foreach(var dot in _puzzleData.dots)
            {
                dot.ReleaseAsset();
            }
        }

        private void GridGenerate()
        {
            GameObject puzzleParent = new GameObject("Puzzle");
            puzzleParent.transform.SetParent(transform);

            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    int index = UnboxedUtility.GetDotArrayIndex(x, y, _grid);

                    GameObject go = Instantiate(_completePref[index], transform);

                    go.name = $"Dot_{x}_{y}";
                    go.transform.position = _grid.GetCellWorldPosition(x, y);
                    go.transform.SetParent(puzzleParent.transform);

                    if(go.TryGetComponent(out Dot dot))
                    {
                        dot.InitDot();
                        dot.SetIndex(index);
                        //dot.SetStarterKeyColor(GetStarterDotGemsColor(dot));
                    }
                    else
                    {
                        Debug.LogWarning("No dot component");
                    }

                    _grid.GridArray[x, y] = go;

                    _lineGenerator.AddLinePosition(go.transform.position);
                    _lineGenerator.AddDot(go);
                }
            }

            _lineGenerator.DrawLine();
            //ClearAsset();
        }

        private void InitDotPairing()
        {
            for (int x = 0; x < _grid.Width; x++)
            {
                for (int y = 0; y < _grid.Height; y++)
                {
                    if (_grid.GridArray[x, y].TryGetComponent(out Dot dot))
                    {
                        if (dot.GemsColor != GemsColor.Empty)
                        {
                            int index = _dotPairing[dot.GemsColor].GetValue(0) == null ? 0 : 1;

                            _dotPairing[dot.GemsColor].SetValue(dot.gameObject, index);
                        }
                    }
                }
            }

            foreach (var pair in _dotPairing)
            {
                Debug.Log($"{pair.Key} Pair 1 {pair.Value[0].name} and Pair 2 {pair.Value[1].name}");
            }
        }

        private GemsColor GetStarterDotGemsColor(Dot dot)
        {
            foreach (PuzzleStarterPairing paring in _starterParings)
            {
                //Debug.Log($"Starter paring count {paring.pairingIndex.Length}");
                for (int i = 0; i < paring.pairingIndex.Length; i++)
                {
                    //Debug.Log($"Dot Index {dot.Index} and paring index {paring.pairingIndex[i]}");
                    if (dot.Index == paring.pairingIndex[i])
                    {
                        return paring.gemsColor;
                    }
                }
            }

            return GemsColor.Empty;
        }

        private Transform GetPuzzleParentTransform()
        {
            return transform.GetChild(0);
        }

        public Dot GetDotByIndex(int index)
        {
            return GetPuzzleParentTransform().GetChild(index).GetComponent<Dot>();
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }

        //public Dot GetDotByIndex(int index)
        //{
        //    for(int i = 0; i < _completePref.Count; i++)
        //    {
        //        Dot dot = _completePref[i].GetComponent<Dot>();

        //        if (index == dot.Index)
        //        {
        //            return dot;
        //        }
        //    }
        //    return null;
        //}
    }
}


