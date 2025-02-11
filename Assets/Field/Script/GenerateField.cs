using System;
using UnityEngine;

public class GenerateField : MonoBehaviour
{
    [SerializeField] Vector3Int _generateSize;
    [SerializeField] GameObject _obj;
    private float _xOrigin;
    private float _yOrigin;
    [SerializeField, Range(0.0001f, 1)] float _flatScale;
    [SerializeField] float _holeSize = 8;
    [SerializeField] float _DonutSize = 16;
    [SerializeField] float _generateSpeed = 1;
    void Awake()
    {
        //StartCoroutine(Generate());
        Generate();
    }
    void Generate()
    {
        _xOrigin = UnityEngine.Random.Range(-10000,10000);
        _yOrigin = UnityEngine.Random.Range(-10000, 10000);
        Debug.Log($"{_xOrigin},{_yOrigin}");
        for (int z = 0; z < _generateSize.z; z++)
        {
            for (int x = 0; x < _generateSize.x; x++)
            {
                float distanceFromCenter = Vector2.Distance(new Vector2(x, z), new Vector2(_generateSize.x / 2, _generateSize.z / 2));
                if (distanceFromCenter < _holeSize || distanceFromCenter > _DonutSize)
                {
                    continue;
                }
                float xValue = _xOrigin + x * _flatScale;
                float yValue = _yOrigin + z * _flatScale;
                float valueXZ = (int)(Mathf.PerlinNoise(xValue, yValue) * 10);
                int n = (int)valueXZ + _generateSize.y;
                GameObject obj;
                for (int y = (int)valueXZ; y < n; y++)
                {
                    obj = Instantiate(_obj, new Vector3(x, y, z), Quaternion.identity);
                    obj.transform.SetParent(transform);
                    //yield return new WaitForSeconds(_generateSpeed);
                }
            }
            //yield return null;
        }
    }
    //IEnumerator Generate()
    //{

    //}
}
