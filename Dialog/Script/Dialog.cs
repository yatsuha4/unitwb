using System;
using System.Threading.Tasks;
using towerb.audio;
using UnityEngine;
using UnityEngine.Events;

namespace towerb.dialog
{
    /**
       <summary>ダイアログ</summary>
    */
    public class Dialog
        : MonoBehaviour
    {
        public AudioClip openSound;
        public AudioClip closeSound;

        /**
         * <value>閉じたときのイベント</value>
         */
        public UnityEvent onClose;

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
                this.onClose.Invoke();
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
