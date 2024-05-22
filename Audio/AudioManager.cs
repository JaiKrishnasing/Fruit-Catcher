using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Menu/Settings")]
    public AudioSource buttonSettingsMenu;

    [Header("Score")]
    public AudioSource scoreSFX;

    [Header("Game Music")]
    public AudioSource gameMusic;

    #region Singelton
    public static AudioManager instance;

    private void Awake()
    {
        instance = this;
    }

#endregion


    public void PlayButtonSettingsMenu()
    {
        buttonSettingsMenu.Play();
    }

    public void PlayScoreSFX()
    {
        scoreSFX.Play();
    }

    public void PlayGameMusic()
    {
        gameMusic.Play();
    }

    public void PauseGameMusic()
    {
        gameMusic.Pause();
    }
}
