using System.Collections;
using System.Collections.Generic;
using Unboxed.Manager;
using Unboxed.Utility;
using UnityEngine;

namespace Unboxed.Puzzle
{
    public class Dot : MonoBehaviour
    {
        public enum DotState
        {
            Empty,
            Filled,
            Complete
        }

        public int Index => _index;
        public GemsColor GemsColor => GetGemsColor();
        public GemsColor KeyGemsColor => GetKeyGemsColor();
        public DotState State => _dotState;

        public bool IsStarter => _isStarter;
        public bool IsSpecial => _isSpecial;
        public bool IsSecondary => _isSecondary;
        public int FilledSlot => _filledSlot;

        [SerializeField] protected int _maxFilledSlot = 1;
        [SerializeField] protected GemsColor _gemsColor;
        [SerializeField] protected bool _isStarter;
        [SerializeField] protected bool _isSpecial;

        protected int _index;
        protected Animator _animator;
        protected GemsColor _starterColor;
        protected GemsColor _starterKeyColor;
        protected GemsColor _keyGemsColor;
        protected DotState _dotState = DotState.Empty;
        protected int _filledSlot = 0;
        protected bool _isSecondary = false;

        protected List<GemsColor> _gemsColors = new List<GemsColor>();
        protected List<GemsColor> _keyGemsColors = new List<GemsColor>();

        protected void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void OnSpawn()
        {
            SpawnDot();
        }

        public void OnEnter()
        {
            EnterDot();
        }

        public void OnExit()
        {
            ExitDot();
        }

        public void OnComplete()
        {
            CompleteDot();
        }

        public void InitDot()
        {
            for (int i = 0; i < _maxFilledSlot; i++)
            {
                _gemsColors.Add(GemsColor.Empty);
                _keyGemsColors.Add(GemsColor.Empty);
            }

            SetGemsColor(_gemsColor);
            SetKeyGemsColor(_gemsColor);
            SetStarterColor(_gemsColor);

            Debug.LogWarning($"Count GS {_gemsColors.Count} and KGS {_keyGemsColors.Count}");
        }

        protected virtual void Start()
        {
            //TODO: Check start
        }

        protected virtual void SpawnDot()
        {
            //TODO: Check spawn condition
        }

        protected virtual void EnterDot()
        {
            //TODO: Check enter condition
        }

        protected virtual void ExitDot()
        {
            //TODO: Check exit condition
        }

        protected virtual void CompleteDot()
        {
            //TODO: Check complete condition
        }

        protected virtual void PlaySpawnAnimation()
        {
            //TODO: Trigger spawn animation
        }

        protected virtual void PlayFillAnimation()
        {
            //TODO: Trigger fill animation
        }

        protected virtual void PlayClearAniamtion()
        {
            //TODO: Trigger clear animation
        }

        protected virtual void PlayCompleteAnimation()
        {
            //TODO: Trigger complete animation
        }

        public void SetIndex(int index)
        {
            _index = index;
        }

        public void SetGemsColor(GemsColor gemsColor)
        {
            _gemsColor = gemsColor;
            _gemsColors[_filledSlot] = gemsColor;
        }

        public void SetGemsColorByIndex(int index, GemsColor gemsColor)
        {
            _gemsColors[index] = gemsColor;
        }

        public void SetKeyGemsColor(GemsColor gemsColor)
        {
            _keyGemsColor = gemsColor;
            _keyGemsColors[_filledSlot] = gemsColor;
        }

        public void SetStarterColor(GemsColor gemsColor)
        {
            _starterColor = gemsColor;
            _starterKeyColor = gemsColor;
        }

        //public void SetKeyGemsColor(GemsColor gemsColor)
        //{
        //    _keyGemsColor = gemsColor;
        //}

        public void SetKeyGemsColorByIndex(int index, GemsColor gemsColor)
        {
            _keyGemsColors[index] = gemsColor;
        }

        public void SetIsSecondary(bool value)
        {
            _isSecondary = value;
        }

        public void ResetDotColor()
        {
            _gemsColor = _starterColor;
            _keyGemsColor = _starterKeyColor;
        }

        public void ResetDotColorByIndex(int index)
        {
            _gemsColors[index] = _starterColor;
            _keyGemsColors[index] = _starterKeyColor;
        }

        public GemsColor GetGemsColor()
        {
            Debug.LogWarning($"{name} GCs {_gemsColors.Count} | Filled ID: {_filledSlot} | State: {_dotState}");
            //GemsColor gemsColor = _dotState switch
            //{
            //    DotState.Empty => _gemsColors[_filledSlot],
            //    DotState.Filled => _gemsColors[_filledSlot - 1],
            //    DotState.Complete => _gemsColors[_filledSlot - 1],
            //    _ => _starterColor
            //};

            //return gemsColor;

            if (!IsSpecial)
            {
                return _gemsColors[0];
            }
            else
            {
                return GetLastedGemsColors();
            }
        }

        public GemsColor GetGemsColorByIndex(int index)
        {
            return _gemsColors[index];
        }

        public GemsColor GetKeyGemsColor()
        {
            Debug.LogWarning($"{name} KGCs {_keyGemsColors.Count} | Filled ID: {_filledSlot} | State: {_dotState}");
            //GemsColor gemsColor = _dotState switch
            //{
            //    DotState.Empty => _keyGemsColors[_filledSlot],
            //    DotState.Filled => _keyGemsColors[_filledSlot - 1],
            //    DotState.Complete => _keyGemsColors[_filledSlot - 1],
            //    _ => _starterKeyColor
            //};

            //return gemsColor;

            if (!IsSpecial)
            {
                return _keyGemsColors[0];
            }
            else
            {
                return GetLastedKeyGemsColors();
            }
        }

        private GemsColor GetLastedGemsColors()
        {
            for (int i = 0; i < _gemsColors.Count; i++)
            {
                if (_gemsColors[i] != GemsColor.Empty)
                {
                    return _gemsColors[i];
                }
            }

            return GemsColor.Empty;
        }

        private GemsColor GetLastedKeyGemsColors()
        {
            for (int i = 0; i < _keyGemsColors.Count; i++)
            {
                if (_keyGemsColors[i] != GemsColor.Empty)
                {
                    return _keyGemsColors[i];
                }
            }

            return GemsColor.Empty;
        }

        protected void SetSpriteColor(GameObject dot, GemsColor gemsColor)
        {
            Debug.Log($"{dot.name} set color {gemsColor}");
            SpriteRenderer spriteRenderer = dot.GetComponent<SpriteRenderer>();
            spriteRenderer.color = UnboxedUtility.GetZeroToOneRangeColor(gemsColor);
            spriteRenderer.material.color = UnboxedUtility.GetZeroToOneRangeColor(gemsColor);
        }

        protected bool IsEnter()
        {
            return _dotState == DotState.Empty && _filledSlot < _maxFilledSlot;
        }

        protected bool IsExit()
        {
            return _filledSlot > 0;
        }

        protected bool IsComplete()
        {
            return _dotState == DotState.Filled && _filledSlot == _maxFilledSlot;
        }

        protected void ChangeGlowColor(GemsColor gemsColor)
        {
            SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.material.color = UnboxedUtility.GetZeroToOneRangeColor(gemsColor);
        }
    }
}

