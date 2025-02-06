using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMeshPro;
    ulong _score;
    
    void Start()
    {
        AddScore(0);
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
        textMeshPro.text = "SCORE:" + _score.ToString("D9");
    }
}
