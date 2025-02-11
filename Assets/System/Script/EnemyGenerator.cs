using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    [SerializeField] GameObject _enemy;
    [SerializeField] float _generateInterval;
    [SerializeField] float _decreaseInterval = 0.02f;
    [SerializeField] float _generateAmount = 1;
    [SerializeField] List<GenerateArea> _areas;
    ScoreManager _scoreManager;
    EnemyVisible _enemyVisible;
    float _xStartRange;
    float _yStartRange;
    float _zStartRange;
    float _xEndRange;
    float _yEndRange;
    float _zEndRange;
    [System.Serializable]
    public struct GenerateArea
    {
        public Vector3 Center;
        public Vector3 Size;
    }
    private void Start()
    {
        _scoreManager = GameObject.FindAnyObjectByType<ScoreManager>();
        _enemyVisible = GameObject.FindAnyObjectByType<EnemyVisible>();
        StartCoroutine(Generate());
    }
    private void SetRange(Vector3 center,Vector3 size)
    {
        _xStartRange = center.x - size.x / 2;
        _yStartRange = center.y - size.y / 2;
        _zStartRange = center.z - size.z / 2;
        _xEndRange = center.x + size.x / 2;
        _yEndRange = center.y + size.y / 2;
        _zEndRange = center.z + size.z / 2;
    }
    private IEnumerator Generate()
    {
        yield return null;
        while (GameOverChecker.IsGameOver == false)
        {
            for (float i = 0.5f; i < Random.Range(1, _generateAmount); i++)
            {
                GenerateArea area = _areas[Random.Range(0,_areas.Count)];
                SetRange(area.Center, area.Size);
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
        foreach(GenerateArea generateArea in _areas)
        {
            Gizmos.DrawCube(generateArea.Center, generateArea.Size);
        }
    }
}
