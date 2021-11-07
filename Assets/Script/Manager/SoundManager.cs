using System.Linq.Expressions;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum SFX
{
    buy,
    swipePetal,
    uiTouch
    // takeOff,
    // plant
}

public enum BGM
{
    main,
    bgm
    // takeOff,
    // plant
}

public class SoundManager : Singleton<SoundManager>
{
    public bool IsPlayingAudio => this.audioSource.isPlaying || (!this.audioSource.isPlaying && this.audioSource.time != 0);
    public AudioSource AudioSource => this.audioSource;
    public void PlayBGMSound(BGM bgm)
    {
        Debug.Log("PlayBGM : " + bgm);
        this.audioSource.clip = clipMusicList.Find(l => l.name == bgm.ToString());
        this.audioSource.volume = (PlayerPrefs.GetInt(StringConst.PREF_ON_BG, NumberConst.TRUE) == NumberConst.TRUE) ? PlayerPrefs.GetFloat(StringConst.PREF_VOLUME_BG, 0.5f) : 0.0f;

        this.audioSource.time = 0;
        this.audioSource.Play();

    }

    public void StopBGMSound()
    {
        this.audioSource.Pause();
    }

    public void ChangeSpeed(float speed)
    {
        this.audioSource.pitch = speed;
        Debug.Log("curSpeed : " + speed);
    }

    public float CurrentMusicTime()
    {
        if (this.audioSource == null || this.audioSource.clip == null) return 0;

        return this.audioSource.time;
    }

    public void ContinueBGMSound()
    {
        this.audioSource.UnPause();
    }

    public void PlaySFXSound(SFX sfx)
    {
        SfxSound sfxSound = Instantiate<SfxSound>(this.sfxAudioSource);
        sfxSound.Init(this.clipSFXList.Find(l => l.name == sfx.ToString()));
    }

    public void ChangeBGSound(float volume)
    {
        this.audioSource.volume = volume;
    }


    [SerializeField] List<AudioClip> clipMusicList;
    [SerializeField] List<AudioClip> clipSFXList;
    [SerializeField] AudioSource audioSource;
    [SerializeField] SfxSound sfxAudioSource;
}
