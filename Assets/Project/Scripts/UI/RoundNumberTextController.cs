using Multidle.Core;
using TMPro;
using UnityEngine;

namespace Multidle.UI
{
    public class RoundNumberTextController : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            if (GameManager.Instance == null)
                return;

            text.text = "Round " + GameManager.Instance.RoundNumber.ToString();
        }
    }
}
