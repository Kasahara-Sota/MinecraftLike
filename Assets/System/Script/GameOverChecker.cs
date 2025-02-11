using System;
using UnityEngine;

public class GameOverChecker : MonoBehaviour
{
    [SerializeField] float height = -1.0f;
    [SerializeField] GameObject _fadePanel;
    public event Action OnGameOver;
    GameObject _player;
    static bool _isGameOver = false;
    public static bool IsGameOver => _isGameOver;
    void Start()
    {
        _isGameOver = false;
        _player = FindAnyObjectByType<PlayerController>().gameObject;
    }

    void Update()
    {
        if (!_isGameOver && height > _player.transform.position.y)
        {
            OnGameOver?.Invoke();
            _player.GetComponent<PlayerController>().enabled = false;
            _fadePanel.SetActive(true);
            _fadePanel.GetComponent<Fade>().FadeOut();
            _isGameOver = true;
        }
    }
}
