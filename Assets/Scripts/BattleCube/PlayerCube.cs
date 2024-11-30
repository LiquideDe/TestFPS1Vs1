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
                SomeAction?.Invoke("����� ��������� ������");
            }
                
            else if (Input.GetKeyDown(KeyCode.S))
            {
                FullBack();
                SomeAction?.Invoke("����� ��������� �����");
            }
                

            if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S))
            {
                StopCube();
                SomeAction?.Invoke("����� ���������������");
            }
                

            if (Input.GetKeyDown(KeyCode.D))
            {
                RotateRight();
                SomeAction?.Invoke("����� ������������ �������");
            }
                
            else if (Input.GetKeyDown(KeyCode.A))
            {
                RotateLeft();
                SomeAction?.Invoke("����� ������������ ������");
            }
                

            if (Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.A))
            {
                StopRotate();
                SomeAction?.Invoke("����� �������� ��������������");
            }
                

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Fire();
                SomeAction?.Invoke("����� ������ �����");
            }
                
        }        
    }
}
