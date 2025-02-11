using UnityEngine;

public class RotateSky : MonoBehaviour
{
    void Update()
    {
        transform.RotateAround(transform.position, Vector3.up, 10 * Time.deltaTime);
    }
}
