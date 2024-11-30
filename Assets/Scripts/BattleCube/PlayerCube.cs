using UnityEngine;
using System;

public class PlayerCube : BattleCube
{
    public event Action<string> SomeAction;
    void Update()
    {
        if (_isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                FullForward();
                SomeAction?.Invoke("Игрок двигается вперед");
            }
                
            else if (Input.GetKeyDown(KeyCode.S))
            {
                FullBack();
                SomeAction?.Invoke("Игрок двигается назад");
            }
                

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                StopCube();
                SomeAction?.Invoke("Игрок останавливается");
            }
                

            if (Input.GetKeyDown(KeyCode.D))
            {
                RotateRight();
                SomeAction?.Invoke("Игрок поворачивает направо");
            }
                
            else if (Input.GetKeyDown(KeyCode.A))
            {
                RotateLeft();
                SomeAction?.Invoke("Игрок поворачивает налево");
            }
                

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                StopRotate();
                SomeAction?.Invoke("Игрок перестал поворачиваться");
            }
                

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
                SomeAction?.Invoke("Игрок открыл огонь");
            }
                
        }        
    }
}
