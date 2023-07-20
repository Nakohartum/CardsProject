using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace _Root.Test.Words
{
    public class MainWord : MonoBehaviour
    {
        private TMP_Text _text;
        public string FirstLanguage { get; private set; }
        public string SecondLanguage { get; private set; }
        private List<WordInList> _wordsInList;
        private Coroutine _coroutine;
        private float _fadeOutTime = 5f;
        public Hint.Hint Hint;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void Initialize(List<WordInList> wordsInList)
        {
            _wordsInList = wordsInList;
            ChangeWord();
        }

        public void ChangeWord()
        {
            if (_wordsInList.Count > 0)
            {
                var index = Random.Range(0, _wordsInList.Count);
                FirstLanguage = _wordsInList[index].FirstLanguage;
                SecondLanguage = _wordsInList[index].SecondLanguage;
                Hint.SetCorrectWord(_wordsInList[index]);
                SetText(FirstLanguage);
                StartFadingOut();
            }
            else
            {
                StopAllCoroutines();
                _text.alpha = 1f;
                SetText("You've won");
            }
            Hint.OffHints();
        }

        private void SetText(string text)
        {
            _text.text = text;
        }

        private IEnumerator FadeOut()
        {
            float timer = _fadeOutTime;
            yield return null;
            while (timer > 0f)
            {
                timer -= Time.deltaTime;
                _text.alpha = Mathf.Lerp(1, 0, 1/timer);
                yield return null;
            }
            
            ChangeWord();
        }

        private void StartFadingOut()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _text.alpha = 1f;
            }

            _coroutine = StartCoroutine(FadeOut());
        }
    }
}