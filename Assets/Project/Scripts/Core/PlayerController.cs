using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine;

namespace Multidle.Core
{
    public class PlayerController : NetworkBehaviour
    {
        public static PlayerController LocalPlayer { get; private set; }

        /// <summary>
        /// Only valid in two-player contexts
        /// </summary>
        public static PlayerController EnemyPlayer { get; private set; }

        [SyncVar] public string PlayerName = "Player";
        [SyncVar] public int RoundWins = 0;
        [SyncVar] public int RoundProgress = 0;
        [SyncVar] public string LastGuessedWord = "";
        [SyncObject] public readonly SyncList<int> LastGuessedValidity = new SyncList<int> { 0, 0, 0, 0, 0 };

        public override void OnStartClient()
        {
            base.OnStartClient();

            if (IsOwner)
                LocalPlayer = this;
            else
                EnemyPlayer = this;

            if (IsOwner) UpdatePlayerName(ApplicationManager.Instance.Username);
        }

        public override void OnStartNetwork()
        {
            base.OnStartNetwork();
            GameManager.Instance.Players.Add(this);
        }

        public override void OnStopNetwork()
        {
            base.OnStopNetwork();
            if (GameManager.Instance == null) return;
            GameManager.Instance.Players.Remove(this);
        }

        [ServerRpc]
        public void GuessWord(string word)
        {
            if (!string.IsNullOrEmpty(word) && word.Length == 5 && WordListManager.Instance.WordList.Contains(word))
            {
                var progress = 0;
                word = word.ToUpper();
                Debug.Log(PlayerName + " is guessing " + word);
                for (var i = 0; i < word.Length; i++)
                {
                    if (word[i] == GameManager.Instance.CurrentWord[i])
                    {
                        LastGuessedValidity[i] = 2;
                        progress++;
                    }
                    else if (GameManager.Instance.CurrentWord.Contains(word[i].ToString()))
                    {
                        LastGuessedValidity[i] = 1;
                    }
                    else
                    {
                        LastGuessedValidity[i] = 0;
                    }
                }
                if (progress > RoundProgress) RoundProgress = progress;
                LastGuessedWord = word;
            }
        }

        [ServerRpc]
        private void UpdatePlayerName(string newName)
        {
            if (!string.IsNullOrEmpty(newName))
                PlayerName = newName;
        }

        private void Update()
        {
            if (!IsOwner) return;

            if (Input.GetKeyDown(KeyCode.Return))
            {
                var word = Multidle.UI.WordInputFieldController.Instance.GetComponent<UnityEngine.UI.InputField>().text;
                if (word.Length == 5)
                {
                    GuessWord(word);
                    Multidle.UI.WordInputFieldController.Instance.GetComponent<UnityEngine.UI.InputField>().text = "";
                }
            }
        }
    }
}