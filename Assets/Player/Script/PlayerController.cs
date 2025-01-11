using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    float _horizontalInput;
    float _verticalInput;
    public float speed = 6.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    float fallSpeed = 0f;
    [SerializeField] bool _isGround;
    [SerializeField] GameObject _body;
    private Vector3 moveDirection = Vector3.zero;
    CharacterController _characterController;
    PlayerInput _playerInput;
    Camera _camera;
    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _camera = Camera.main;
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
        _characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        PlayerRotate();
    }
    private void Move()
    {
        _isGround = _characterController.isGrounded;
        if (!_isGround)
        {
            _isGround = Physics.Raycast(transform.position + new Vector3(0, 0.1f, 0), Vector3.down, 0.5f, LayerMask.GetMask("Block"));
        }
        Vector3 velocity = _body.transform.forward * _verticalInput + _body.transform.right * _horizontalInput;
        velocity *= speed;
        if (_isGround)
        {
            if (fallSpeed < 0)
            {
                fallSpeed = 0;
            }
        }
        else
        {
            fallSpeed = fallSpeed - (gravity * Time.deltaTime);
        }
        velocity.y = fallSpeed;
        _characterController.Move(velocity * Time.deltaTime);
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
            fallSpeed = jumpSpeed;
        }
    }
    void PlayerRotate()
    {
        _body.transform.eulerAngles = new Vector3(0, _camera.transform.eulerAngles.y, 0);
    }

}
