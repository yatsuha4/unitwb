using UnityEngine;

namespace unitwb.dialog {
/**
   ダイアログマネージャ
*/
public class DialogManager
  : MonoBehaviour
{
  private Singleton<Dialog> dialog = null;

  /**
   */
  void Awake() {
    this.dialog = new Singleton<Dialog>();
  }

  /**
   */
  void Update() {
  }

  /**
     ダイアログを開く
     @param[in] prefab ダイアログプレハブ
  */
  public void Open(GameObject prefab) {
    Close();
    var obj = Instantiate(prefab, this.transform);
    var dialog = obj.GetComponent<Dialog>();
    Debug.Assert(dialog != null);
    this.dialog.Set(dialog);
    GetComponent<Animator>().SetBool("Open", true);
  }

  /**
     ダイアログを閉じる
  */
  public void Close() {
    GetComponent<Animator>().SetBool("Open", false);
    this.dialog.Release()?.Close();
  }
}
}
