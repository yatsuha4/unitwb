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
    if(!Update(this.coroutine)) {
      Stop();
    }
  }

  /**
   */
  private static bool Update(IEnumerator iter) {
    if(iter.Current is IEnumerator child) {
      if(Update(child)) {
        return true;
      }
    }
    return iter.MoveNext();
  }

  /**
   */
  public static EditorCoroutine Start(IEnumerator coroutine) {
    return new EditorCoroutine(coroutine);
  }
}
}
