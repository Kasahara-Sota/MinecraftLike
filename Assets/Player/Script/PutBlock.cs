using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PutBlock : MonoBehaviour
{
    [SerializeField] float _distance = 5;
    [SerializeField] GameObject _blockPrefab;
    Camera _camera;
    PlayerInput _playerInput;
    bool _isPutting = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Interact"].performed += OnPut;
        _playerInput.actions["Interact"].canceled += OnPutCanceled;
    }
    IEnumerator Putting()
    {
        while (_isPutting)
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _distance, LayerMask.GetMask("Block")))
            {
                Vector3 currentPosition = hit.point + hit.normal / 2;
                float signX = Mathf.Sign(currentPosition.x);
                float signY = Mathf.Sign(currentPosition.y);
                float signZ = Mathf.Sign(currentPosition.z);
                currentPosition.x = (int)(currentPosition.x / 0.5f + signX) / 2;
                currentPosition.y = (int)(currentPosition.y / 0.5f + signY) / 2;
                currentPosition.z = (int)(currentPosition.z / 0.5f + signZ) / 2;
                if (!Physics.BoxCast(hit.collider.transform.position, Vector3.one * 0.4f, hit.normal, out hit))
                {
                    Instantiate(_blockPrefab, currentPosition, Quaternion.identity);
                    yield return new WaitForSeconds(0.2f);
                }
                else
                {
                    yield return null;
                    Debug.Log($"{hit.collider.transform.position}‚É‚ ‚é{hit.collider.gameObject.name}‚Ì‚¹‚¢‚Å’u‚¯‚Ü‚¹‚ñ");
                }
            }
            else
            {
                yield return null;
            }
        }    
    }
    void OnPut(InputAction.CallbackContext context)
    {
        _isPutting = true;
        StartCoroutine(Putting());
    }
    void OnPutCanceled(InputAction.CallbackContext context)
    {
        _isPutting = false;
    }
}
