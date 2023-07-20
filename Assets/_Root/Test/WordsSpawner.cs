using System;
using System.Collections.Generic;
using System.Linq;
using _Root.Test.Words;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace _Root.Test
{
    public class WordsSpawner : MonoBehaviour
    {
        [SerializeField] private WordsScriptableObject _wordsScriptableObject;
        [SerializeField] private WordInList _wordInListPrefab;
        [SerializeField] private MainWord _mainWordPrefab;
        [SerializeField] private Transform _content;
        [SerializeField] private Transform _mainWordPlace;
        public Hint.Hint Hint;
        private List<Word> _words;
        private List<WordInList> _instantiatedWords = new List<WordInList>();
        private MainWord _instantiatedMainWord;
        private void Start()
        {
            _words = _wordsScriptableObject.WordsList.ToList();
        }

        public void SpawnWord()
        {
            if (_instantiatedMainWord == null)
            {
                _instantiatedMainWord = Instantiate(_mainWordPrefab, _mainWordPlace);
                _instantiatedMainWord.gameObject.SetActive(false);
            }
            var index = Random.Range(0, _words.Count);
            var go = Instantiate(_wordInListPrefab, _content);
            go.Initialize(_words[index].FirstLanguage, _words[index].SecondLanguage, _instantiatedMainWord);
            go.SetText();
            go.OnWordDestroy += RemoveWordFromList;
            _instantiatedWords.Add(go);
            Hint.WordsForHint.Add(go);
            _words.RemoveAt(index);
        }

        private void RemoveWordFromList(WordInList obj)
        {
            _instantiatedWords.Remove(obj);
            Hint.WordsForHint.Remove(obj);
            if (_words.Count > 0)
            {
                SpawnWord();
            }
        }

        public void SpawnMainWord()
        {
            var index = Random.Range(0, _instantiatedWords.Count);
            _instantiatedMainWord.gameObject.SetActive(true);
            _instantiatedMainWord.Hint = Hint;
            _instantiatedMainWord.Initialize(_instantiatedWords);
            
        }

        public string GetText()
        {
            var index = Random.Range(0, _instantiatedWords.Count);
            return _instantiatedWords[index].FirstLanguage;
        }
    }
}