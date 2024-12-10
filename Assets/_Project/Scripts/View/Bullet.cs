using System.Collections;
using UnityEngine;

namespace FPS
{
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        private bool _isBulletOld = false;
        private Vector3 _startPosition;
        private BattleCube _battleCubeSender;

        public bool IsBulletOld => _isBulletOld;

        public BattleCube BattleCubeSender => _battleCubeSender;

        private void Update()
        {
            if (Vector3.Distance(transform.position, _startPosition) > 100)
                DestroyBullet();
        }

        public void Fire(Vector3 startPosition, Vector3 vector, BattleCube sender)
        {
            _battleCubeSender = sender;
            gameObject.SetActive(true);
            transform.position = startPosition;
            _startPosition = startPosition;
            _rigidbody.AddForce(vector * 0.0002f);            
            StartCoroutine(SwitchOnCollider());
        }

        public void DestroyBullet() => Destroy(gameObject);

        private IEnumerator SwitchOnCollider()
        {
            yield return new WaitForSeconds(0.1f);
            _isBulletOld = true;
        }
    }
}


