using UnityEngine.SceneManagement;
using UnityEngine;

namespace unitwb {
/**
 */
public class Transition
  : MonoBehaviour
{
  private string scene;

  /**
   */
  public void Transit(string scene) {
    this.scene = scene;
    GetComponent<Animator>().SetTrigger("Out");
  }

  /**
   */
  public void OnOut() {
    SceneManager.LoadScene(this.scene);
  }

  /**
   */
  public static Transition Get() {
    return FindObjectOfType<Transition>();
  }
}
}
