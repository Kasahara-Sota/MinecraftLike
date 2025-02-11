using System;
using System.Collections.Generic;
using UnityEngine;

public class BombEnemy : MonoBehaviour
{
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _attackInterval = 10f;
    [SerializeField] float _bulletSpeed = 5f;
    public event Action<Transform> _onDestroy;
    private float _timer;
    private GameObject _player;
    private void OnDestroy()
    {
        _onDestroy?.Invoke(transform);
    }
    private void Start()
    {
        _player = GameObject.FindAnyObjectByType<PlayerController>().gameObject;
        AudioManager.Audio.PlaySE("EnemySpawn");
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
    private void LookPlayer()
    {
        transform.LookAt(_player.transform.position);
    }
    private void Attack()
    {
        _timer += Time.deltaTime;
        if (_timer > _attackInterval && GameOverChecker.IsGameOver == false)
        {
            _timer = 0;
            GameObject obj = Instantiate(_bullet, _muzzle.transform.position, Quaternion.identity);
            obj.GetComponent<Rigidbody>().AddForce((_player.transform.position - _muzzle.transform.position).normalized * _bulletSpeed, ForceMode.Impulse);
            AudioManager.Audio.PlaySE("Shoot2");
            _attackInterval = UnityEngine.Random.Range(7.5f,12.5f);
        }
    }
}
