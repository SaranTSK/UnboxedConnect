using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Puzzle;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.Player
{
    public class DrawController : MonoBehaviour
    {
        private LinePlayer _linePlayer;
        private PlayerManager _player;
        private Dictionary<GemsColor, LinePlayer> _lines;

        private void FixedUpdate()
        {
            if(GameManager.Instance.GameState == GameState.PuzzleState)
            {
                DrawSelectedLine();
            }
            else if(GameManager.Instance.GameState == GameState.CompleteState)
            {
                OnComplete();
            }
        }

        //private void ResetController()
        //{
        //    Debug.LogWarning($"Reset DrawController!");
        //    foreach (var line in _lines)
        //    {
        //        line.Value.ResetLine();
        //    }
        //}

        // TODO: Change to parent class
        internal void BindPlayer(PlayerManager player, LinePlayer linePlayer)
        {
            _player = player;
            _player.OnClickEvent += OnClick;
            _player.OnHoldEvent += OnHold;
            _player.OnReleaseEvent += OnRelease;
            _player.OnRestartEvent += OnRestart;
            _linePlayer = linePlayer;
            //_player.OnExitEvent += OnExit;
        }

        // TODO: Change to parent class
        internal void InitDrawController(List<GemsColor> gemsColors)
        {
            //Debug.Log($"Line is null: {_lines == null}");

            _lines ??= new Dictionary<GemsColor, LinePlayer>();

            foreach (var gemsColor in gemsColors)
            {
                _lines.Add(gemsColor, Instantiate(_linePlayer, _player.transform));
                _lines[gemsColor].name = $"{gemsColor}PlayerLine";
            }

            InitLinePlayers();
        }

        // TODO: Change to parent class
        internal void UpdateDrawController(List<GemsColor> gemsColors)
        {
            foreach (var gemsColor in gemsColors)
            {
                if (!_lines.ContainsKey(gemsColor))
                {
                    _lines.Add(gemsColor, Instantiate(_linePlayer, _player.transform));
                    _lines[gemsColor].name = $"{gemsColor}PlayerLine";
                }
                else
                {
                    _lines[gemsColor].ResetLine();
                }
            }

            InitLinePlayers();
        }

        // TODO: Change to parent class
        private void InitLinePlayers()
        {
            foreach (var line in _lines)
            {
                //Debug.Log($"{line.Key}, {line.Value}");
                line.Value.InitLinePlayer(line.Key);
            }
        }

        // TODO: Change to parent class
        internal void OnClick(GameObject dot)
        {
            if (dot.TryGetComponent(out Dot selectedDot))
            {
                if (selectedDot.GemsColor != GemsColor.Empty && selectedDot.State != Dot.DotState.Complete)
                {
                    GetCurrentLines().Select();
                }
            }
        }

        // TODO: Change to parent class
        internal void OnHold(GameObject dot)
        {

        }

        // TODO: Change to parent class
        internal void OnRelease()
        {
            Debug.Log("Draw Release");
            if(_player.CurrentGemsColor != GemsColor.Empty)
            {
                var dots = _player.PuzzleController.GetDots(_player.CurrentGemsColor);
                var line = GetCurrentLines();

                line.Deselect();

                if(dots.Count < 2)
                {
                    line.ResetLine();
                }
                else
                {
                    line.DrawLine(dots);
                }
            }
        }

        // TODO: Change to parent class
        internal void OnRestart()
        {
            foreach (var line in _lines)
            {
                line.Value.ResetLine();
                line.Value.Hide();
            }
        }

        // TODO: Change to parent class
        internal void OnComplete()
        {
            foreach (var line in _lines)
            {
                line.Value.ResetLine();
                line.Value.Complete();
                line.Value.Hide();
            }
        }

        //internal void OnExit()
        //{
        //    //OnRestart();
        //    _lines.Clear();
        //}

        //TODO: Hide line with this!
        private void DrawSelectedLine()
        {
            if(_player.CurrentGemsColor != GemsColor.Empty)
            {
                var dots = _player.PuzzleController.GetDots(_player.CurrentGemsColor);
                var line = GetCurrentLines();

                //Debug.Log($"{_player.CurrentGemsColor} : {dots.Count}");

                if (dots.Count > 0 && line.IsLineHaveToDraw())
                {
                    line.Show();
                    line.DrawLine(dots);
                }
                else
                {
                    line.Hide();
                }
            }
        }

        private LinePlayer GetCurrentLines()
        {
            return (_player.CurrentGemsColor == GemsColor.Empty) ? null : _lines[_player.CurrentGemsColor];
        }

        //private GameObject cursorPref;
        //private LineRenderer line;

        //private GameObject cursor;
        //private LineRenderer[] lineList;
        //private DotList dotList;
        //private int currentList;
        //private LevelManager levelManager;

        //public void Initialize()
        //{
        //    cursorPref = ObjectAsset.instance.GetPref(PrefType.Cursor);
        //    line = ObjectAsset.instance.GetPref(PrefType.PlayerLine).GetComponent<LineRenderer>();

        //    dotList = new DotList();
        //    dotList.Create();

        //    lineList = new LineRenderer[dotList.list.Count];
        //    levelManager = LevelManager.instance;
        //}

        //public void SetCurrentList(int listIndex)
        //{
        //    currentList = listIndex;
        //}

        //public void OnClick(Dot dot)
        //{
        //    switch (dot.type)
        //    {
        //        case DotType.MainAlphaDot:
        //        case DotType.MainBetaDot:
        //        case DotType.MainGammaDot:
        //            ClickMainDot(dot);
        //            break;

        //        case DotType.AlphaDot:
        //        case DotType.BetaDot:
        //        case DotType.GammaDot:
        //            ClickNormalDot(dot);
        //            break;
        //    }
        //}

        //public void OnEnter(Dot dot)
        //{
        //    switch (dot.type)
        //    {
        //        case DotType.MainAlphaDot:
        //        case DotType.MainBetaDot:
        //        case DotType.MainGammaDot:
        //            EnterMainDot(dot);
        //            break;

        //        case DotType.AlphaDot:
        //        case DotType.BetaDot:
        //        case DotType.GammaDot:
        //            EnterNormalDot(dot);
        //            break;
        //    }
        //}

        //public void SetMousePos(Vector3 position)
        //{
        //    if (GetDotList() != null && GetDotListCount() > 1 && cursor != null)
        //    {
        //        cursor.transform.position = new Vector3(position.x, position.y, 0);
        //    }
        //}

        //public void OnRelease()
        //{
        //    if (GetDotList() != null && GetDotListCount() > 1)
        //    {
        //        RemoveCursor();

        //        //Debug.Log("List count: " + GetDotList(currentList).Count);
        //        // Reset when release mouse button
        //        if (GetDotListCount() == 1)
        //        {
        //            //GetDotList()[0].SetPointIndex(-1);
        //            //GetDotList()[0].CheckDotState();
        //            UpdatePointIndex(GetDot(0), 1);
        //            ClearDotList();
        //            //GetDotList().Clear();
        //        }
        //    }
        //}

        //public void DrawLine()
        //{
        //    for (int i = 0; i < dotList.list.Count; i++)
        //    {
        //        if (GetDotList(i) != null && GetDotListCount(i) > 0)
        //        {
        //            for (int index = 0; index < GetDotListCount(i); index++)
        //            {
        //                lineList[i].SetPosition(index, GetDot(i, index).transform.position);
        //            }
        //        }
        //    }
        //}

        //public bool CheckMatchType(int index, Dot dot)
        //{
        //    switch (index)
        //    {
        //        case (int)ListType.Alpha:
        //            return dot.type == DotType.MainAlphaDot || dot.type == DotType.AlphaDot;

        //        case (int)ListType.Beta:
        //            return dot.type == DotType.MainBetaDot || dot.type == DotType.BetaDot;

        //        case (int)ListType.Gamma:
        //            return dot.type == DotType.MainGammaDot || dot.type == DotType.GammaDot;

        //        default: return false;
        //    }
        //}

        //// For start line drawing
        //private void ClickMainDot(Dot dot)
        //{
        //    if (GetDotListCount() <= 0)
        //    {
        //        CreateCursor();
        //        UpdatePointIndex(dot, 0);

        //        AddDotToList(dot);
        //        AddDotToList(GetCursorDot());
        //        CreateLine(currentList);
        //        ChangeLinePositionList();
        //    }
        //    else if (GetDotListCount() > 1 && !IsDotListComplete())
        //    {
        //        CreateCursor();
        //        UpdatePointIndex(dot, 0);
        //        ResetDot();

        //        AddDotToList(dot);
        //        AddDotToList(GetCursorDot());
        //        CreateLine(currentList);
        //        ChangeLinePositionList();
        //    }
        //}

        //// For undo line drawing
        //private void ClickNormalDot(Dot dot)
        //{
        //    if (GetDotListCount() > 1 && IsDotInList(dot) && !IsDotListComplete())
        //    {
        //        Debug.Log("Click normal dot - " + dot.pointIndex);
        //        RemoveDot(dot.pointIndex);
        //        CreateCursor();
        //        AddDotToList(GetCursorDot());
        //        CreateLine(currentList);
        //        ChangeLinePositionList();
        //    }
        //}

        //private void EnterMainDot(Dot dot)
        //{
        //    if (GetDotListCount() > 1 && CheckMatchType(currentList, dot) && IsDotAllowed(dot))
        //    {
        //        UpdatePointIndex(dot, GetCurrentDotIndex());
        //        Debug.Log("Enter main dot " + dot.index + " : " + dot.pointIndex);
        //        InsertDotToList(dot, GetCurrentDotIndex());
        //        ChangeLinePositionList();
        //        RemoveCursor();
        //        OnComplete();
        //    }
        //    else
        //    {
        //        dot.DotWarning();
        //    }
        //}

        //private void EnterNormalDot(Dot dot)
        //{
        //    if (GetDotListCount() > 0 && CheckMatchType(currentList, dot) && IsDotAllowed(dot))
        //    {
        //        UpdatePointIndex(dot, GetCurrentDotIndex());
        //        Debug.Log("Enter normal dot " + dot.index + " : " + dot.pointIndex);
        //        InsertDotToList(dot, GetCurrentDotIndex());
        //        ChangeLinePositionList();
        //    }
        //    else
        //    {
        //        dot.DotWarning();
        //    }
        //}

        //private void RemoveDot(int pointIndex)
        //{
        //    if (pointIndex < GetCurrentDotIndex())
        //    {
        //        // Reset dot property before remove from list
        //        for (int index = pointIndex + 1; index < GetDotListCount(); index++)
        //        {
        //            UpdatePointIndex(GetDot(index), -1);
        //        }

        //        GetDotList().RemoveRange(pointIndex + 1, GetDotListCount() - pointIndex - 1);
        //    }
        //}

        //// Reset dot property before clear list
        //private void ResetDot()
        //{
        //    for (int index = 0; index < GetDotListCount(); index++)
        //    {
        //        UpdatePointIndex(GetDot(index), -1);
        //    }
        //    ClearDotList();
        //}

        //private void OnComplete()
        //{
        //    Debug.Log("On Complete!!!");
        //    levelManager.CheckListResult(currentList, GetDotList());
        //    if (IsDotListComplete())
        //    {
        //        for (int index = 0; index < GetDotList().Count; index++)
        //        {
        //            UpdatePointIndex(GetDot(index), -2);
        //        }

        //        if (levelManager.CheckPuzzleResult())
        //        {
        //            levelManager.OnClearedPuzzle();
        //        }

        //        //Debug.Log("List " + (ListType)currentList + " | List status: " + IsDotListComplete() + " | Puzzle status: " + levelManager.CheckPuzzleResult());
        //    }
        //}

        //private void CreateCursor()
        //{
        //    if (cursor == null)
        //    {
        //        cursor = Instantiate(cursorPref);
        //        cursor.AddComponent<Dot>();
        //    }
        //}

        //// For remove cursor point object
        //private void RemoveCursor()
        //{
        //    if (cursor != null)
        //    {
        //        GetDotList().Remove(GetCursorDot());
        //        ChangeLinePositionList();
        //        Destroy(cursor);
        //    }
        //}

        //private void CreateLine(int index)
        //{
        //    lineList[index] = (lineList[index] == null) ? Instantiate(line) : lineList[index];
        //    levelManager.AddChildPuzzle(lineList[index].transform);
        //}

        //private List<Dot> GetDotList()
        //{
        //    return dotList.list[currentList];
        //}

        //private List<Dot> GetDotList(int index)
        //{
        //    return dotList.list[index];
        //}

        //private Dot GetDot(int index)
        //{
        //    return GetDotList()[index];
        //}

        //private Dot GetDot(int targetList, int index)
        //{
        //    return GetDotList(targetList)[index];
        //}

        //private int GetDotListCount()
        //{
        //    return GetDotList().Count;
        //}

        //private int GetDotListCount(int index)
        //{
        //    return GetDotList(index).Count;
        //}

        //private void AddDotToList(Dot dot)
        //{
        //    GetDotList().Add(dot);
        //}

        //private void InsertDotToList(Dot dot, int index)
        //{
        //    GetDotList().Insert(index, dot);
        //}

        //private void ClearDotList()
        //{
        //    GetDotList().Clear();
        //}

        //private int GetCurrentDotIndex()
        //{
        //    return GetDotList().Count - 1;
        //}

        //private int GetPreviousDotIndex()
        //{
        //    return GetDotList().Count - 2;
        //}

        //private Dot GetCursorDot()
        //{
        //    if (cursor != null)
        //    {
        //        return cursor.GetComponent<Dot>();
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}

        //private void ChangeLinePositionList()
        //{
        //    lineList[currentList].positionCount = GetDotList().Count;
        //}

        //private bool IsDotAllowed(Dot dot)
        //{

        //    Dot prevDot = GetDotList()[GetPreviousDotIndex()];

        //    //Debug.Log("Dot: " + dot.name + " | PrevDot: " + prevDot.name + " | " + (dot.state == DotState.Empty) + ", " + (dot.type != DotType.Empty) + ", " + levelManager.CheckGridValue(prevDot, dot));
        //    return dot.state == DotState.Empty && dot.type != DotType.Empty && levelManager.CheckGridValue(prevDot, dot);
        //}

        //private bool IsDotInList(Dot dot)
        //{
        //    return GetDotList().Contains(dot);
        //}

        //private void UpdatePointIndex(Dot dot, int index)
        //{
        //    dot.SetPointIndex(index);
        //    dot.CheckDotState();
        //}

        //private bool IsDotListComplete()
        //{
        //    return levelManager.GetDotListResult(currentList);
        //}
    }
}

