using UnityEngine.SceneManagement;
using UnityEngine;

namespace unitwb {
/**
 */
public class Transition
  : MonoBehaviour
{
  private System.Action callback;

  /**
   */
  public void Transit(string scene) {
    Transit(() => {
      SceneManager.LoadScene(scene);
    });
  }

  /**
   */
  public void Transit(System.Action callback) {
    this.callback = callback;
    GetComponent<Animator>().SetTrigger("Out");
  }

  /**
   */
  public void OnOut() {
    this.callback?.Invoke();
  }

  /**
   */
  public void SetPause(bool isPause) {
    GetComponent<Animator>().speed = isPause ? 0.0f : 1.0f;
  }
  
  /**
   */
  public static Transition Get() {
    return FindObjectOfType<Transition>();
  }
}
}
