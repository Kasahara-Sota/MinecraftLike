using UnityEngine;
using DG.Tweening;

public class Fade : MonoBehaviour
{
    private void Start()
    {
        FadeIn();
    }
    public void FadeIn()
    {
        if (TryGetComponent<CanvasGroup>(out CanvasGroup canvasGroup))
        {
            canvasGroup.DOFade(0.0f, 1f).SetEase(Ease.Linear).OnComplete(() => gameObject.SetActive(false));
        }
        else
        {
            return;
        }
    }
    public void FadeOut()
    {
        if (TryGetComponent<CanvasGroup>(out CanvasGroup canvasGroup))
        {
            canvasGroup.DOFade(1.0f, 1f).SetEase(Ease.Linear);
        }
        else
        {
            return;
        }
    }
    public void FadeOut(string sceneName)
    {
        if (TryGetComponent<CanvasGroup>(out CanvasGroup canvasGroup))
        {
            canvasGroup.DOFade(1.0f, 1f).SetEase(Ease.Linear)
                                        .OnComplete(() => FindAnyObjectByType<SceneLoader>().ChangeScene(sceneName));
        }
        else
        {
            return;
        }
    }
}
