using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class BreakBlock : MonoBehaviour
{
    [SerializeField] float _distance = 5;
    [SerializeField] float _breakSpeed = 5;
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
    void OnDisable()
    {
        _playerInput.actions["Attack"].performed -= OnBreak;
        _playerInput.actions["Attack"].canceled -= OnBreakCanceled;
    }
    IEnumerator BreakingBlock()
    {
        BlockBase blockBase = null;
        Vector3 vector3 = Vector3.zero;
        while (_isBreaking)
        {
            if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out RaycastHit hit, _distance, LayerMask.GetMask("Block")))
            {
                BlockBase NewBlockBase = hit.collider.gameObject.GetComponent<BlockBase>();
                if(NewBlockBase.transform.position == vector3)
                {
                    blockBase.ReduceEndurance(_breakSpeed);
                }
                else
                {
                    if(blockBase != null)
                    {
                        blockBase.ResetEndurance();
                    }    
                    blockBase = NewBlockBase;
                    vector3 = blockBase.transform.position;
                }
            }
            yield return null;
        }
    }
    void OnBreak(InputAction.CallbackContext context)
    {
        _isBreaking = true;
        StartCoroutine(BreakingBlock());
    }
    void OnBreakCanceled(InputAction.CallbackContext context)
    {
        _isBreaking = false;
    }
}
