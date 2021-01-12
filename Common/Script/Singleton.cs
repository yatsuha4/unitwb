using System;
using UnityEngine;

namespace unitwb {
/**
   シングルトンインスタンス
*/
public class Singleton<T> where T: class {
  private T instance = null;
  private Func<T> creater;

  /**
     コンストラクタ
     @param[in] creater インスタンスを生成する関数
  */
  public Singleton(Func<T> creater = null) {
    this.creater = creater;
  }

  /**
     インスタンスをセットする
     @param[in] instance インスタンス
  */
  public void Set(T instance) {
    Debug.Assert(this.instance == null);
    this.instance = instance;
  }

  /**
     インスタンスを取得する。存在しないときは生成する
     @return インスタンス
  */
  public T Get() {
    if(this.instance == null && this.creater != null) {
      this.instance = this.creater.Invoke();
    }
    return this.instance;
  }

  /**
     インスタンスを解放する
     @return インスタンス
  */
  public T Release() {
    var instance = this.instance;
    this.instance = null;
    return instance;
  }
}
}
