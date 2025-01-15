using System;
using UnityEngine;
using UnityEngine.Purchasing;

namespace FPS
{
    public class ShopPresenter
    {
        public event Action Close;
        private IMenuSounds _menuSounds;
        private ShopView _view;

        public ShopPresenter(IMenuSounds menuSounds, ShopView view)
        {
            _menuSounds = menuSounds;
            _view = view;
            Subscribe();
            if(PlayerPrefs.GetInt("removeAds") == 1)
                view.HideButtonPurchise();

            view.Show();
            menuSounds.PlayPopUp();
        }

        private void Subscribe()
        {
            _view.CloseShop += CloseShop;
            _view.Purchasing += Purchase;
            _view.Restore += Restore;
        }

        private void Unscribe()
        {
            _view.CloseShop -= CloseShop;
            _view.Purchasing -= Purchase;
            _view.Restore -= Restore;
        }

        private void CloseShop()
        {
            _menuSounds.PlayClick();
            Unscribe();
            _view.DestroyView();
            Close?.Invoke();
        }

        private void Purchase(Product product)
        {
            _menuSounds.PlayClick();
            switch (product.definition.id)
            {
                case "com.LiquideDe.TestFPS1Vs1.removeAds":
                    RemoveAds();
                    break;

                default:
                    Debug.LogError($"Не нашел нужного id {product.definition.id}");
                    break;
            }
        }

        private void RemoveAds()
        {
            PlayerPrefs.SetInt("removeAds", 1);
            _view.HideButtonPurchise();
        }

        private void Restore()
        {
            _menuSounds.PlayClick();
            PlayerPrefs.SetInt("removeAds", 0);
            _view.ShowButtonPurchise();
        }
    }
}

