using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;
using System;
using DG.Tweening;

namespace FPS
{
    public class ShopView : ViewCanDestroy
    {
        [SerializeField] private IAPListener _iAPListener;
        [SerializeField] private Button _buttonClose, _buttonRestore, _buttonBuy;
        [SerializeField] private CanvasGroup _canvasGroup;

        public event Action CloseShop;
        public event Action Restore;
        public event Action<Product> Purchasing;

        private void OnEnable()
        {
            _iAPListener.onPurchaseComplete.AddListener(Purchase);
            _buttonClose.onClick.AddListener(ClosePressed);
            _buttonRestore.onClick.AddListener(RestorePressed);
        }        

        private void OnDisable()
        {
            _iAPListener.onPurchaseComplete.RemoveAllListeners();
            _buttonClose.onClick.RemoveAllListeners();
            _buttonRestore.onClick.RemoveAllListeners();
        }

        public void Show()
        {
            Sequence animation = DOTween.Sequence();

            animation.Append(_canvasGroup.DOFade(1, 0.5f).From(0)).
                Append(_buttonBuy.transform.DOScale(1, 0.5f).From(0)).
                Join(_buttonClose.transform.DOScale(1, 0.5f).From(0)).
                Join(_buttonRestore.transform.DOScale(1, 0.5f).From(0));
        }

        public void ShowButtonPurchise() => _buttonBuy.gameObject.SetActive(true);

        public void HideButtonPurchise() => _buttonBuy.gameObject?.SetActive(false);

        private void Purchase(Product  product) => Purchasing?.Invoke(product);

        private void RestorePressed() => Restore?.Invoke();

        private void ClosePressed() => CloseShop?.Invoke();
    }
}

