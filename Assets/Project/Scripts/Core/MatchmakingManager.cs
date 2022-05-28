using PlayFab;
using PlayFab.MultiplayerModels;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multidle.Core
{
    public class MatchmakingManager : Singleton<MatchmakingManager>
    {
        [SerializeField]
        private bool isMatchmaking = false;
        [SerializeField]
        private string matchmakingTicketId;
        [SerializeField]
        private string matchId;
        [SerializeField]
        private string matchmakingStatus;

        public bool IsMatchmaking => isMatchmaking;
        public string MatchmakingStatus => matchmakingStatus;

        public void StartMatchmaking()
        {
            matchmakingStatus = "CreatingTicket";
            PlayFabMultiplayerAPI.CreateMatchmakingTicket
            (
                new CreateMatchmakingTicketRequest
                {
                    Creator = new MatchmakingPlayer
                    {
                        Entity = new EntityKey
                        {
                            Id = ApplicationManager.Instance.EntityId,
                            Type = ApplicationManager.Instance.EntityType
                        },

                        Attributes = new MatchmakingPlayerAttributes
                        {
                            DataObject = new
                            {
                                Latencies = new object[]
                                {
                                    new
                                    {
                                        region = "EastUs",
                                        latency = 10
                                    }
                                }
                            }
                        },
                    },
                    GiveUpAfterSeconds = 120,
                    QueueName = "QuickMatch"
                },
                OnMatchmakingTicketCreated,
                OnMatchmakingError
            );
        }

        private void CheckMatchmakingStatus()
        {
            if (!isMatchmaking) return;

            Debug.Log("Polling matchmaking status");

            PlayFabMultiplayerAPI.GetMatchmakingTicket
            (
                new GetMatchmakingTicketRequest
                {
                    TicketId = matchmakingTicketId,
                    QueueName = "QuickMatch"
                },
                OnMatchmakingTicketStatusReceived,
                OnMatchmakingError
            );
        }

        private void GetMatch()
        {
            PlayFabMultiplayerAPI.GetMatch
            (
                new GetMatchRequest
                {
                    MatchId = matchId,
                    QueueName = "QuickMatch"
                },
                OnMatchInformationReceived,
                OnMatchmakingError
            );
        }

        private void OnMatchmakingTicketCreated(CreateMatchmakingTicketResult result)
        {
            isMatchmaking = true;
            matchmakingTicketId = result.TicketId;
            StartCoroutine(MatchmakingPollingCoroutine());
            CheckMatchmakingStatus();
            Debug.Log("Matchmaking ticket created");
        }

        private void OnMatchmakingTicketStatusReceived(GetMatchmakingTicketResult result)
        {
            matchmakingStatus = result.Status;
            if (result.Status == "Matched")
            {
                isMatchmaking = false;
                matchId = result.MatchId;
                GetMatch();
            }
            Debug.Log("Matchmaking ticket status polled, status: " + result.Status);
        }

        private void OnMatchInformationReceived(GetMatchResult result)
        {
            ApplicationManager.Instance.MatchServerIP = result.ServerDetails.IPV4Address;
            ApplicationManager.Instance.MatchServerPort = result.ServerDetails.Ports[0].Num;
            ApplicationManager.Instance.MatchReady = true;
            Debug.Log("Match information received: " + ApplicationManager.Instance.MatchServerIP + ":" + ApplicationManager.Instance.MatchServerPort.ToString());
            SceneManager.LoadScene("Game");
        }

        private IEnumerator MatchmakingPollingCoroutine()
        {
            while (isMatchmaking)
            {
                yield return new WaitForSeconds(8);
                CheckMatchmakingStatus();
            }
            yield return null;
        }

        private void OnMatchmakingError(PlayFabError error)
        {
            Debug.Log("Matchmaking failed");
            isMatchmaking = false;
        }
    }
}
