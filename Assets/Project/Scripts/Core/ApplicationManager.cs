using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Multidle.Core
{
    public class ApplicationManager : MonoBehaviour
    {
        public static ApplicationManager Instance { get; private set; }

        public string Username = "Guest";

        public bool IsLoggedIn = false;
        public string EntityId;
        public string EntityType;
        public string EntityToken;

        public bool MatchReady = false;
        public bool IsMatchServer = false;
        public string MatchServerIP = "127.0.0.1";
        public int MatchServerPort = 1234;

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
#if ENABLE_PLAYFABSERVER_API
            PlayFabMultiplayerAgentAPI.OnShutDownCallback += OnPlayFabShutdown;
            PlayFabMultiplayerAgentAPI.Start();
#endif

            if (Application.isBatchMode)
            {
                Debug.Log("Running in server mode");
                MatchReady = true;
                IsMatchServer = true;
                SceneManager.LoadScene("Game");
            }
        }

        public void Login(string username)
        {
            // Log into PlayFab
            Username = username;
            var request = new LoginWithCustomIDRequest { CustomId = Random.value.ToString(), CreateAccount = true };
            PlayFabClientAPI.LoginWithCustomID(request, (LoginResult r) =>
            {
                EntityId = r.EntityToken.Entity.Id;
                EntityType = r.EntityToken.Entity.Type;
                EntityToken = r.EntityToken.EntityToken;
                IsLoggedIn = true;
            }, (PlayFabError e) =>
            {
                Debug.LogError("Log in failed");
            });
        }

        private void OnPlayFabShutdown()
        {
            Application.Quit();
        }
    }
}
