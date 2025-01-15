using UnityEngine;

namespace FPS
{
    public class TimeCounter : ViewCanDestroy
    {
        public float Timer { get; private set; }
        private bool _isGameStarted;

        void Update()
        {
            if (_isGameStarted)
                Timer += Time.deltaTime;
        }

        public void StopCount() => _isGameStarted = false;

        public void StartCount() => _isGameStarted = true;

        public void ResetTimer() => Timer = 0;
    }
}


