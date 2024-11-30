using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    private Vector3 _startPosition;  

    private void Update()
    {
        if (Vector3.Distance(transform.position, _startPosition) > 100)
            DestroyBullet();
    }

    public void Fire(Vector3 startPosition, Vector3 vector)
    {
        gameObject.SetActive(true);
        transform.position = startPosition;
        _startPosition = startPosition;
        _rigidbody.AddForce(vector * 0.0002f);
    }

    public void DestroyBullet() => Destroy(gameObject);
}
