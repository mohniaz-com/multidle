using UnityEngine;

namespace Multidle.Core
{
    public class InGameState : BaseGameState
    {
        public override void Enter(GameManager manager)
        {
            foreach (var player in manager.Players)
            {
                player.RoundProgress = 0;
                player.LastGuessedValidity[0] = 0;
                player.LastGuessedValidity[1] = 0;
                player.LastGuessedValidity[2] = 0;
                player.LastGuessedValidity[3] = 0;
                player.LastGuessedValidity[4] = 0;
            }
            TimerManager.Instance.SetEndTime(Time.time + 120.0f);
        }

        public override void Update(GameManager manager)
        {
            if (manager.Configuration.waitForPlayers && manager.Players.Count < 2)
            {
                manager.EndGame();
            }

            if (manager.Players[0].RoundProgress >= 5 && (manager.Players.Count > 1 && manager.Players[1].RoundProgress >= 5))
            {
                manager.RoundNumber++;
                manager.SetGameState(GameState.RoundEnd);

                return;
            }

            if (manager.Players[0].RoundProgress >= 5)
            {
                manager.Players[0].RoundWins++;
                manager.RoundNumber++;
                manager.SetGameState(GameState.RoundEnd);

                return;
            }

            if (manager.Players.Count > 1 && manager.Players[1].RoundProgress >= 5)
            {
                manager.Players[1].RoundWins++;
                manager.RoundNumber++;
                manager.SetGameState(GameState.RoundEnd);

                return;
            }

            if (TimerManager.Instance.IsComplete)
            {
                if (manager.Players.Count < 2)
                {
                    manager.Players[0].RoundWins += 1;
                }
                else if (manager.Players[0].RoundProgress > manager.Players[1].RoundProgress)
                {
                    manager.Players[0].RoundWins += 1;
                }
                else if (manager.Players[0].RoundProgress < manager.Players[1].RoundProgress)
                {
                    manager.Players[1].RoundWins += 1;
                }
                else
                {
                    // Sudden death
                    return;
                }

                manager.RoundNumber++;
                manager.SetGameState(GameState.RoundEnd);
            }
        }

        public override void Leave(GameManager manager)
        {
        }
    }
}
