using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXCubeManager : MonoBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    //Создал этот класс потому, что надо было сделать отдельную систему, до этого инстанциировал объект прямо в BattleCube
    public void PlayingDestroyCube(Vector3 position)
    {
        GameObject gameObject = Instantiate(_explosionPrefab);
        gameObject.transform.position = position;
        gameObject.SetActive(true);
    }
}
