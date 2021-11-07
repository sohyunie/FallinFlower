using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetUpPop : UEPopup
{
    public static SetUpPop Instance;
    public static SetUpPop ShowPop()
    {
        Instance = UEPopup.GetInstantiateComponent<SetUpPop>();
        Instance.ShowPopUp();
        Instance.Init();
        return Instance;
    }

    public void Init()
    {
        this.onBgSound = (PlayerPrefs.GetInt(StringConst.PREF_ON_BG, NumberConst.TRUE) == NumberConst.TRUE) ? true : false;
        this.onEffectSound = (PlayerPrefs.GetInt(StringConst.PREF_IS_EFFECT, NumberConst.TRUE) == NumberConst.TRUE) ? true : false;
        this.SetBgButton(onBgSound);
        this.SetEffectButton(onEffectSound);

        this.BgSlider.value = this.onBgSound ? PlayerPrefs.GetFloat(StringConst.PREF_VOLUME_BG, 0.5f) : 0.0f;
        this.EffectSlider.value = this.onEffectSound ? PlayerPrefs.GetFloat(StringConst.PREF_VOLUME_EFFECT, 0.5f) : 0.0f;

        this.SetVibrateButton(PlayerPrefs.GetInt(StringConst.PREF_IS_VIBRATE, (int)NumberConst.TRUE) == (int)NumberConst.TRUE);
    }

    public void OnBgSoundOn()
    {
        // SoundManager.Instance.PlaySFXSound(SFX.click2);
        PlayerPrefs.SetInt(StringConst.PREF_ON_BG, NumberConst.TRUE);
        PlayerPrefs.Save();
        this.SetBgButton(true);

        this.BgSlider.value = PlayerPrefs.GetFloat(StringConst.PREF_VOLUME_BG, 0.5f);
        PlayerPrefs.SetFloat(StringConst.PREF_VOLUME_BG, this.BgSlider.value);
    }
    public void OnBgSoundOff()
    {
        // SoundManager.Instance.PlaySFXSound(SFX.click2);
        PlayerPrefs.SetInt(StringConst.PREF_ON_BG, NumberConst.FALSE);
        PlayerPrefs.Save();
        this.SetBgButton(false);
        this.BgSlider.value = 0;
    }
    public void OnBgSoundControl(Slider slider)
    {
        // BG가 OFF 상태일 때
        if (PlayerPrefs.GetInt(StringConst.PREF_ON_BG, NumberConst.TRUE) == NumberConst.FALSE)  
        {
            if (slider.value != 0)
            {
                PlayerPrefs.SetInt(StringConst.PREF_ON_BG, NumberConst.TRUE);
                PlayerPrefs.SetFloat(StringConst.PREF_VOLUME_BG, slider.value);
                PlayerPrefs.Save();
                this.SetBgButton(true);
            }
        }
        else
        {
            PlayerPrefs.SetFloat(StringConst.PREF_VOLUME_BG, slider.value);
            PlayerPrefs.Save();
        }
        this.BgSlider.value = slider.value;
        //  SoundManager.Instance.PlaySFXSound(SFX.click2);
        SoundManager.Instance.ChangeBGSound(slider.value);
        // this.soundManagerSlider.value = this.BgSlider.value;
    }
    public void OnEffectSoundOn()
    {
        
        PlayerPrefs.SetInt(StringConst.PREF_IS_EFFECT, NumberConst.TRUE);
        this.SetEffectButton(true);
        // SoundManager.Instance.PlaySFXSound(SFX.click2);
        PlayerPrefs.Save();

        this.EffectSlider.value = PlayerPrefs.GetFloat(StringConst.PREF_VOLUME_EFFECT,0.5f);
        PlayerPrefs.SetFloat(StringConst.PREF_VOLUME_EFFECT, this.EffectSlider.value);
    }
    public void OnEffectSoundOff()
    {
        //SoundManager.Instance.PlaySFXSound(SFX.click2);
        PlayerPrefs.SetInt(StringConst.PREF_IS_EFFECT, NumberConst.FALSE);
        this.SetEffectButton(false);
        PlayerPrefs.Save();
        this.EffectSlider.value = 0;
    }
    public void OnEffectSoundControl(Slider slider)
    {
        if(PlayerPrefs.GetInt(StringConst.PREF_IS_EFFECT, NumberConst.TRUE) == NumberConst.FALSE)
        {
            if(slider.value != 0)
            {
                PlayerPrefs.SetInt(StringConst.PREF_IS_EFFECT, NumberConst.TRUE);
                PlayerPrefs.SetFloat(StringConst.PREF_VOLUME_EFFECT, slider.value);
                PlayerPrefs.Save();
                this.SetEffectButton(true);
            }   
        }
        else
        {    
            PlayerPrefs.SetFloat(StringConst.PREF_VOLUME_EFFECT, slider.value);
            PlayerPrefs.Save();
        }
        //SoundManager.Instance.PlaySFXSound(SFX.click2);
        this.EffectSlider.value = slider.value;
    }

    public void OnVibrateOff()
    {
        PlayerPrefs.SetInt(StringConst.PREF_IS_VIBRATE, NumberConst.FALSE);
        this.SetVibrateButton(false);
    }

    public void OnVibrateOn()
    {
        PlayerPrefs.SetInt(StringConst.PREF_IS_VIBRATE, NumberConst.TRUE);
        this.SetVibrateButton(true);
    }

    public void ExitPop()
    {
        // SoundManager.Instance.PlaySFXSound(SFX.click2);
        this.DestroyPopUp();
    }

    public float GetSliderValue()
    {
        // this.soundManagerSlider.value = this.BgSlider.value;
        return this.BgSlider.value;
    }
    [SerializeField] private Image BgOn;
    [SerializeField] private Image BgOff;
    [SerializeField] private Image EffectOn;
    [SerializeField] private Image EffectOff;
    [SerializeField] private Slider BgSlider;
    [SerializeField] private Slider EffectSlider;
    [SerializeField] private Color dimmedColor;
    [SerializeField] private Image vibrateOn;
    [SerializeField] private Image vibrateOff;

    private bool onBgSound;
    private bool onEffectSound;
    public Slider soundManagerSlider;

    private void SetBgButton(bool isOn)
    {
        this.BgOn.color = (isOn) ? Color.white : this.dimmedColor;
        this.BgOff.color = (!isOn) ? Color.white : this.dimmedColor;
    }

    private void SetVibrateButton(bool isOn)
    {
        this.vibrateOn.color = (isOn) ? Color.white : this.dimmedColor;
        this.vibrateOff.color = (!isOn) ? Color.white : this.dimmedColor;
    }

    private void SetEffectButton(bool isOn)
    {
        this.EffectOn.color = (isOn) ? Color.white : this.dimmedColor;
        this.EffectOff.color = (!isOn) ? Color.white : this.dimmedColor;
    }
    public void OnClickInsta()
    {
        Application.OpenURL("https://www.instagram.com/ndolphinconnect");
    }

    public void OnClickFaceBook()
    {
        Application.OpenURL("https://www.facebook.com/%EC%97%94%EB%8F%8C%ED%95%80%EC%BB%A4%EB%84%A5%ED%8A%B8-103441671801072");
    }

    public void OnClickYoutube()
    {
        Application.OpenURL("https://www.youtube.com/channel/UCkHRdqi1NBY5i64jXPOjYGg/featured");
    }
}
