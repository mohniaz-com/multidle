using Multidle.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Multidle.UI
{
    public class LetterController : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMP_Text text;
        [SerializeField] private int letterIndex;

        private void Update()
        {
            if (PlayerController.LocalPlayer == null) return;

            if (PlayerController.LocalPlayer.LastGuessedWord.Length != 5) return;

            text.text = PlayerController.LocalPlayer.LastGuessedWord[letterIndex].ToString();

            if (PlayerController.LocalPlayer.LastGuessedValidity[letterIndex] == 0)
                image.color = new Color(0.4980392f, 0.5490196f, 0.5529412f);
            else if (PlayerController.LocalPlayer.LastGuessedValidity[letterIndex] == 1)
                image.color = new Color(0.9529412f, 0.6117647f, 0.07058824f);
            else
                image.color = new Color(0.1529412f, 0.682353f, 0.3764706f);
        }
    }
}
