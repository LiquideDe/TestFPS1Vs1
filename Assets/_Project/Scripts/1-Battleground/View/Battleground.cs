using System.Collections.Generic;
using UnityEngine;

namespace FPS
{
    public class Battleground : MonoBehaviour
    {
        [field: SerializeField] public List<OrientPoint> _orientPoints { get; private set; }
        [field: SerializeField] public Transform _playerStartPosition { get; private set; }
        [field: SerializeField] public Transform _aiStartPostition { get; private set; }
    }
}

