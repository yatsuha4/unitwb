using System;
using UnityEngine;
using UnityEngine.Events;

namespace unitwb {
/**
   ジェスチャー検出
*/
public class Gesture
  : MonoBehaviour
{
  [Serializable]
  public class OnFlick : UnityEvent<Vector2> {}

  public int button = 0;
  public float flickTime = 0.3f;
  public float flickLength = 0.2f;
  public OnFlick onFlick = null;

  private Vector2 pos = Vector2.zero;
  private float time = 0.0f;

  /**
     更新
  */
  void Update() {
    if(Input.GetMouseButtonDown(this.button)) {
      var pos = (Vector2)Input.mousePosition;
      if(IsContain(pos)) {
        this.pos = GetPos(pos);
        this.time = Time.time;
      }
    }
    if(Input.GetMouseButtonUp(this.button)) {
      if(this.time > 0.0f) {
        var v = GetPos((Vector2)Input.mousePosition) - this.pos;
        var t = Time.time - this.time;
        this.time = 0.0f;
        if(t < this.flickTime && v.magnitude >= this.flickLength) {
          this.onFlick?.Invoke(v.normalized);
        }
      }
    }
  }

  /**
     相対座標を取得する
     @param[in] screenPos スクリーン座標
     @return 相対座標
  */
  private Vector2 GetPos(Vector2 screenPos) {
    return screenPos / Mathf.Min(Screen.width, Screen.height);
  }

  /**
     スクリーン座標が自身内か調べる
     @param[in] screenPos スクリーン座標
     @return 自身内のとき真
  */
  private bool IsContain(Vector2 screenPos) {
    if(GetComponent<RectTransform>() is RectTransform rect) {
      return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPos);
    }
    return true;
  }
}
}
