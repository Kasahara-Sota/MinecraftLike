using UnityEngine;
public class EnemyRotate : MonoBehaviour
{
    [SerializeField] float _rotateSpeed = 360.0f;
    float _seed;
    private void Start()
    {
        _seed = Random.Range(0,6);
    }
    void Update()
    {
        Rotate();
    }
    private void Rotate()
    {
        transform.RotateAround(transform.position, transform.right, _rotateSpeed * Time.deltaTime * Mathf.Cos(Time.time - _seed));
    }
}
