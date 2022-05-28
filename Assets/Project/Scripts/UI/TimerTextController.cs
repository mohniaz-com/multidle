using Multidle.Core;
using System;
using TMPro;
using UnityEngine;

namespace Multidle.UI
{
    public class TimerTextController : MonoBehaviour
    {
        private TMP_Text text;
        private TimeSpan ts;

        private void Awake()
        {
            text = GetComponentInChildren<TMP_Text>();
        }

        private void Update()
        {
            if (TimerManager.Instance == null)
                return;

            //var timeDiff = TimerManager.Instance.EndTime - Time.time;
            //if (timeDiff < 0) timeDiff = 0;
            ts = TimeSpan.FromSeconds(TimerManager.Instance.CurrentTime);
            text.text = ts.ToString("m\\:ss");
        }
    }
}
