using Multidle.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Multidle.UI
{
    public class ProgressIndicatorController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private int letterIndex;
        [SerializeField] private bool isEnemy;

        private void Update()
        {
            var player = isEnemy ? PlayerController.EnemyPlayer : PlayerController.LocalPlayer;

            if (player == null) return;

            if (player.LastGuessedWord.Length != 5) return;

            if (player.RoundProgress > letterIndex)
                image.color = new Color(0.1529412f, 0.682353f, 0.3764706f);
            else
                image.color = new Color(0.4980392f, 0.5490196f, 0.5529412f);
        }
    }
}