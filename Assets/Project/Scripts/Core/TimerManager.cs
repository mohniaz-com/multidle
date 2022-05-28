using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace Multidle.Core
{
    public class TimerManager : NetworkBehaviour
    {
        public static TimerManager Instance { get; private set; }

        [SyncVar] public float CurrentTime;
        public float EndTime;

        public bool IsComplete => Time.time >= EndTime;

        public void SetEndTime(float time)
        {
            EndTime = time;
        }

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            Instance = this;
        }

        public override void OnStopNetwork()
        {
            base.OnStopNetwork();
            if (Instance == this) Instance = null;
        }

        private void Update()
        {
            if (!IsServer) return;

            CurrentTime = EndTime - Time.time;
            if (CurrentTime < 0) CurrentTime = 0;
        }
    }
}
