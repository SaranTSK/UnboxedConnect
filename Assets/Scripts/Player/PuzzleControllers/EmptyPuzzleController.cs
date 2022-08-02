using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Puzzle;
using UnityEngine;

namespace Unboxed.Player
{
    public class EmptyPuzzleController : AbstactPuzzleController
    {
        protected internal override void InitPuzzleController(List<GemsColor> gemsColors)
        {
            // For Init puzzle controller
            Debug.Log($"Init EmptyPuzzleController");
            InitSingleDictionary(gemsColors);
        }

        protected internal override void UpdatePuzzleController(List<GemsColor> gemsColors)
        {
            // For Update puzzle controller
            Debug.Log($"Update EmptyPuzzleController");
            UpdateSingleDictionary(gemsColors);
        }

        protected internal override void UpdatePlayerDotPosition()
        {
            // For Update override dot position
            //Debug.Log($"Update player EmptyPuzzleController");
            UpdateDotPlayerPosition(_firstDotPlayer, _player.PlayerController.GetMousePosition());
        }

        protected override void OnClick(GameObject dot)
        {
            // For OnClick puzzle controller
            Debug.Log($"Click EmptyPuzzleController");
            SetCurrentDot(dot);
            SpawnFirstPlayerDot(_player.transform);
            ShowDotPlayer(_firstDotPlayer);
            UpdatePlayerDotPosition();
            //TODO: Update player position

            if(dot.TryGetComponent(out Dot dotComponet))
            {
                if (IsDotCanClick(dotComponet))
                {
                    //TODO: Click dot
                    Debug.LogWarning($"Filled Slot {dotComponet.FilledSlot}");
                    _player.SetCurrentGemsColor(dotComponet.GemsColor);
                    _player.SetKeyGemsColor(dotComponet.KeyGemsColor);
                    Debug.LogWarning($"Click dot {dot.name} and color {dotComponet.GemsColor} and key color {dotComponet.KeyGemsColor}");

                    switch(dotComponet.State)
                    {
                        case Dot.DotState.Empty:
                            if(!dotComponet.IsSpecial) { OnClickedEmptyDot(dotComponet); }
                            else { OnClickEmptySpecialDot(dotComponet); }
                            break;
                        case Dot.DotState.Filled:
                            if(!dotComponet.IsSpecial) { OnClickedFilledDot(dotComponet);}
                            else { OnClickFilledSpecialDot(dotComponet); }
                            break;
                    }
                }
                else
                {
                    Debug.LogWarning($"{dot.name} cannot click!");
                }
            }
            else
            {
                Debug.LogWarning($"{dot.name} doesn't has dot component!");
            }
            
        }

        protected override void OnHold(GameObject other)
        {
            // For OnHold puzzle controller
            Debug.Log($"Hold EmptyPuzzleController");
            if(other.TryGetComponent(out Dot dot))
            {
                if (IsDotCanEnter(dot))
                {
                    //TODO: Enter dot
                    if (IsDotLinked(_currentDot, other))
                    {
                        if (!dot.IsSpecial) { OnEnterDot(dot); }
                        else { OnEnterSpecialDot(dot); }
                    }
                    else
                    {
                        Debug.LogWarning("Position don't match!");
                    }
                }
                else
                {
                    Debug.LogWarning($"{other.name} cannot enter!");
                }
            }
            else if(other.TryGetComponent(out ColorSlot colorSlot))
            {
                if(!colorSlot.IsEmptyColor())
                {
                    OnEnterColorSlot(colorSlot);
                }
            }
            
        }

        protected override void OnRelease()
        {
            // For OnRelease puzzle controller
            Debug.Log($"Release EmptyPuzzleController");
            // TODO: Hide color picker
            if(!_player.IsShowColorPicker)
            {
                if (IsDotCanRelease(_player.KeyGemsColor))
                {
                    //TODO: Release dot
                    var dots = GetFirstDots(_player.KeyGemsColor);

                    HideDotPlayer(_firstDotPlayer);
                    RemoveSingleDotPlayer(_player.KeyGemsColor);

                    if (IsDotsHaveToClear(dots))
                    {
                        ResetSingleDots(_player.KeyGemsColor);
                    }
                    SetCurrentDot(null);
                }
            }
            else
            {
                if(_currentDot.TryGetComponent(out DotSpecial dotSpecial))
                {
                    dotSpecial.HideColorPickerParent();
                    _player.SetShowColorPicker(false);
                    SetCurrentDot(null);
                }
            }
            
            //TODO: Check Color Picking State
        }

        protected override void OnRestart()
        {
            // For OnRestart puzzle controller
            Debug.Log($"Restart EmptyPuzzleController");
            foreach (var dots in _firstDots)
            {
                foreach (var dot in dots.Value)
                {
                    if(dot.TryGetComponent(out Dot dotComponent))
                    {
                        if(!dotComponent.IsSpecial)
                        {
                            dotComponent.OnExit();
                        }
                        else
                        {
                            dot.GetComponent<DotSpecial>().ResetAllFilledColor(); ;
                        }
                    }
                }
                dots.Value.Clear();
            }
        }

