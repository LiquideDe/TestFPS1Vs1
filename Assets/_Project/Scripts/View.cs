using UnityEngine;
using TMPro;

namespace FPS
{
    public class View : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCounts; 

        public void SetCounts(string text) => _textCounts.text = text;
    }
}

