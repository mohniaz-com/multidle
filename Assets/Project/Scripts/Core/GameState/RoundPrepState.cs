using UnityEngine;

namespace Multidle.Core
{
    public class RoundPrepState : BaseGameState
    {
        public override void Enter(GameManager manager)
        {
            GameManager.Instance.CurrentWord = WordListManager.Instance.WordList[Random.Range(0, WordListManager.Instance.WordList.Count)].ToUpper();
            TimerManager.Instance.SetEndTime(Time.time + 6.0f);
        }

        public override void Update(GameManager manager)
        {
            if (TimerManager.Instance.IsComplete)
            {
                manager.SetGameState(GameState.InGame);
            }
        }

        public override void Leave(GameManager manager)
        {
        }
    }
}
