using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakBlock : MonoBehaviour
{
    [SerializeField] float _distance = 5;
    Camera _camera;
    PlayerInput _playerInput;
    bool _isBreaking;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _camera = Camera.main;
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Attack"].performed += OnBreak;
        _playerInput.actions["Attack"].canceled += OnBreakCanceled;
    }

    // Update is called once per frame
    void Update()
    {
        if (_isBreaking)
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _distance, LayerMask.GetMask("Block")))
            {
                hit.collider.gameObject.GetComponent<BlockBase>().ReduceEndurance(0.1f);
            }
        }
    }
    void OnBreak(InputAction.CallbackContext context)
    {
        _isBreaking = true;
    }
    void OnBreakCanceled(InputAction.CallbackContext context)
    {
        _isBreaking = false;
    }
}
