using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 声音管理器
/// </summary>
public class SoundManager
{
    private AudioSource bgmSource; //播放bgm的音频组件

    private Dictionary<string, AudioClip> clips;

    private bool isStop;

    public bool IsStop
    {
        get
        {
            return isStop;
        }
        set
        {
            isStop = value;
            if(isStop == true)
            {
                bgmSource.Pause();
            }
            else
            {
                bgmSource.Play();
            }
        }
    }

    private float bgmVolume;//bgm音量大小

    public float BgmVolume
    {
        get
        {
            return bgmVolume;
        }
        set
        {
            bgmVolume = value;
            bgmSource.volume = bgmVolume;
        }
    }

    private float effectVolume;//音效大小(攻击 受伤等短音效)

    public float EffectVolume
    {
        get
        {
            return effectVolume;
        }
        set
        {
            effectVolume = value;
        }
    }


    public SoundManager()
    {
        clips = new Dictionary<string, AudioClip>();
        bgmSource = GameObject.Find("game").GetComponent<AudioSource>();
        IsStop = false;
        BgmVolume = 1;
        EffectVolume = 1;
    }

    public void PlayBGM(string res)
    {
        if(isStop == true)
        {
            return;
        }
        if (clips.ContainsKey(res) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Sounds/{res}");
            clips.Add(res,clip);
        }
        bgmSource.clip = clips[res];
        bgmSource.Play();
    }
}
