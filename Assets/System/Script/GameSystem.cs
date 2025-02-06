using UnityEngine;

public class GameSystem : MonoBehaviour
{
    [SerializeField] int _fps = 60;
    void Start()
    {
        Application.targetFrameRate = _fps;
    }
}
