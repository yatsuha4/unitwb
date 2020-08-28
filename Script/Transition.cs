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
  public static Transition Get() {
    return FindObjectOfType<Transition>();
  }
}
}
