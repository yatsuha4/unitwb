using System;
using System.Threading.Tasks;
using UnityEngine;

namespace unitwb.dialog {
  /**
     <summary>ダイアログ</summary>
  */
  public class Dialog : MonoBehaviour {
    /** <value>開いてる？</value> */
    public bool isOpen { private set; get; } = false;

    /** <value>閉じたときのコールバック</value> */
    public Action onClose = null;

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
      this.isOpen = true;
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
      while(this.isOpen) {
        await Task.Yield();
      }
    }

    /**
     */
    public void OnClose() {
      this.isOpen = false;
      this.onClose?.Invoke();
      this.onClose = null;
      GetComponentInParent<DialogManager>().OnClose();
    }
  }
}
