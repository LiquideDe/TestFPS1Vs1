using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using DG.Tweening;

namespace FPS
{
    public class GameOverView : ViewCanDestroy
    {
        [SerializeField] private Button _buttonPlayAgain, _buttonExitMainmenu, _buttonExit;
        [SerializeField] private TextMeshProUGUI _textGameOver;
        [SerializeField] private CanvasGroup _bodyAlphaGroup;

        public event Action PlayAgain;
        public event Action ExitMainmenu;
        public event Action Exit;

        public delegate void PlaySomeSound();

        private void OnEnable()
        {
            _buttonPlayAgain.onClick.AddListener(PlayAgainPressed);
            _buttonExitMainmenu.onClick.AddListener(ExitMainmenuPressed);
            _buttonExit.onClick.AddListener(ExitPressed);
        }

        private void OnDisable()
        {
            _buttonPlayAgain.onClick.RemoveAllListeners();
            _buttonExitMainmenu.onClick.RemoveAllListeners();
            _buttonExit.onClick.RemoveAllListeners();
        }

        public void SetTextGameOver(string text) => _textGameOver.text = text;

        public void Show(PlaySomeSound playSomeSound)
        {
            Sequence animation = DOTween.Sequence();

            animation.Append(_bodyAlphaGroup.DOFade(1, 0.5f).From(0)).
                Append(_buttonPlayAgain.transform.DOScale(1, 0.5f).From(0)).
                Join(_buttonExitMainmenu.transform.DOScale(1, 0.5f).From(0)).
                Join(_buttonExit.transform.DOScale(1, 0.5f).From(0)).OnComplete(() => playSomeSound());
        }

        private void PlayAgainPressed() => Hide(PlayAgain);

        private void ExitMainmenuPressed() => Hide(ExitMainmenu);

        private void ExitPressed() => Application.Quit();

        private void Hide(Action SomeAction)
        {
            Sequence animation = DOTween.Sequence();
            animation.Append(_bodyAlphaGroup.DOFade(0, 0.5f).From(1)).OnComplete(() => SomeAction?.Invoke());
        }
    }
}


