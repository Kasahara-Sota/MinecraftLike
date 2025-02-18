using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] List<BGMs> _bgms;
    [SerializeField] List<SEs> _ses;
    public static AudioManager Audio;
    private AudioSource _audioSource;
    [Serializable]
    public struct BGMs
    {
        public string Name;
        public AudioClip BGM;
    }
    [Serializable]
    public struct SEs
    {
        public string Name;
        public AudioClip SE;
    }
    private void Awake()
    {
        if (Audio == null)
        {
            Audio = this;
            DontDestroyOnLoad(this);
            if (TryGetComponent<AudioSource>(out AudioSource audioSource))
            {
                _audioSource = audioSource;
            }
            else
            {
                _audioSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void PlaySE(string name)
    {
        AudioClip audioClip = _ses.Find(x => x.Name == name).SE;
        if (audioClip == null)
        {
            Debug.LogWarning($"ë∂ç›ÇµÇ»Ç¢å¯â âπ:{name}");
            return;
        }
        _audioSource.PlayOneShot(audioClip);
    }
    public void PlayBGM(string name)
    {
        AudioClip audioClip = _bgms.Find(x => x.Name == name).BGM;
        if (audioClip == null)
        {
            Debug.LogWarning($"ë∂ç›ÇµÇ»Ç¢BGM:{name}");
            return;
        }
        _audioSource.clip = audioClip;
        _audioSource.Play();
        _audioSource.loop = true;
    }
    public void StopBGM()
    {
        _audioSource?.Stop();
    }
}
