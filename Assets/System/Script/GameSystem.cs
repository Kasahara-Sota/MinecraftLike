using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] int _fps = 60;
    GameOverChecker _gameOverChecker;
    void Awake()
    {
        Application.targetFrameRate = _fps;
        Cursor.lockState = CursorLockMode.Locked;
        _gameOverChecker = FindAnyObjectByType<GameOverChecker>();
        _gameOverChecker.OnGameOver += UnlockCursor;
    }
    void UnlockCursor()
    {
        Cursor.lockState = CursorLockMode.Confined;
    }
}
