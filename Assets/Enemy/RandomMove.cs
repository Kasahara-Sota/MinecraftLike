using System.Collections;
using UnityEngine;
using DG.Tweening;
public class RandomMove : MonoBehaviour
{
    [SerializeField,Range(1,5)] float _moveInterval;
    [SerializeField] Vector3 _size;
    [SerializeField] Vector3 _offset;
    Vector3 _center;
    float _xStartRange;
    float _yStartRange;
    float _zStartRange;
    float _xEndRange;
    float _yEndRange;
    float _zEndRange;
    private void Start()
    {
        _center = transform.position + _offset;
        _xStartRange = _center.x - _size.x / 2;
        _yStartRange = _center.y - _size.y / 2;
        _zStartRange = _center.z - _size.z / 2;
        _xEndRange = _center.x + _size.x / 2;
        _yEndRange = _center.y + _size.y / 2;
        _zEndRange = _center.z + _size.z / 2;
        StartCoroutine(Move());
    }
    private IEnumerator Move()
    {
        while (true)
        {
            Vector3 pos = new Vector3(Random.Range(_xStartRange, _xEndRange),
                                      Random.Range(_yStartRange, _yEndRange),
                                      Random.Range(_zStartRange, _zEndRange));
            transform.DOMove(pos,Random.Range(1,_moveInterval)).SetEase(Ease.InOutQuad);
            yield return new WaitForSeconds(_moveInterval);
        }
    }
}
