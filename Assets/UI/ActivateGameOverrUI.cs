using UnityEngine;

public class ActivateGameOverrUI : MonoBehaviour
{
    [SerializeField] GameObject _gameOverUI;
    GameOverChecker _gameOverChecker;
    void Start()
    {
        _gameOverUI.SetActive(false);
        _gameOverChecker = GetComponent<GameOverChecker>();
        _gameOverChecker.OnGameOver += Activate;
    }
    void Activate()
    {
        _gameOverUI.SetActive(true);
    }
}
