using UnityEngine;

namespace unitwb.audio {
/**
   サウンドオブジェクト
*/
public class SoundObject
  : MonoBehaviour
{
  /**
     サウンドを再生する
     @param[in] clip クリップ
     @param[in] volume ボリューム
  */
  public void Play(AudioClip clip, float volume) {
    GetComponent<AudioSource>().PlayOneShot(clip, volume);
  }

  /**
   */
  void Update() {
    if(!GetComponent<AudioSource>().isPlaying) {
      Destroy(this.gameObject);
    }
  }
}
}
