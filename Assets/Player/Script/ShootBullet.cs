using UnityEngine;
using UnityEngine.InputSystem;

public class ShootBullet : MonoBehaviour
{
    [SerializeField] float _bulletSpeed = 5f;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] GameObject _muzzle;
    Camera _camera;
    PlayerInput _playerInput;
    void Start()
    {
        _camera = Camera.main;
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Interact"].performed += OnShoot;
    }
    private void OnDisable()
    {
        _playerInput.actions["Interact"].performed -= OnShoot;
    }
    void OnShoot(InputAction.CallbackContext context)
    {
        GameObject obj = Instantiate(_bulletPrefab, _muzzle.transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().AddForce(_camera.transform.forward * _bulletSpeed, ForceMode.Impulse);
        AudioManager.Audio.PlaySE("Shoot");
    }
}
