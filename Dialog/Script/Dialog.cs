using System.Threading.Tasks;
using UnityEngine;

namespace unitwb.dialog {
  /**
     <summary>ダイアログ</summary>
  */
  public class Dialog : MonoBehaviour {
    /** <value>閉じてる？</value> */
    public bool isClose = false;

    private Animator animator;

    /**
     */
    void Awake() {
      this.animator = GetComponent<Animator>();
    }

    /**
       <summary>開く</summary>
    */
    public void Open() {
      this.animator.SetBool("Close", false);
      this.animator.SetTrigger("Open");
    }

    /**
       <summary>閉じる</summary>
    */
    public void Close() {
      this.animator.SetBool("Close", true);
    }

    /**
       <summary>閉じるまで待つ</summary>
    */
    public async Task Modal() {
      while(!this.isClose) {
        await Task.Yield();
      }
    }

    /**
     */
    public void OnClose() {
      GetComponentInParent<DialogManager>().OnClose();
    }
  }
}
