using System;
using System.Collections.Generic;
using _Root.Test.Words;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace _Root.Test.Hint
{
    public class Hint : MonoBehaviour
    {
        [SerializeField] private Image _hintImage;
        [SerializeField] private Button _button;
        public List<IHintable> WordsForHint = new List<IHintable>();
        private IHintable _currentCorrectWord;

        private void Start()
        {
            _button.onClick.AddListener(ShowHints);
        }

        public void FillHint()
        {
            _hintImage.fillAmount += 1f / 4f;
        }

        public void SetCorrectWord(IHintable correctWord)
        {
            _currentCorrectWord = correctWord;
        }
        
        public void ShowHints()
        {
            if (_hintImage.fillAmount > 0.99f)
            {
                int startHintIndex;
                int lastHintIndex;
                var indexOfCorrectWord = WordsForHint.IndexOf(_currentCorrectWord);
                startHintIndex = Random.Range(0, indexOfCorrectWord);
                lastHintIndex = Random.Range(0, 3) + indexOfCorrectWord;
                if (lastHintIndex - startHintIndex < 3)
                {
                    startHintIndex = lastHintIndex - 3;
                }
                else if (lastHintIndex - startHintIndex > 3)
                {
                    lastHintIndex = startHintIndex + 3;
                }
                if (!(indexOfCorrectWord - startHintIndex < 0) && !(startHintIndex > 0))
                {
                    lastHintIndex += lastHintIndex - startHintIndex;
                    startHintIndex = 0;
                }

                if (lastHintIndex > WordsForHint.Count - 1)
                {
                    lastHintIndex = WordsForHint.Count - 1;
                }

                for (int i = startHintIndex; i <= indexOfCorrectWord; i++)
                {
                    WordsForHint[i].ShowHint();
                }

                for (int i = indexOfCorrectWord; i <= lastHintIndex; i++)
                {
                    WordsForHint[i].ShowHint();
                }
            }

            _hintImage.fillAmount = 0f;
        }

        public void OffHints()
        {
            foreach (var hintable in WordsForHint)
            {
                if (hintable.Hinted)
                {
                    hintable.DisableHint();
                }
            }
        }
    }
}