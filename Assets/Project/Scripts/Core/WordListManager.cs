using Multidle;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Multidle.Core
{
    public class WordListManager : Singleton<WordListManager>
    {
        public List<string> WordList = new List<string>();

        private void Start()
        {
            StartCoroutine(LoadWordListCoroutine());
        }

        private IEnumerator LoadWordListCoroutine()
        {
            using (var www = UnityWebRequest.Get(Application.streamingAssetsPath + "/valid_words.txt"))
            {
                yield return www.SendWebRequest();
                var text = www.downloadHandler.text;
                WordList = text.Split('\n').ToList();
                yield return null;
            }
        }
    }
}