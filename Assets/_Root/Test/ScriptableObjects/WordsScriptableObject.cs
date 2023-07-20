using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Root.Test
{
    [CreateAssetMenu(fileName = nameof(WordsScriptableObject), menuName = "Words/"+nameof(WordsScriptableObject), order = 0)]
    public class WordsScriptableObject : ScriptableObject
    {
        public Word[] WordsList;
    }

    [Serializable]
    public class Word
    {
        public string FirstLanguage;
        public string SecondLanguage;
    }
}