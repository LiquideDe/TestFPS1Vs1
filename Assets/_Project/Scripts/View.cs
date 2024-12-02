using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

namespace FPS
{
    public class View : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textCounts, _textActions;   

        public void SetCounts(string text) => _textCounts.text = text;

        public void SetActionsText(string text) => _textActions.text = text;

    }
}

