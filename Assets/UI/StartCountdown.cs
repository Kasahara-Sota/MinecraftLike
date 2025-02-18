using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartCountdown : MonoBehaviour
{
    [SerializeField] List<GameObject> _countdownUI;
    [SerializeField] GameObject _startUI;
    void Start()
    {
        StartCoroutine(Countdown());
    }
    IEnumerator Countdown()
    {
        foreach (var item in _countdownUI)
        {
            item.SetActive(false);
        }
        _startUI.SetActive(false);
        yield return new WaitForSeconds(1);
        for (int i = 0;i < _countdownUI.Count; i++)
        {
            _countdownUI[i].SetActive(true);
            AudioManager.Audio.PlaySE("Countdown");
            yield return new WaitForSeconds(1f);
            _countdownUI[i].SetActive(false);
        }
        _startUI.SetActive(true);
        AudioManager.Audio.PlaySE("Start");
        yield return new WaitForSeconds(0.8f);
        _startUI.SetActive(false) ;
    }
}
