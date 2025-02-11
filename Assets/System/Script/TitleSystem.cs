using UnityEngine;

public class TitleSystem : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Audio.PlayBGM("TitleBGM");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
