using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class ViewLog : ViewCanDestroy
{
    [SerializeField] private TextMeshProUGUI _textActionsLog;
    [SerializeField] private Scrollbar _scrollbar;

    public void AddAction(string text)
    {
        _textActionsLog.text += text + "\n";
        StartCoroutine(ScrollToBottom());
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForSeconds(0.1f);
        _scrollbar.value = 0;
    }

}
