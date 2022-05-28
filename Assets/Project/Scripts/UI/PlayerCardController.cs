using Multidle.Core;
using TMPro;
using UnityEngine;

namespace Multidle.UI
{
    public class PlayerCardController : MonoBehaviour
    {
        [SerializeField]
        private bool isEnemyCard = false;

        private TMP_Text text;

        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            if ((isEnemyCard && PlayerController.EnemyPlayer == null) || PlayerController.LocalPlayer == null)
                return;

            if (isEnemyCard)
                text.text = PlayerController.EnemyPlayer.PlayerName;
            else
                text.text = PlayerController.LocalPlayer.PlayerName;
        }
    }
}
