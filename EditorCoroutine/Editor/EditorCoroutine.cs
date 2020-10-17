using System.Collections;
using UnityEditor;

namespace unitwb.editor {
/**
   エディタ用コルーチン
*/
public class EditorCoroutine {
  private IEnumerator coroutine;

  /**
   */
  private EditorCoroutine(IEnumerator coroutine) {
    this.coroutine = coroutine;
    EditorApplication.update += Update;
  }

  /**
   */
  public void Stop() {
    if(this.coroutine != null) {
      EditorApplication.update -= Update;
      this.coroutine = null;
    }
  }

  /**
   */
  private void Update() {
    if(!this.coroutine.MoveNext()) {
      Stop();
    }
  }

  /**
   */
  public static EditorCoroutine Start(IEnumerator coroutine) {
    return new EditorCoroutine(coroutine);
  }
}
}
