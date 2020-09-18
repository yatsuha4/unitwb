using UnityEngine.Audio;
using UnityEngine;

namespace unitwb.audio {
/**
   オーディオオブジェクト
*/
public class AudioObject
  : MonoBehaviour
{
  /**
     フェード用クラス
  */
  class Fade {
    private float src;
    private float dst;
    private float time;
    private float t;

    /**
     */
    public Fade(float src, float dst, float time) {
      this.src = src;
      this.dst = dst;
      this.time = time;
      this.t = 0.0f;
    }

    /**
     */
    public float Update() {
      this.t += Time.deltaTime;
      return Mathf.Lerp(this.src, this.dst, Mathf.Clamp01(this.t / this.time));
    }

    /**
     */
    public bool isEnd {
      get {
        return this.t >= this.time;
      }
    }
  }

  public AudioSource audioSource { private set; get; }
  private Fade fade = null;

  /**
   */
  void Awake() {
    this.audioSource = GetComponent<AudioSource>();
  }

  /**
   */
  void Update() {
    if(!this.audioSource.isPlaying) {
      Destroy(this.gameObject);
    }
    else if(this.fade != null) {
      this.audioSource.volume = this.fade.Update();
      if(this.fade.isEnd) {
        this.fade = null;
        if(this.audioSource.volume == 0.0f) {
          this.audioSource.Stop();
          Destroy(this.gameObject);
        }
      }
    }
  }

  /**
     再生する
     @param[in] group グループ
     @param[in] clip クリップ
     @param[in] volume ボリューム
     @param[in] loop ループ
  */
  public AudioObject Play(AudioMixerGroup group, 
                          AudioClip clip, 
                          float volume = 1.0f, 
                          bool loop = false) {
    this.audioSource.outputAudioMixerGroup = group;
    this.audioSource.clip = clip;
    this.audioSource.volume = volume;
    this.audioSource.loop = loop;
    this.audioSource.Play();
    return this;
  }

  /**
     ボリュームを変更する
     @param[in] volume ボリューム
     @param[in] time 変化時間(秒)
  */
  public void SetVolume(float volume, float time = 0.0f) {
    if(time > 0.0f) {
      this.fade = new Fade(this.audioSource.volume, volume, time);
    }
    else {
      this.audioSource.volume = volume;
    }
  }

  /**
   */
  public void Stop(float time = 0.0f) {
    if(time > 0.0f) {
      SetVolume(0.0f, time);
    }
    else {
      this.audioSource.Stop();
      Destroy(this.gameObject);
    }
  }

  /**
     クリップを取得する
  */
  public AudioClip clip {
    get {
      return this.audioSource.clip;
    }
  }
}
}
