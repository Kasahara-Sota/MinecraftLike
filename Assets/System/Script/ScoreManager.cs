using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    ulong _score;
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void AddEventListener(AddScore addScore)
    {
        addScore._onAddScore += AddScore;
    }
    private void AddScore(ulong score)
    {
        _score += score;
        textMeshPro.text = _score.ToString();
    }
}
