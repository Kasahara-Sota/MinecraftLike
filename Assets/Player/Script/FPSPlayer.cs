using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPSPlayer : MonoBehaviour
{
    [SerializeField] float _speed;
    [SerializeField] float _jumpHeight;
    [SerializeField] GameObject _body;
    [SerializeField] LayerMask _layerMask;
    float _jumpPower;
    float _horizontalInput;
    float _verticalInput;
    [SerializeField] bool _isGround;
    [SerializeField] bool _isForwardWall;
    [SerializeField] bool _isBackWall;
    [SerializeField] bool _isRightWall;
    [SerializeField] bool _isLeftWall;
    [SerializeField] float _wallCheckDistance;
    PlayerInput _playerInput;
    Camera _camera;
    Rigidbody _rb;
    private void OnDrawGizmosSelected()
    {
        //Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position, transform.position + Vector3.down * 10);
    }
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _jumpPower = Mathf.Sqrt(19.6f * _jumpHeight);//ジャンプした時の最高点が_jumpHeightになるような初速度を求める
    }
    private void OnEnable()
    {
        _playerInput.actions["Move"].performed += OnMove;
        _playerInput.actions["Move"].canceled += OnMoveCanceled;
        _playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnDisable()
    {
        // イベントリスナーを解除（オブジェクトが無効になる前に行うべき）
        _playerInput.actions["Move"].performed -= OnMove;
        _playerInput.actions["Move"].canceled -= OnMoveCanceled;
        _playerInput.actions["Jump"].performed -= OnJump;
    }
    void Start()
    {
        _camera = Camera.main;
        _rb = GetComponent<Rigidbody>();
        _rb.useGravity = false;
        _isGround = true;
        Physics.Raycast(new Vector3(0, 200, 0), Vector3.down, out RaycastHit hit, 250, LayerMask.GetMask("Block"));
        transform.position = hit.point + hit.normal / 10000;
    }
    void Update()
    {
        PlayerRotate();
        WallCheck();
        PlayerMove();
        GroundCheck();
    }
    void OnMove(InputAction.CallbackContext context)
    {
        Vector2 moveInput = context.ReadValue<Vector2>();
        _horizontalInput = moveInput.x;
        _verticalInput = moveInput.y;
    }
    void OnMoveCanceled(InputAction.CallbackContext context)
    {
        _horizontalInput = 0;
        _verticalInput = 0;
    }
    void OnJump(InputAction.CallbackContext context)
    {
        if (_isGround)
        {
            _isGround = false;
            _rb.useGravity = true;
            _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _jumpPower, _rb.linearVelocity.z);
        }
    }
    void PlayerMove()
    {
        //Debug.Log($"{_horizontalInput},{_verticalInput}");
        Vector3 velocity = _body.transform.forward * _verticalInput + _body.transform.right * _horizontalInput;
        velocity *= _speed;
        velocity.y = _rb.linearVelocity.y;
        if (velocity.z > 0 && _isForwardWall)
        {
            velocity.z = 0;
        }
        if (velocity.z < 0 && _isBackWall)
        {
            velocity.z = 0;
        }
        if (velocity.x > 0 && _isRightWall)
        {
            velocity.x = 0;
        }
        if (velocity.z < 0 && _isLeftWall)
        {
            velocity.x = 0;
        }
        _rb.linearVelocity = velocity;
    }
    void PlayerRotate()
    {
        _body.transform.eulerAngles = new Vector3(0, _camera.transform.eulerAngles.y, 0);
    }
    void GroundCheck()
    {
        _isGround = Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 1.1f);
        
        if(!_isGround)
        {
            _isGround = true;
        }
        if (_isGround && _rb.linearVelocity.y <= 0)
        {
            _rb.useGravity = false;
            transform.position = hit.point + hit.normal / 10000;
        }
    }
    void WallCheck()
    {
        Debug.DrawRay(transform.position + Vector3.up * 0.1f, Vector3.forward * _wallCheckDistance, Color.blue);
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.forward, out _, _wallCheckDistance))
        {
            _isForwardWall = true;
        }
        else
        {
            _isForwardWall = false;
        }
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.back, out _, _wallCheckDistance))
        {
            _isBackWall = true;
        }
        else
        {
            _isBackWall = false;
        }
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.right, out _, _wallCheckDistance))
        {
            _isRightWall = true;
        }
        else
        {
            _isRightWall = false;
        }
        if (Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.left, out _, _wallCheckDistance))
        {
            _isLeftWall = true;
        }
        else
        {
            _isLeftWall = false;
        }
    }
}
