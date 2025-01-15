using DG.Tweening;
using System.Collections;
using UnityEngine;

namespace FPS
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private GameObject _explosionPrefab;
        private bool _isBulletOld = false;
        private Vector3 _startPosition;
        private BattleCube _battleCubeSender;
        private IBulletSounds _bulletSounds;

        public bool IsBulletOld => _isBulletOld;

        public BattleCube BattleCubeSender => _battleCubeSender;

        private void Update()
        {
            if (Vector3.Distance(transform.position, _startPosition) > 100)
                DestroyBullet();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.TryGetComponent<Wall>(out Wall wall)) 
            {
                _bulletSounds.PlayRebound();
            }
        }

        public void Fire(Vector3 startPosition, Vector3 vector, IBulletSounds bulletSounds,BattleCube sender)
        {
            _battleCubeSender = sender;
            _bulletSounds = bulletSounds;
            gameObject.SetActive(true);
            transform.position = startPosition; 
            _startPosition = startPosition;
            _rigidbody.AddForce(vector * 0.00015f);
            _bulletSounds.PlayStartBullet();
            StartCoroutine(SwitchOnCollider());
        }

        public void DestroyBullet() => Destroy(gameObject);

        private IEnumerator SwitchOnCollider()
        {
            //Вся система создана потому, что когда даю бонусное ускорение, то моделька вертолета быстрее спавнешейся пули и сама на себя насаживалась
            yield return new WaitForSeconds(0.1f);
            _isBulletOld = true;
        }
    }
}


