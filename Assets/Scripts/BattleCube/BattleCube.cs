using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BattleCube : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Bullet _bulletPrefab;

    public event Action<BattleCube> GetBreakdown;
    public event Action<Bullet> BulletInAir;

    private readonly int _maxSpeed = 5;
    private Vector3 _eulerAngle = Vector3.zero;
    private int _speed = 0;
    private Vector3 _startPosition;
    protected bool _isGameStarted = false;

    private void FixedUpdate()
    {
        if (_isGameStarted)
        {
            Quaternion deltaRotation = Quaternion.Euler(_eulerAngle * Time.fixedDeltaTime);
            _rigidbody.MoveRotation(_rigidbody.rotation * deltaRotation);
            _rigidbody.MovePosition(_rigidbody.position + (_rigidbody.transform.forward * _speed * Time.fixedDeltaTime));
        }       
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Есть пробитие, name = {collision.gameObject.name}");
        if (collision.gameObject.GetComponent<Bullet>())
            GetBreakdown?.Invoke(this);
        else if(collision.gameObject.name.Contains("Floor"))
            GetBreakdown?.Invoke(this);
    }
    public virtual void Initialize(Vector3 startPosition) 
    {
        _startPosition = startPosition;
        transform.position = _startPosition;
    } 

    public virtual void StartGame()
    {
        _isGameStarted = true;
        transform.position = _startPosition;
    }

    public virtual void StopGame()
    {
        _isGameStarted = false;
        StopCube();
        StopRotate();
    }

    protected void Fire()
    {
        if (_isGameStarted)
        {
            Bullet bullet = Instantiate(_bulletPrefab);
            bullet.Fire(_bulletPrefab.transform.position, _rigidbody.transform.forward);
            BulletInAir?.Invoke(bullet);
        }        
    }

    protected void RotateLeft() => _eulerAngle = new Vector3(0, -100, 0);

    protected void RotateRight() => _eulerAngle = new Vector3(0, 100, 0);

    protected void StopRotate() => _eulerAngle = Vector3.zero;

    protected void FullForward() => _speed = _maxSpeed;

    protected void FullBack() => _speed = _maxSpeed * -1;

    protected void StopCube() => _speed = 0;

}
