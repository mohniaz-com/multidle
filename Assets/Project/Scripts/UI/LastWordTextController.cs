using Multidle.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Multidle.UI
{
    public class LastWordTextController : MonoBehaviour
    {
        private Text text;

        private void Awake()
        {
            text = GetComponentInChildren<Text>();
        }

        private void Update()
        {
            if (GameManager.Instance == null || string.IsNullOrEmpty(GameManager.Instance.CurrentWord))
                return;

            text.text = "Word was: " + GameManager.Instance.CurrentWord;
        }
    }
}
