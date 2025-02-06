using UnityEngine;
using TMPro;
using DG.Tweening;

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
        ulong startScore = _score;
        ulong targetScore = _score + score;

        // 既存のアニメーションをキャンセル
        DOTween.Kill("ScoreTween");

        DOTween.To(() => startScore, x =>
        {
            startScore = x;
            textMeshPro.text = "SCORE:" + startScore.ToString("D9");
        }, targetScore, 1.0f)
        .SetEase(Ease.OutQuad)
        .SetId("ScoreTween"); // IDを設定して管理

        _score = targetScore;
    }
}
