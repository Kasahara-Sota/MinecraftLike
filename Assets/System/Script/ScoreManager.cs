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

        // �����̃A�j���[�V�������L�����Z��
        DOTween.Kill("ScoreTween");

        DOTween.To(() => startScore, x =>
        {
            startScore = x;
            textMeshPro.text = "SCORE:" + startScore.ToString("D9");
        }, targetScore, 1.0f)
        .SetEase(Ease.OutQuad)
        .SetId("ScoreTween"); // ID��ݒ肵�ĊǗ�

        _score = targetScore;
    }
}
