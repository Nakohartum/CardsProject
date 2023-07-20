using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace _Root.Test.Words
{
    public class WordInList : MonoBehaviour, IHintable
    {
        private TMP_Text _text;
        public string FirstLanguage { get; private set; }
        public string SecondLanguage { get; private set; }
        private Button _button;
        private MainWord _mainWord;
        public event Action<WordInList> OnWordDestroy = list => { };
        private Color _hintColor = Color.red;
        private Color _defaultColor = Color.white;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _button = GetComponent<Button>();
            _text.color = _defaultColor;
        }

        public void Initialize(string firstLanguage, string secondLanguage, MainWord mainWord)
        {
            FirstLanguage = firstLanguage;
            SecondLanguage = secondLanguage;
            _mainWord = mainWord;
            _button.onClick.AddListener(CheckWord);
        }

        private void CheckWord()
        {
            if (_mainWord.FirstLanguage == FirstLanguage)
            {
                OnWordDestroy.Invoke(this);
                _mainWord.ChangeWord();
                Destroy(gameObject);
                _mainWord.Hint.FillHint();
            }
            _mainWord.Hint.OffHints();
        }
        
        public void SetText()
        {
            _text.SetText(SecondLanguage);
        }

        public bool Hinted { get; set; } = false;
        public bool Correct { get; set; }

        public void ShowHint()
        {
            _text.color = _hintColor;
            Hinted = true;
        }

        public void DisableHint()
        {
            _text.color = _defaultColor;
            Hinted = false;
        }
    }
}