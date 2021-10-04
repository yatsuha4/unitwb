using UnityEngine;

namespace unitwb {
/**
 */
public class MusicPlayer
  : MonoBehaviour
{
  public AudioClip[] musicClips;
  public float crossFadeTime = 1.0f;

  /**
   */
  void Start() {
    if(this.musicClips.Length > 0) {
      PlayMusic(this.musicClips[0]);
    }
  }

  /**
   */
  public AudioObject PlayMusic(int index) {
    return PlayMusic(this.musicClips[index]);
  }

  /**
   */
  public AudioObject PlayMusic(AudioClip musicClip) {
    return PlayMusic(musicClip, this.crossFadeTime);
  }

  /**
   */
  public AudioObject PlayMusic(AudioClip musicClip, float crossFadeTime) {
    if(AudioManager.instance is AudioManager manager) {
      if(!manager.IsPlayMusic(musicClip)) {
        return manager.PlayMusic(musicClip, crossFadeTime: crossFadeTime);
      }
    }
    return null;
  }
}
}
