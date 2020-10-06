using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

namespace unitwb.audio {
/**
   オーディオマネージャー
*/
public class AudioManager
  : MonoBehaviour
{
  public enum Mixer {
    Sound, 
    Music
  }

  public static AudioManager instance { private set; get; } = null;

  public AudioMixer mixer;
  public AudioMixerGroup[] mixers;
  public GameObject audioPrefab;

  private List<AudioClip> playSounds = new List<AudioClip>();
  private AudioObject music = null;

  /**
   */
  void Awake() {
    if(instance == null) {
      instance = this;
      DontDestroyOnLoad(this.gameObject);
    }
    else {
      Destroy(this.gameObject);
    }
  }

  /**
   */
  void LateUpdate() {
    this.playSounds.Clear();
  }

  /**
     サウンドを再生する
     @param[in] clip クリップ
     @param[in] volume ボリューム
     @return オブジェクト
  */
  public AudioObject PlaySound(AudioClip clip, float volume = 1.0f) {
    if(!this.playSounds.Contains(clip)) {
      if(Play(this.audioPrefab) is AudioObject audioObj) {
        audioObj.Play(GetMixer(Mixer.Sound), clip, volume);
        this.playSounds.Add(clip);
        return audioObj;
      }
    }
    return null;
  }

  /**
     曲を再生する
     @param[in] clip クリップ
     @param[in] fadeTime フェード時間
     @param[in] volume ボリューム
     @param[in] loop ループ
  */
  public AudioObject PlayMusic(AudioClip clip, 
                               float fadeTime = 0.0f, 
                               float volume = 1.0f, 
                               bool loop = true) {
    StopMusic(fadeTime);
    if(Play(this.audioPrefab) is AudioObject audioObj) {
      if(fadeTime > 0.0f) {
        audioObj.Play(GetMixer(Mixer.Music), clip, 0.0f, loop).
          SetVolume(volume, fadeTime);
      }
      else {
        audioObj.Play(GetMixer(Mixer.Music), clip, volume, loop);
      }
      this.music = audioObj;
      return audioObj;
    }
    return null;
  }

  /**
     曲が再生中か調べる
     @param[in] clip クリップ
     @return clipが再生中のとき真
  */
  public bool IsPlayMusic(AudioClip clip) {
    return this.music != null && this.music.clip == clip;
  }

  /**
     曲を停止する
     @param[in] time フェードアウト時間
  */
  public void StopMusic(float time = 0.5f) {
    if(this.music != null) {
      this.music.SetVolume(0.0f, time);
      this.music = null;
    }
  }

  /**
   */
  public AudioObject Play(GameObject prefab) {
    return Instantiate(prefab, this.transform)?.GetComponent<AudioObject>();
  }

  /**
   */
  public void SetMasterVolume(Mixer mixer, float volume) {
    this.mixer.SetFloat(mixer.ToString(), 
                        (volume > 0.0f)
                        ? Mathf.Clamp(Mathf.Log10(volume) * 20.0f, -80.0f, 0.0f)
                        : -80.0f);
  }

  /**
   */
  private AudioMixerGroup GetMixer(Mixer mixer) {
    return this.mixers[(int)mixer];
  }

  /**
   */
  public void StopAll(float time = 0.0f) {
    foreach(var audioObject in GetComponentsInChildren<AudioObject>()) {
      audioObject.Stop(time);
    }
  }
}
}
