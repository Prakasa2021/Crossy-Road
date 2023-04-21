using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    static AudioSource bgmInstance;

    [SerializeField] AudioSource bgm;

    public bool IsMute { get => bgm.mute; }
    public float BgmVolume { get => bgm.volume; }

    private void Start() 
    {
        if(bgmInstance != null)
        {
            Destroy(bgm.gameObject);
            bgm = bgmInstance;
        }
        else
        {
            bgmInstance = bgm;
            bgm.transform.SetParent(null);
            DontDestroyOnLoad(bgm.gameObject);
        }
    }

    public void PlayBGM(AudioClip clip, bool loop = true)
    {
        if(bgm.isPlaying)
            bgm.Stop();

        bgm.clip = clip;
        bgm.loop = loop;
        bgm.Play();
    }

    public void SetMute(bool value)
    {
        bgm.mute = value;
    }

    public void SetBgmVolume(float value)
    {
        bgm.volume = value;
    }
}