        protected override void OnClickedEmptyDot(Dot dot)
        {
            if(IsStarterDotInit(_player.KeyGemsColor))
            { ResetSingleDots(_player.KeyGemsColor); }

            var dots = GetFirstDots(_player.KeyGemsColor);

            dots.Add(dot.gameObject);
            dots.Add(_firstDotPlayer);
            dot.OnEnter();
        }

        protected override void OnClickEmptySpecialDot(Dot dot)
        {
            //TODO: Check color picker show up
            Debug.Log("This is empty dot special clicked!");

            var dots = GetFirstDots(_player.KeyGemsColor);
            var index = dots.IndexOf(dot.gameObject);

            switch (dot.FilledSlot)
            {
                case 1:
                    //TODO: Reset special dot
                    ResetRangeSingleDots(_player.KeyGemsColor, index + 1, dots.Count);
                    dots.Add(_firstDotPlayer);
                    break;
                case 2:
                    //TODO: Show color picker
                    dot.GetComponent<DotSpecial>().ShowColorPickerParent();
                    _player.SetShowColorPicker(true);
                    break;
                default:
                    break;
            }
        }

        protected override void OnClickedFilledDot(Dot dot)
        {
            var dots = GetFirstDots(_player.KeyGemsColor);
            var index = dots.IndexOf(dot.gameObject);

            if(index != GetDotsLastedIndex(dots) && !dot.IsStarter)
            {
                //TODO: Create Reset Range Function
                ResetRangeSingleDots(_player.KeyGemsColor, index + 1, dots.Count);
                dots.Add(_firstDotPlayer);
            }
            else if(dot.IsStarter)
            {
                ResetSingleDots(_player.KeyGemsColor);
                dots.Add(dot.gameObject);
                dots.Add(_firstDotPlayer);
                dot.OnEnter();
            }
            else
            {
                dots.Add(_firstDotPlayer);
            }
        }

        protected override void OnClickFilledSpecialDot(Dot dot)
        {
            //TODO: Check color picker show up
            Debug.Log("This is filled dot special clicked!");

            var dots = GetFirstDots(_player.KeyGemsColor);
            var index = dots.IndexOf(dot.gameObject);

            switch(dot.FilledSlot)
            {
                case 1:
                    //TODO: Reset special dot
                    break;
                case 2:
                case 3:
                    //TODO: Show color picker
                    dot.GetComponent<DotSpecial>().ShowColorPickerParent();
                    _player.SetShowColorPicker(true);
                    break;
                default:
                    break;
            }
        }

        protected override void OnEnterDot(Dot dot)
        {
            SetCurrentDot(dot.gameObject);
            var dots = GetFirstDots(_player.KeyGemsColor);
            Debug.LogWarning($"Get {dot.name} key color {_player.KeyGemsColor}");


            dots.Insert(GetDotsLastedIndex(dots), dot.gameObject);
            dot.SetGemsColor(_player.CurrentGemsColor);
            dot.SetKeyGemsColor(_player.KeyGemsColor);
            dot.OnEnter();

            Debug.LogWarning($"Enter current color {_player.CurrentGemsColor} and key color {_player.KeyGemsColor}");

            OnDotPairedHanderler();
        }

        protected override void OnEnterSpecialDot(Dot dot)
        {
            if(dot.TryGetComponent(out DotSpecial dotSpecial))
            {
                if(!dotSpecial.IsColorRepeated(_player.KeyGemsColor))
                {
                    SetCurrentDot(dot.gameObject);
                    var dots = GetFirstDots(_player.KeyGemsColor);
                    int index = dotSpecial.GetEmptyFillSlotIndex();

                    dots.Insert(GetDotsLastedIndex(dots), dot.gameObject);
                    dot.SetGemsColorByIndex(index, _player.CurrentGemsColor);
                    dot.SetKeyGemsColorByIndex(index, _player.KeyGemsColor);
                    dotSpecial.SetDotFillColor(index, _player.KeyGemsColor);
                    dot.OnEnter();

                    OnSpecialEventTrigger();
                    OnDotPairedHanderler();
                }
            }
        }

        //TODO: Reset dots if any
        protected override void OnEnterColorSlot(ColorSlot colorSlot)
        {
            Debug.LogWarning($"Enter color slot {colorSlot.name}");
            DotSpecial dotSpecial = colorSlot.GetDotSpacial();
            dotSpecial.HideColorPickerParent();

            SetCurrentDot(dotSpecial.gameObject);

            colorSlot.OnColorSelected(_player);

            var dots = GetFirstDots(_player.KeyGemsColor);

            var index = dots.IndexOf(dotSpecial.gameObject);
            if (index != GetDotsLastedIndex(dots))
            {
                //TODO: Create Reset Range Function
                ResetRangeSingleDots(_player.KeyGemsColor, index + 1, dots.Count);
                dots.Add(_firstDotPlayer);
            }
            else
            {
                dots.Add(_firstDotPlayer);
            }
        }

        protected override IEnumerator OnCompleted()
        {
            SingleDotsComplete();

            LevelManager.Instance.OnComplete();
            LevelManager.Instance.SavegameData();
            GameManager.Instance.SetGameState(GameState.CompleteState);

            yield return new WaitForSecondsRealtime(1f);

            StartCoroutine(LevelManager.Instance.LoadLevel());
        }
    }
}


