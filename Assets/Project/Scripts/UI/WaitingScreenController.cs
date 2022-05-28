using Multidle.Core;
using UnityEngine;

namespace Multidle.UI
{
    public class WaitingScreenController : MonoBehaviour
    {
        private Canvas canvas;

        private void Awake()
        {
            canvas = GetComponent<Canvas>();
        }

        private void Update()
        {
            if (GameManager.Instance == null)
            {
                canvas.enabled = false;
                return;
            }

            if (GameManager.Instance.State == GameState.WaitingForPlayers)
                canvas.enabled = true;
            else
                canvas.enabled = false;
        }
    }
}
