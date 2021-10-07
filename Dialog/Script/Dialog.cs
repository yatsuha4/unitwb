using System;
using System.Threading.Tasks;
using Towerb.Audio;
using UnityEngine;

namespace Towerb
{
  /**
     <summary>ダイアログ</summary>
  */
  public class Dialog
    : MonoBehaviour
  {
    /** <value>閉じたときのコールバック</value> */
    public Action onClose = null;

    public AudioClip openSound;
    public AudioClip closeSound;

    private Animator animator;

    /**
     */
    void Awake()
    {
      this.animator = GetComponent<Animator>();
    }

    /**
       <value>開いてる？</value>
    */
    public bool IsOpen
    {
      get
      {
        return !this.animator.GetBool("Close");
      }
    }

    /**
       <summary>開く</summary>
    */
    public void Open()
    {
      AudioManager.Instance.PlaySound(this.openSound);
      this.animator.SetBool("Close", false);
      this.animator.SetTrigger("Open");
    }

    /**
       <summary>閉じる</summary>
    */
    public void Close()
    {
      if(this.IsOpen)
      {
        AudioManager.Instance.PlaySound(this.closeSound);
        this.animator.SetBool("Close", true);
        this.onClose?.Invoke();
        this.onClose = null;
        GetComponentInParent<DialogManager>().OnClose();
      }
    }

    /**
       <summary>閉じるまで待つ</summary>
    */
    public async Task Modal()
    {
      while(this.IsOpen)
      {
        await Task.Yield();
      }
    }
  }
}
