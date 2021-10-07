using UnityEngine;

namespace Towerb
{
  /**
     <summary>ダイアログマネージャ</summary>
  */
  public class DialogManager
    : MonoBehaviour
  {
    private Dialog dialog = null;

    /**
       <summary>ダイアログを開く</summary>
       <param name="dialog">ダイアログ</summary>
    */
    public Dialog Open(Dialog dialog)
    {
      Close();
      GetComponent<Animator>().SetBool("Open", true);
      dialog.Open();
      this.dialog = dialog;
      return dialog;
    }

    /**
       <summary>ダイアログを閉じる</summary>
    */
    public void Close()
    {
      this.dialog?.Close();
      this.dialog = null;
    }

    /**
     */
    public void OnClose()
    {
      GetComponent<Animator>().SetBool("Open", false);
    }
  }
}
