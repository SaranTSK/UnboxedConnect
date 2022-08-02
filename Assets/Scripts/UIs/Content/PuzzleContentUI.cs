using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unboxed.Interface;
using TMPro;
using Unboxed.Manager;
using Unboxed.Scriptable;

namespace Unboxed.UI
{
    public class PuzzleContentUI : MonoBehaviour, IShowable
    {
        [SerializeField] Transform _conditionPanel;
        [SerializeField] Transform _hintText;

        private Transform[] _conditions;
        private Dictionary<GemsColor, int> _conditionIndex;
        private PuzzleScriptable _puzzleScriptable;

        private void Start()
        {
            //InitConditionPanel();
        }

        public void InitConditionPanel(GameMode gameMode)
        {
            _conditions = new Transform[_conditionPanel.childCount];
            _conditionIndex = new Dictionary<GemsColor, int>();

            for (int i = 0; i < _conditionPanel.childCount; i++)
            {
                _conditions[i] = _conditionPanel.GetChild(i);
            }

            _conditionPanel.gameObject.SetActive(gameMode == GameMode.Condition);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void LoadContent(PuzzleScriptable puzzle)
        {
            _hintText.gameObject.SetActive(puzzle.hintText.Length > 0);
            SetHintText(puzzle.hintText);
        }

        private void SetHintText(string text)
        {
            if (_hintText.TryGetComponent(out TextMeshProUGUI hintText))
            {
                hintText.text = text;
            }
        }
    }
}


