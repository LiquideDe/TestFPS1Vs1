using System;
using UnityEngine;

namespace FPS
{
    [CreateAssetMenu(fileName = "PrefabHolder", menuName = "Holder/PrefabHolder")]
    public class PrefabHolder : ScriptableObject
    {
        [SerializeField] private GameObject _mainmenu,_battlegroundView, _advertising, _timer, _gameOverPanel, _shop;
        public GameObject Get(TypeScene typeScene)
        {
            switch (typeScene)
            {
                case TypeScene.MainMenu:
                    return _mainmenu;

                case TypeScene.BattlegroundView:
                    return _battlegroundView;

                case TypeScene.AdvertisingMenu:
                    return _advertising;

                case TypeScene.Timer:
                    return _timer;

                case TypeScene.GameOverPanel:
                    return _gameOverPanel;

                case TypeScene.Shop:
                    return _shop;

                default:
                    throw new ArgumentException($"Неверный тип сцены {typeScene}");
            }
        }
    }
}


