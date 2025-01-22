using System;
using System.Collections;
using UnityEngine;

public class GenerateField : MonoBehaviour
{
    [SerializeField] GameObject _obj;
    [SerializeField] float _xOrigin;
    [SerializeField] float _yOrigin;
    float _beforeX;
    float _beforeY;
    [SerializeField, Range(0.0001f, 1)] float _flatScale;
    float _beforeFlatScale;
    [SerializeField] float _generateSpeed = 1;
    void Start()
    {
        //StartCoroutine(Generate());
        Generate();
    }
    void Generate()
    {
        _beforeX = _xOrigin;
        _beforeY = _yOrigin;
        _beforeFlatScale = _flatScale;
        for (int z = 0; z < 16 * 4; z++)
        {
            for (int x = 0; x < 16 * 4; x++)
            {
                float xValue = _xOrigin + x * _flatScale;
                float yValue = _yOrigin + z * _flatScale;
                float valueXZ = (int)(Mathf.PerlinNoise(xValue, yValue) * 10);
                int n = (int)valueXZ + 64;
                valueXZ /= 10;
                GameObject obj;
                for (int y = 0; y < n; y++)
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
    void Update()
    {
        if (_beforeX != _xOrigin || _beforeY != _yOrigin || _beforeFlatScale != _flatScale)
            for (int z = 0; z < 64; z++)
            {
                for (int x = 0; x < 64; x++)
                {
                    float xValue = _xOrigin + x * _flatScale;
                    float yValue = _yOrigin + z * _flatScale;
                    float value = (int)(Mathf.PerlinNoise(xValue, yValue) * 10);
                    transform.GetChild(z * 64 + x).position = new Vector3(x, value, z);
                    value /= 10;
                    transform.GetChild(z * 64 + x).gameObject.GetComponent<Renderer>().material.color = new Color(value, value * 2, value, 1);

                }
            }
        _beforeX = _xOrigin;
        _beforeY = _yOrigin;
        _beforeFlatScale = _flatScale;
    }
}
