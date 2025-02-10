using System;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField] float height = -1.0f;
    public event Action OnGameOver;
    GameObject _player;
    bool _isGameOver = false;
    void Start()
    {
        _player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void Update()
    {
        if (!_isGameOver && height > _player.transform.position.y)
        {
            OnGameOver?.Invoke();
            _isGameOver = true;
        }
    }
}
