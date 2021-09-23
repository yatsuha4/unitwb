using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace unitwb {
/**
 */
public class Transition
  : MonoBehaviour
{
  public bool autoIn = true;
  public UnityEvent onIn;
  private System.Action callback;

  public bool IsIn { private set; get; } = false;

  /**
   */
  void Awake() {
    GetComponent<Animator>().SetBool("In", this.autoIn);
  }

  /**
   */
  public async Task In() {
    GetComponent<Animator>().SetBool("In", true);
    await WaitIn();
  }

  /**
   */
  public async Task WaitIn()
  {
    while(!this.IsIn) {
      await Task.Yield();
    }
  }

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
    GetComponent<Animator>().SetBool("In", false);
  }

  /**
   */
  public void OnIn() {
    this.IsIn = true;
    this.onIn?.Invoke();
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
