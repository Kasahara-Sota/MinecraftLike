using UnityEngine;

public class BlockBase : MonoBehaviour
{
    private void OnEnable()
    {
        DrawVisible(false);
    }
    private void OnDisable()
    {
        DrawVisible(true);
    }
    private void DrawVisible(bool enabled)
    {
        DrawRenderer(Vector3.up, enabled);
        DrawRenderer(Vector3.down, enabled);
        DrawRenderer(Vector3.right, enabled);
        DrawRenderer(Vector3.left, enabled);
        DrawRenderer(Vector3.forward, enabled);
        DrawRenderer(Vector3.back, enabled);
    }
    private void DrawRenderer(Vector3 vector3, bool enabled)
    {
        Physics.Raycast(transform.position, vector3, out RaycastHit hit, 1, LayerMask.GetMask("Block"));
        if (hit.collider != null)
        {
            hit.collider.gameObject.GetComponent<Renderer>().enabled = enabled;
        }
    }
}
