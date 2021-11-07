using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SfxSound : MonoBehaviour
{
    // Start is called before the first frame update
    public void Init(AudioClip clip)
    {
        this.audioSource.clip = clip;
        this.audioSource.Play();
        this.audioSource.volume = (PlayerPrefs.GetInt(StringConst.PREF_IS_EFFECT, NumberConst.TRUE) == NumberConst.TRUE)? PlayerPrefs.GetFloat(StringConst.PREF_VOLUME_EFFECT, 0.5f) : 0.0f;
        this.StartCoroutine(this.EndClip());
    }
    [SerializeField] private AudioSource audioSource;
    private IEnumerator EndClip()
    {
        yield return new WaitUntil(() => !this.audioSource.isPlaying);
        
        Destroy(this.gameObject);
    }
}
