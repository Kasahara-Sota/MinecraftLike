using System.Collections;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] float _generateInterval;
    [SerializeField] Vector3 _center;
    [SerializeField] Vector3 _size;
    ScoreManager _scoreManager;
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
        StartCoroutine(Generate());
    }
    private void Update()
    {
        
    }
    private IEnumerator Generate()
    {
        while (true)
        {
            Vector3 pos = new Vector3(Random.Range(_xStartRange, _xEndRange),
                                      Random.Range(_yStartRange, _yEndRange),
                                      Random.Range(_zStartRange, _zEndRange));
            GameObject obj = Instantiate(_enemy, pos, Quaternion.identity);
            _scoreManager.AddEventListener(obj.GetComponent<AddScore>());
            yield return new WaitForSeconds(_generateInterval);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(255, 0, 0, 10);
        Gizmos.DrawCube(_center, _size);
    }
}
