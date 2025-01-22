using UnityEngine;

public class BlockBase : MonoBehaviour
{
    [SerializeField] float _endurance = 10;//ëœãvê´
    float _defaultEndurance;
    private void Start()
    {
        _defaultEndurance = _endurance;
        ResetEndurance();
    }
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
    public void DrawRenderer(Vector3 vector3, bool enabled)
    {
        Physics.Raycast(transform.position, vector3, out RaycastHit hit, 1, LayerMask.GetMask("Block"));
        if (hit.collider != null)
        {
            int index;
            //transform.å¸Ç´Ç≈Ç‚Ç¡ÇΩÇŸÇ§Ç™Ç¢Ç¢Ç©Ç‡
            if (vector3 == Vector3.back)
            {
                index = 0;
            }
            else if (vector3 == Vector3.left)
            {
                index = 1;
            }
            else if (vector3 == Vector3.forward)
            {
                index = 2;
            }
            else if (vector3 == Vector3.right)
            {
                index = 3;
            }
            else if (vector3 == Vector3.up)
            {
                index = 4;
            }
            else
            {
                index = 5;
            }
            transform.GetChild(index).gameObject.GetComponent<Renderer>().enabled = enabled;
            GameObject obj = hit.collider.gameObject;
            obj.GetComponent<BlockBase>().DrawRenderer(transform, enabled);
        }
    }
    public void DrawRenderer(Transform targetTransform, bool enabled)
    {
        Vector3 vector3 = targetTransform.position - this.transform.position;
        Physics.Raycast(this.transform.position, vector3, out RaycastHit hit, 1, LayerMask.GetMask("Block"));
        if (hit.collider != null)
        {
            int index;
            if (vector3 == Vector3.back)
            {
                index = 0;
            }
            else if (vector3 == Vector3.left)
            {
                index = 1;
            }
            else if (vector3 == Vector3.forward)
            {
                index = 2;
            }
            else if (vector3 == Vector3.right)
            {
                index = 3;
            }
            else if (vector3 == Vector3.up)
            {
                index = 4;
            }
            else
            {
                index = 5;
            }
            transform.GetChild(index).gameObject.GetComponent<Renderer>().enabled = enabled;
        }
    }
    public void ReduceEndurance(float ó )
    {
        _endurance -= ó ;
        float äÑçá = _endurance / _defaultEndurance * 255;
        //GetComponentInChildren<Renderer>().material.color = new Color(äÑçá, äÑçá, äÑçá);
        if (_endurance <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void ResetEndurance()
    {
        _endurance = _defaultEndurance;
        //GetComponentInChildren<Renderer>().material.color = new Color(255,255,255);
    }
}
