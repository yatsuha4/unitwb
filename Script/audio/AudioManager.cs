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

  public GameObject soundPrefab;

  private List<AudioClip> playSounds = new List<AudioClip>();

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
  public GameObject PlaySound(AudioClip clip, float volume = 1.0f) {
    if(!this.playSounds.Contains(clip)) {
      if(Instantiate(this.soundPrefab, this.transform) is GameObject obj) {
        var soundObj = obj.GetComponent<SoundObject>();
        soundObj.Play(clip, volume);
        this.playSounds.Add(clip);
        return obj;
      }
    }
    return null;
  }
}
}
