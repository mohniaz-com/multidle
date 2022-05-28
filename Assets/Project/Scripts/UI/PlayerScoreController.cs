using Multidle.Core;
using TMPro;
using UnityEngine;

namespace Multidle.UI
{
    public class PlayerScoreController : MonoBehaviour
    {
        [SerializeField]
        private bool isEnemyScore = false;

        private TMP_Text text;

        private void Awake()
        {
            if (GetComponentInChildren<TMP_Text>() != null)
            {
                text = GetComponentInChildren<TMP_Text>();
            }
            else
            {
                text = GetComponent<TMP_Text>();
            }
        }

        private void Update()
        {
            if ((isEnemyScore && PlayerController.EnemyPlayer == null) || PlayerController.LocalPlayer == null)
                return;

            if (isEnemyScore)
                text.text = PlayerController.EnemyPlayer.RoundWins.ToString();
            else
                text.text = PlayerController.LocalPlayer.RoundWins.ToString();
        }
    }
}
