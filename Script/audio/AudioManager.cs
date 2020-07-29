using System.Collections.Generic;
using UnityEngine;

namespace unitwb.audio {
/**
   オーディオマネージャー
*/
public class AudioManager
  : MonoBehaviour
{
  public static AudioManager instance { private set; get; } = null;

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
        audioObj.Play(clip, volume);
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
  public void PlayMusic(AudioClip clip, 
                        float fadeTime = 0.5f, 
                        float volume = 1.0f, 
                        bool loop = true) {
    StopMusic(fadeTime);
    if(Play(this.audioPrefab) is AudioObject audioObj) {
      if(fadeTime > 0.0f) {
        audioObj.Play(clip, 0.0f, loop).SetVolume(volume, fadeTime);
      }
      else {
        audioObj.Play(clip, volume, loop);
      }
      this.music = audioObj;
    }
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
}
}
