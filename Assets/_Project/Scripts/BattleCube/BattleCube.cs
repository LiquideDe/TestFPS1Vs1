using UnityEngine;
using System;

namespace FPS
{
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
            if (collision.gameObject.TryGetComponent<Bullet>(out Bullet bullet))
                GetBreakdown?.Invoke(this);
            else if (collision.gameObject.TryGetComponent<Floor>(out Floor floor))
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

        public void StopGame()
        {
            _isGameStarted = false;
            StopCube();
            StopRotate();
        }

        public void Fire()
        {
            if (_isGameStarted)
            {
                Bullet bullet = Instantiate(_bulletPrefab);
                bullet.Fire(_bulletPrefab.transform.position, _rigidbody.transform.forward);
                BulletInAir?.Invoke(bullet);
            }
        }

        public void RotateLeft() => _eulerAngle = new Vector3(0, -100, 0);

        public void RotateRight() => _eulerAngle = new Vector3(0, 100, 0);

        public void StopRotate() => _eulerAngle = Vector3.zero;

        public void FullForward() => _speed = _maxSpeed;

        public void FullBack() => _speed = _maxSpeed * -1;

        public void StopCube() => _speed = 0;

    }
}


