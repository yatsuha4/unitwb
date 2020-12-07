using UnityEngine;

namespace unitwb {
/**
 */
public class MusicPlayer
  : MonoBehaviour
{
  public AudioClip musicClip;

  /**
   */
  void Start() {
    if(AudioManager.instance is AudioManager manager) {
      if(!manager.IsPlayMusic(musicClip)) {
        manager.PlayMusic(musicClip);
      }
    }
  }
}
}
