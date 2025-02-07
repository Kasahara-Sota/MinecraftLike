using System.Collections.Generic;
using UnityEngine;

public class EnemyVisible : MonoBehaviour
{
    [SerializeField] float _radius;
    [SerializeField] GameObject _visualGuide;
    List<RectTransform> _visualGuides = new List<RectTransform>();
    Transform _playerTransform;
    List<Transform> _enemiesTransform = new List<Transform>();
    void Start()
    {
        _playerTransform = GameObject.FindAnyObjectByType<PlayerController>().transform.GetChild(0);
    }
    void Update()
    {
        UpdateDirection();
    }
    void UpdateDirection()
    {
        for (int i = 0; i < _enemiesTransform.Count; i++)
        {
            float x = _enemiesTransform[i].position.x - _playerTransform.position.x;
            float y = _enemiesTransform[i].position.z - _playerTransform.position.z;
            Vector3 forward3 = _playerTransform.forward;
            Vector2 forward2 = new Vector2(forward3.x, forward3.z).normalized;
            Vector2 enemyFromPlayer = new Vector2(x, y).normalized;
            float angle = Vector2.SignedAngle(forward2, enemyFromPlayer) + 90;
            angle = Mathf.Deg2Rad * angle;//ƒ‰ƒWƒAƒ“‚É•ÏŠ·
            _visualGuides[i].localPosition = new Vector3(Mathf.Cos(angle) * _radius, Mathf.Sin(angle) * _radius, 0);
        }
    }
    public void AddVisualGuide(Transform enemyTransform)
    {
        GameObject obj = Instantiate(_visualGuide, gameObject.transform);
        _visualGuides.Add(obj.GetComponent<RectTransform>());
        _enemiesTransform.Add(enemyTransform);
    }
    public void RemoveVisualGuide(Transform enemyTransform)
    {
        GameObject obj = _visualGuides[0].gameObject;
        _visualGuides.RemoveAt(0);
        Destroy(obj);
        _enemiesTransform.Remove(enemyTransform);
    }
}
