using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletTag _bulletTag = BulletTag.PlayerBullet;
    [SerializeField] float _radius;
    [SerializeField] GameObject _explosion;
    public enum BulletTag
    {
        PlayerBullet,
        EnemyBullet
    }
    void Start()
    {
        tag = _bulletTag.ToString();
        Destroy(gameObject,10f);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (_bulletTag == BulletTag.PlayerBullet)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Destroy(collision.gameObject);
            }
        }
        else
        {
            RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, Vector3.down, 0.01f, LayerMask.GetMask("Block"));
            foreach (RaycastHit hit in hits)
            {
                Destroy(hit.collider.gameObject);
            }
        }
        GameObject obj = Instantiate(_explosion, transform.position, Quaternion.identity);
        Destroy(obj, 2f);
        Destroy(gameObject);
    }
}
