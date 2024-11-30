using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class View : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textCounts, _textActions;
    [SerializeField] private Button _buttonStart, _buttonStop;

    public Button ButtonStart => _buttonStart;
    public Button ButtonStop => _buttonStop; 
    public TextMeshProUGUI TextActions => _textActions;
    public TextMeshProUGUI TextCounts => _textCounts;

    public event Action StartGame, StopGame;


    private void OnEnable()
    {
        _buttonStart.onClick.AddListener(StartPressed);
        _buttonStop.onClick.AddListener(StopPressed);
    }    

    private void OnDisable()
    {
        _buttonStart.onClick.RemoveAllListeners();
        _buttonStop.onClick.RemoveAllListeners();
    }

    private void StopPressed() => StopGame?.Invoke();

    private void StartPressed() => StartGame?.Invoke();
}
