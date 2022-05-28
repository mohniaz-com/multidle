namespace Multidle.Core
{
    public class WaitingGameState : BaseGameState
    {
        public override void Enter(GameManager manager)
        {
        }

        public override void Update(GameManager manager)
        {
            if (manager.Configuration.waitForPlayers && manager.Players.Count > 1)
            {
                manager.SetGameState(GameState.GamePrep);
            }
            else if (!manager.Configuration.waitForPlayers && manager.Players.Count > 0)
            {
                manager.SetGameState(GameState.GamePrep);
            }
        }

        public override void Leave(GameManager manager)
        {
        }
    }
}
