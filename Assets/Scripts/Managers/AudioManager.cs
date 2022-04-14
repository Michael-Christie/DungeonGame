using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Space]
    [SerializeField] private AudioClip[] musicSoundFiles;

    [SerializeField] private AudioClip[] sfxSoundFiles;

    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    private bool isSFXMuted;
    public bool IsSFXMuted
    {
        get
        {
            return isSFXMuted;
        }
        private set
        {
            isSFXMuted = value;
            sfxSource.mute = isSFXMuted;
            PlayerPrefs.SetInt(GameConstants.PlayerPrefs.isSFXMuted, isSFXMuted ? 1 : 0);
        }
    }

    private bool isMusicMuted;
    public bool IsMusicMuted
    {
        get
        {
            return isMusicMuted;
        }
        private set
        {
            isMusicMuted = value;
            musicSource.mute = isMusicMuted;
            PlayerPrefs.SetInt(GameConstants.PlayerPrefs.isMusicMuted, isMusicMuted ? 1 : 0);
        }
    }

    //
    private void Awake()
    {
        Instance = this;
        isSFXMuted = PlayerPrefs.GetInt(GameConstants.PlayerPrefs.isSFXMuted, 0) == 1;
        isMusicMuted = PlayerPrefs.GetInt(GameConstants.PlayerPrefs.isMusicMuted, 0) == 1;
    }

    public void PlayMusic(GameConstants.MusicClip _music)
    {
        musicSource.Stop();
        musicSource.clip = musicSoundFiles[(int)_music];
        musicSource.Play();
    }

    public void PlaySoundEffect(GameConstants.SoundClip _sound)
    {
        sfxSource.PlayOneShot(sfxSoundFiles[(int)_sound]);
    }
}
