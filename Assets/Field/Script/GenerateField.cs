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
    void Start()
    {
        _beforeX = _xOrigin;
        _beforeY = _yOrigin;
        _beforeFlatScale = _flatScale;
        for (int z = 0; z < 16; z++)
        {
            for (int x = 0; x < 16; x++)
            {
                float xValue = _xOrigin + x * _flatScale;
                float yValue = _yOrigin + z * _flatScale;
                float valueXZ = (int)(Mathf.PerlinNoise(xValue, yValue) * 10);
                GameObject obj;
                int n = (int)valueXZ + 64;
                valueXZ /= 10;
                for (int y = 0; y < n; y++)
                {
                    obj = Instantiate(_obj, new Vector3(x, y, z), Quaternion.identity);
                    if (Physics.Raycast(obj.transform.position, Vector3.down, out RaycastHit hit, 0.6f))
                    {
                        hit.collider.gameObject.GetComponent<Renderer>().enabled = false;
                    }
                    obj.transform.SetParent(transform);
                }
            }
        }

    }
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
