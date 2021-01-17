using UnityEngine;

namespace unitwb.dialog {
/**
   ダイアログ
*/
public class Dialog
  : MonoBehaviour
{
  /**
     閉じる
  */
  public void Close() {
    GetComponentInParent<DialogManager>().Close();
  }

  /**
   */
  public void OnClose() {
    GetComponent<Animator>().SetBool("Close", true);
  }
}
}
