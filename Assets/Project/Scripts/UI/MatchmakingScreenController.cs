using Multidle.Core;
using UnityEngine;

namespace Multidle.UI
{
    public class MatchmakingScreenController : MonoBehaviour
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
            if (MatchmakingManager.Instance == null)
            {
                animator.enabled = false;
                canvas.enabled = false;
                return;
            }

            if (MatchmakingManager.Instance.IsMatchmaking)
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
