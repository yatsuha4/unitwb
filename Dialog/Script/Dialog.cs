using System.Threading.Tasks;
using UnityEngine;

namespace unitwb.dialog {
/**
   ダイアログ
*/
public class Dialog
  : MonoBehaviour
{
  private bool isClose = false;

  /**
   */
  public async Task Modal() {
    while(!this.isClose) {
      await Task.Yield();
    }
  }

  /**
     閉じる
  */
  public void Close() {
    GetComponentInParent<DialogManager>().Close();
  }

  /**
   */
  public void OnClose() {
    this.isClose = true;
    GetComponent<Animator>().SetBool("Close", true);
  }
}
}
