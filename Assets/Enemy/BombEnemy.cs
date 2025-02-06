using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _attackInterval = 10f;
    [SerializeField] float _bulletSpeed = 5f;
    float _timer;
    GameObject _player;
    private void Start()
    {
        _player = GameObject.FindAnyObjectByType<PlayerController>().gameObject;
    }

    private void Update()
    {
        LookPlayer();
        Attack();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {
            Destroy(this.gameObject);
        }
    }
    void LookPlayer()
    {
        transform.LookAt(_player.transform.position);
    }
    private void Attack()
    {
        _timer += Time.deltaTime;
        if (_timer > _attackInterval)
        {
            _timer = 0;
            GameObject obj = Instantiate(_bullet, _muzzle.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce((_player.transform.position - _muzzle.transform.position).normalized * _bulletSpeed, ForceMode.Impulse);
        }
    }

}
