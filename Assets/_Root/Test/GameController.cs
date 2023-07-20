using System;
using _Root.Test.Words;
using TMPro;
using UnityEngine;

namespace _Root.Test
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private WordsSpawner _wordsSpawner;
        [SerializeField] private int _wordsCount;
        [SerializeField] private TMP_Text _mainWord;
        [SerializeField] private Hint.Hint _hint;
        private void Start()
        {
            _wordsSpawner.Hint = _hint;
            for (int i = 0; i < _wordsCount; i++)
            {
                _wordsSpawner.SpawnWord();
            }
            _wordsSpawner.SpawnMainWord();
        }
    }
}