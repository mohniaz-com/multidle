using UnityEngine;

namespace Multidle.Core
{
    public class RoundEndState : BaseGameState
    {
        public override void Enter(GameManager manager)
        {
            TimerManager.Instance.SetEndTime(Time.time + 11.0f);
        }

        public override void Update(GameManager manager)
        {
            if (TimerManager.Instance.IsComplete)
            {
                manager.SetGameState(GameState.RoundPrep);
            }
        }

        public override void Leave(GameManager manager)
        {
        }
    }
}
