using UnityEngine;
using System;
using Zenject;
using System.Collections;

namespace FPS
{
    [RequireComponent(typeof(FXCubeManager))]
    public class BattleCube : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private Bullet _bulletPrefab;
        [SerializeField] private FXCubeManager _fxCubeManager;

        public event Action<BattleCube> GetBreakdown;
        public event Action<Bullet> BulletInAir;
        public event Action StopGameForAnimation;

        private int _maxSpeed = 5;
        private int _maxBonusSpeed = 2;
        private int _maxClip = 4;
        private int _maxCoolDown = 2;
        private Vector3 _eulerAngle = Vector3.zero;
        private int _speed = 0;
        private int _bonusSpeed = 1;
        private int _clip = 4;
        private float _coolDown = 2;
        private Vector3 _startPosition;
        protected bool _isGameStarted = false;
        protected bool _isCooldown = false;
        private IBattleCubeSounds _battleCubeSounds;
        private IBulletSounds _bulletSounds;

        [Inject]
        private void Construct(AudioManager audioManager)
        {
            _battleCubeSounds = audioManager;
            _bulletSounds = audioManager;
        }

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
            {
                if (bullet.IsBulletOld)
                {
                    PlayingDestroyCube();
                }                    
                else if(bullet.IsBulletOld == false && bullet.BattleCubeSender != this)
                {
                    PlayingDestroyCube();
                }                    
            }                
            else if (collision.gameObject.TryGetComponent<Floor>(out Floor floor))
            {
                PlayingDestroyCube();
            }
                
        }

        private void Update()
        {
            if(_isCooldown && _isGameStarted)
            {
                _coolDown -= Time.deltaTime;
                if (_coolDown <= 0)
                {
                    
                    _isCooldown = false;
                    _coolDown = _maxCoolDown;
                    _clip = _maxClip;
                }                    
            }                
        }

        public void Initialize(Vector3 startPosition, ConfigData configData)
        {
            _startPosition = startPosition;
            transform.position = _startPosition;
            _maxBonusSpeed = configData.bonusSpeed;
            _maxSpeed = configData.speed;
            _maxCoolDown = configData.cooldown;
            _maxClip = configData.clip;
        }

        public virtual void StartGame()
        {            
            _isGameStarted = true;
            transform.position = _startPosition;            
            _clip = _maxClip;
            _coolDown = _maxCoolDown;
            _isCooldown = false;
            
        }

        public void StopGame()
        {
            _isGameStarted = false;
            _bonusSpeed = 1;
            StopCube();
            StopRotate();            
        }

        public void Fire()
        {
            if (_isGameStarted && _isCooldown == false)
            {
                Bullet bullet = Instantiate(_bulletPrefab);
                bullet.Fire(_bulletPrefab.transform.position, _rigidbody.transform.forward,_bulletSounds ,this);
                BulletInAir?.Invoke(bullet);
                _clip--;
                CheckClip();
            }
        }

        public void RotateLeft() => _eulerAngle = new Vector3(0, -100, 0);

        public void RotateRight() => _eulerAngle = new Vector3(0, 100, 0);

        public void StopRotate() => _eulerAngle = Vector3.zero;

        public void FullForward() {
            _speed = _maxSpeed * _bonusSpeed;
            _battleCubeSounds.StartPlayMoving();
        }

        public void FullBack() {
            _speed = _maxSpeed * -1 * _bonusSpeed;
            _battleCubeSounds.StartPlayMoving();
        }

        public void StopCube() 
        {
            _speed = 0;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _battleCubeSounds.StopPlayMoving();
        }

        public void BoostSpeed() => _bonusSpeed = _maxBonusSpeed;

        public void DestroyCube() => Destroy(gameObject);

        private void CheckClip()
        {
            if (_clip == 0)
                _isCooldown = true;
        }

        protected void UpdateManual()
        {
            Update();
        }

        private void PlayingDestroyCube()
        {
            _fxCubeManager.PlayingDestroyCube(transform.position);
            _battleCubeSounds.PlayBigExplosion();
            StopGameForAnimation?.Invoke();
            StartCoroutine(TakePauseForAnimation());
        }

        private IEnumerator TakePauseForAnimation()
        {
            yield return new WaitForSeconds(1f);
            GetBreakdown?.Invoke(this);
        }
    }
}


