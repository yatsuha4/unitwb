using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Towerb
{
  /**
     オーディオマネージャー
  */
  public class AudioManager 
    : SingletonBehaviour<AudioManager>
  {
    public enum Mixer {
      Sound, 
      Music
    }

    [SerializeField]
    private AudioMixer mixer;

    [SerializeField]
    private AudioMixerGroup[] mixers;

    [SerializeField]
    private GameObject audioPrefab;

    private List<AudioClip> playSounds = new List<AudioClip>();
    private AudioObject music = null;

    /**
     */
    void LateUpdate()
    {
      this.playSounds.Clear();
    }

    /**
       <summary>サウンドを再生する</summary>
       <param name="clip">クリップ</param>
       <param name="volume">ボリューム</param>
       <returns>サウンドオブジェクト</returns>
    */
    public AudioObject PlaySound(AudioClip clip, float volume = 1.0f)
    {
      if(clip != null && !this.playSounds.Contains(clip))
      {
        if(Play(this.audioPrefab) is AudioObject audioObj)
        {
          audioObj.Play(GetMixer(Mixer.Sound), clip, volume);
          this.playSounds.Add(clip);
          return audioObj;
        }
      }
      return null;
    }

    /**
       <summary>曲を再生する</summary>
       <param name="clip">クリップ</param>
       <param name="crossFadeTime">クロスフェード時間</param>
       <param name="fadeTime">フェード時間</param>
       <param name="volume">ボリューム</param>
       <param name="loop">ループ</param>
       <returns>オーディオオブジェクト</returns>
    */
    public AudioObject PlayMusic(AudioClip clip, 
                                 float crossFadeTime = 1.0f, 
                                 float fadeTime = 0.0f, 
                                 float volume = 1.0f, 
                                 bool loop = true)
    {
      if(!StopMusic(crossFadeTime))
      {
        crossFadeTime = 0.0f;
      }
      fadeTime = Mathf.Max(fadeTime, crossFadeTime);
      if(Play(this.audioPrefab) is AudioObject audioObj)
      {
        if(fadeTime > 0.0f)
        {
          audioObj.Play(GetMixer(Mixer.Music), clip, 0.0f, loop).
            SetVolume(volume, fadeTime);
        }
        else
        {
          audioObj.Play(GetMixer(Mixer.Music), clip, volume, loop);
        }
        this.music = audioObj;
        return audioObj;
      }
      return null;
    }

    /**
       <summary>曲が再生中か調べる</summary>
       <param name="clip">クリップ</param>
       <returns>clipが再生中のとき真<returns>
    */
    public bool IsPlayMusic(AudioClip clip)
    {
      return this.music != null && this.music.clip == clip;
    }

    /**
       曲を停止する
       @param[in] time フェードアウト時間
       @return 再生中だったとき真
    */
    public bool StopMusic(float time = 0.5f)
    {
      if(this.music != null)
      {
        this.music.SetVolume(0.0f, time);
        this.music = null;
        return true;
      }
      return false;
    }

    /**
     */
    public AudioObject Play(GameObject prefab)
    {
      return Instantiate(prefab, this.transform)?.GetComponent<AudioObject>();
    }

    /**
     */
    public void SetMasterVolume(Mixer mixer, float volume)
    {
      this.mixer.SetFloat(mixer.ToString(), 
                          (volume > 0.0f)
                          ? Mathf.Clamp(Mathf.Log10(volume) * 20.0f, -80.0f, 0.0f)
                          : -80.0f);
    }

    /**
     */
    public void StopAll(float time = 0.0f)
    {
      foreach(var audioObject in GetComponentsInChildren<AudioObject>())
      {
        audioObject.Stop(time);
      }
    }

    /**
     */
    private AudioMixerGroup GetMixer(Mixer mixer)
    {
      return this.mixers[(int)mixer];
    }
  }
}
