using System.Collections;
using UnityEngine;

namespace unitwb {
/**
   シングルトンなコルーチン
*/
public class SingleCoroutine {
  private MonoBehaviour owner;
  private Coroutine coroutine;

  /**
   */
  public SingleCoroutine(MonoBehaviour owner) {
    this.owner = owner;
    this.coroutine = null;
  }

  /**
   */
  public void Start(IEnumerator coroutine) {
    Stop();
    this.coroutine = this.owner.StartCoroutine(coroutine);
  }

  /**
   */
  public void Stop() {
    if(this.coroutine != null) {
      this.owner.StopCoroutine(this.coroutine);
      this.coroutine = null;
    }
  }
}
}
