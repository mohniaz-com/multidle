using FishNet;
using UnityEngine;

namespace Multidle.Core
{
    public class GameStarter : MonoBehaviour
    {
        private void Start()
        {
            if (ApplicationManager.Instance == null) return;

            if (ApplicationManager.Instance.MatchReady && ApplicationManager.Instance.IsMatchServer)
                InstanceFinder.ServerManager.StartConnection();
            else if (ApplicationManager.Instance.MatchReady)
                InstanceFinder.ClientManager.StartConnection(ApplicationManager.Instance.MatchServerIP, (ushort)ApplicationManager.Instance.MatchServerPort);
        }
    }
}
