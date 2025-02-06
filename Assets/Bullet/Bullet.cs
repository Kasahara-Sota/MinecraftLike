using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] BulletTag _bulletTag = BulletTag.PlayerBullet;
    [SerializeField] float _radius;
    public enum BulletTag
    {
        PlayerBullet,
        EnemyBullet
    }
    void Start()
    {
        tag = _bulletTag.ToString();
    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, _radius, Vector3.down, 0.01f, LayerMask.GetMask("Block"));
        foreach (RaycastHit hit in hits)
        {
            Debug.Log($"Destroy {hit.collider.name}");
            Destroy(hit.collider.gameObject);
        }
        Destroy(gameObject);
    }
}
