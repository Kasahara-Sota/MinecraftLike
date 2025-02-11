using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] float _generateInterval;
    [SerializeField] float _decreaseInterval = 0.02f;
    [SerializeField] float _generateAmount = 1;
    [SerializeField] Vector3 _center;
    [SerializeField] Vector3 _size;
    ScoreManager _scoreManager;
    EnemyVisible _enemyVisible;
    float _xStartRange;
    float _yStartRange;
    float _zStartRange;
    float _xEndRange;
    float _yEndRange;
    float _zEndRange;
    private void Start()
    {
        _xStartRange = _center.x - _size.x / 2;
        _yStartRange = _center.y - _size.y / 2;
        _zStartRange = _center.z - _size.z / 2;
        _xEndRange = _center.x + _size.x / 2;
        _yEndRange = _center.y + _size.y / 2;
        _zEndRange = _center.z + _size.z / 2;
        _scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        _enemyVisible = GameObject.FindAnyObjectByType<EnemyVisible>();
        StartCoroutine(Generate());
    }
    private void Update()
    {

    }
    private IEnumerator Generate()
    {
        while (true)
        {
            for (float i = 0.5f; i < Random.Range(1, _generateAmount); i++)
            {
                Vector3 pos = new Vector3(Random.Range(_xStartRange, _xEndRange),
                          Random.Range(_yStartRange, _yEndRange),
                          Random.Range(_zStartRange, _zEndRange));
                GameObject obj = Instantiate(_enemy, pos, Quaternion.identity);
                _scoreManager.AddEventListener(obj.GetComponent<AddScore>());
                _enemyVisible.AddVisualGuide(obj.transform);
                obj.GetComponent<BombEnemy>()._onDestroy += EnemyDestroyed;
            }
            yield return new WaitForSeconds(_generateInterval);
            _generateInterval -= _decreaseInterval;
            if (_generateInterval <= 2)
            {
                _generateInterval = 2;
            }
            _generateAmount += 0.2f;
            if (_generateAmount > 6)
            {
                _generateAmount = 6;
            }
        }
    }
    private void EnemyDestroyed(Transform transform)
    {
        _enemyVisible.RemoveVisualGuide(transform);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 10);
        Gizmos.DrawCube(_center, _size);
    }
}
