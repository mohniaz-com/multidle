namespace Multidle.Core
{
    public abstract class BaseGameState
    {
        public static BaseGameState GetGameStateObject(GameState state)
        {
            switch (state)
            {
                case GameState.WaitingForPlayers:
                    return new WaitingGameState();
                case GameState.GamePrep:
                    return new GamePrepState();
                case GameState.RoundPrep:
                    return new RoundPrepState();
                case GameState.InGame:
                    return new InGameState();
                case GameState.RoundEnd:
                    return new RoundEndState();
                default:
                    return new WaitingGameState();
            }
        }

        public abstract void Enter(GameManager manager);

        public abstract void Update(GameManager manager);

        public abstract void Leave(GameManager manager);
    }
}
