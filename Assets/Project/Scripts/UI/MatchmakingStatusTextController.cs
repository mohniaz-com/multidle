using Multidle.Core;
using TMPro;
using UnityEngine;

namespace Multidle.UI
{
    public class MatchmakingStatusTextController : MonoBehaviour
    {
        private TMP_Text text;

        private void Awake()
        {
            text = GetComponent<TMP_Text>();
        }

        private void Update()
        {
            if (MatchmakingManager.Instance == null) return;

            switch (MatchmakingManager.Instance.MatchmakingStatus)
            {
                case "CreatingTicket":
                    text.text = "Creating matchmaking ticket";
                    break;
                case "WaitingForPlayers":
                    text.text = "Looking for match";
                    break;
                case "WaitingForMatch":
                    text.text = "Looking for match";
                    break;
                case "WaitingForServer":
                    text.text = "Waiting for server";
                    break;
                case "Matched":
                    text.text = "Match found";
                    break;
                case "Canceled":
                    text.text = "Matchmaking ticket canceled";
                    break;
                default:
                    text.text = "Creating matchmaking ticket";
                    break;
            }
        }
    }
}
