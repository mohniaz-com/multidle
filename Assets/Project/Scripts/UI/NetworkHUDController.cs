using FishNet;
using UnityEngine;

namespace Multidle.UI
{
    public class NetworkHUDController : MonoBehaviour
    {
        public void ServerStartOnClick()
        {
            Debug.Log("Server start requested from NetworkHUD");
            if (InstanceFinder.ServerManager == null) return;
            InstanceFinder.ServerManager.StartConnection();
        }

        public void ClientStartOnClick()
        {
            Debug.Log("Client start requested from NetworkHUD");
            if (InstanceFinder.ClientManager == null) return;
            InstanceFinder.ClientManager.StartConnection();
        }
    }
}
