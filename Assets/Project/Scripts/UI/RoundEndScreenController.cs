using Multidle.Core;
using UnityEngine;

namespace Multidle.UI
{
    public class RoundEndScreenController : MonoBehaviour
    {
        private Animator animator;
        private Canvas canvas;

        private void Awake()
        {
            animator = GetComponent<Animator>();
            canvas = GetComponent<Canvas>();
        }

        private void Update()
        {
            if (GameManager.Instance == null)
            {
                animator.enabled = false;
                canvas.enabled = false;
                return;
            }

            if (GameManager.Instance.State == GameState.RoundEnd)
            {
                animator.enabled = true;
                canvas.enabled = true;
            }
            else
            {
                animator.enabled = false;
                canvas.enabled = false;
            }
        }
    }
}
