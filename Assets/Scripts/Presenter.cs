using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Presenter 
{
    private List<BattleCube> _battleCubes = new List<BattleCube>();
    private View _view;
    private List<Bullet> _bullets = new List<Bullet>();
    private List<string> _stringActions = new List<string>();
    private int _aiWin = 0, _playerWin = 0;

    public void Initialize(View view, AICube aICube, PlayerCube playerCube)
    {
        _view = view;
        _battleCubes.Add(aICube);
        _battleCubes.Add(playerCube);
        playerCube.SomeAction += AddTextAction;
        Subscribe();
    }

    private void AddTextAction(string textAction)
    {
        _view.TextActions.text += textAction + "\n";        
        _stringActions.Add(textAction + "\n");
        /*
        if(_view.TextActions.textInfo.pageCount > 1)
        {
            
            _stringActions.RemoveAt(1);
            _stringActions.RemoveAt(2);
            
        }          */

        if(_stringActions.Count > 31)
        {
            _stringActions.RemoveAt(0);
            _view.TextActions.text = "";
            foreach (string text in _stringActions)
                _view.TextActions.text += text;
        }
    }

    private void Subscribe()
    {
        _view.StartGame += StartGame;
        _view.StopGame += StopGame;

        foreach(BattleCube battleCube in _battleCubes)
        {
            battleCube.BulletInAir += AddBullet;
            battleCube.GetBreakdown += GetBreakdown;
        }
    }

    private void GetBreakdown(BattleCube cube)
    {
        StopGame();
        foreach (Bullet bullet in _bullets)
            if(bullet != null)
                bullet.DestroyBullet();
        _bullets.Clear();

        if (cube is AICube)
            _playerWin++;
        else
            _aiWin++;

        _view.TextCounts.text = $"Игрок {_playerWin}:{_aiWin} Компьютер";            
    }

    private void AddBullet(Bullet bullet) => _bullets.Add(bullet);

    private void StopGame()
    {
        foreach (BattleCube battleCube in _battleCubes)
            battleCube.StopGame();

        _view.ButtonStop.gameObject.SetActive(false);
        _view.ButtonStart.gameObject.SetActive(true);
    }

    private void StartGame()
    {
        foreach (BattleCube battleCube in _battleCubes)
            battleCube.StartGame();

        _view.ButtonStop.gameObject.SetActive(true);
        _view.ButtonStart.gameObject.SetActive(false);
    }
}
