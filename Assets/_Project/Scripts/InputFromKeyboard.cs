using UnityEngine;
using System;

namespace FPS
{
    public class InputFromKeyboard : MonoBehaviour
    {
        public event Action Forward;
        public event Action Back;
        public event Action TurnRight;
        public event Action TurnLeft;
        public event Action StopMove;
        public event Action StopRotate;
        public event Action Fire;

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) StopMove?.Invoke();

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A)) StopRotate?.Invoke();

            if (Input.GetKeyDown(KeyCode.W)) Forward?.Invoke();

            if (Input.GetKeyDown(KeyCode.S)) Back?.Invoke();

            if (Input.GetKeyDown(KeyCode.D)) TurnRight?.Invoke();
            
            if (Input.GetKeyDown(KeyCode.A)) TurnLeft?.Invoke();

            if (Input.GetKeyDown(KeyCode.Space)) Fire?.Invoke();
        }
    }
}



