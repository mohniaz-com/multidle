using Multidle.Core;
using UnityEngine;
using UnityEngine.UI;

namespace Multidle.UI
{
    public class LogInScreenController : MonoBehaviour
    {
        //private Animator animator;
        private Canvas canvas;
        private InputField usernameInputField;

        public void OnLoginButtonClicked()
        {
            var username = usernameInputField.text;
            ApplicationManager.Instance.Login(username);
        }

        private void Awake()
        {
            //animator = GetComponent<Animator>();
            canvas = GetComponent<Canvas>();
            usernameInputField = GetComponentInChildren<InputField>();
        }

        private void Update()
        {
            if (ApplicationManager.Instance == null)
            {
                //animator.enabled = false;
                canvas.enabled = false;
                return;
            }

            if (!ApplicationManager.Instance.IsLoggedIn)
            {
                //animator.enabled = true;
                canvas.enabled = true;
            }
            else
            {
                //animator.enabled = false;
                canvas.enabled = false;
            }
        }
    }
}
