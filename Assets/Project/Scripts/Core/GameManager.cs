using FishNet.Object;
using FishNet.Object.Synchronizing;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Multidle.Core
{
    public enum GameState
    {
        WaitingForPlayers,
        GamePrep,
        RoundPrep,
        InGame,
        RoundEnd
    }

    [Serializable]
    public class GameManagerConfiguration
    {
        public bool waitForPlayers = true;
    }

    public class GameManager : NetworkBehaviour
    {
        public static GameManager Instance { get; private set; }

        public GameManagerConfiguration Configuration;

        [SyncVar] public string CurrentWord = "ABCDE";
        [SyncVar] public GameState State = GameState.WaitingForPlayers;
        [SyncVar] public List<PlayerController> Players = new List<PlayerController>();
        [SyncVar] public int RoundNumber = 1;

        // Finite State Machine (FSM)
        private BaseGameState gameStateObject = new WaitingGameState();

        public void SetGameState(GameState nState)
        {
            State = nState;
            gameStateObject.Leave(this);
            gameStateObject = BaseGameState.GetGameStateObject(State);
            gameStateObject.Enter(this);
        }

        public void EndGame()
        {
            if (IsServer)
                ServerManager.StopConnection(true);
        }

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            Instance = this;
#if ENABLE_PLAYFABSERVER_API
            PlayFab.PlayFabMultiplayerAgentAPI.ReadyForPlayers();
#endif
        }

        public override void OnStopNetwork()
        {
            base.OnStopNetwork();
            if (Instance == this) Instance = null;

            // Terminate app if server mode
            if (Application.isBatchMode)
            {
                Debug.Log("Terminating app because running in server mode");
                Application.Quit();
            }
            else
            {
                UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
            }
        }

        private void Update()
        {
            if (IsServer)
                gameStateObject.Update(this);
        }
    }
}