using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

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
  public float flickLength = 64.0f;
  public OnFlick onFlick = null;

  public float swipeBrake = 0.8f;

  public Vector2 swipe { private set; get; } = Vector2.zero;

  private Camera camera = null;
  private Vector2? pos = null;
  private float time = 0.0f;
  private bool prevButton = false;

  /**
   */
  void Awake() {
    if(GetComponentInParent<Canvas>() is Canvas canvas) {
      this.camera = canvas.worldCamera;
    }
  }

  /**
     更新
  */
  void FixedUpdate() {
    if(Input.GetMouseButton(this.button)) {
      this.swipe = Vector2.zero;
      if(!this.prevButton) {
        var pos = (Vector2)Input.mousePosition;
        if(IsContain(pos)) {
          this.pos = GetPos(pos);
          this.time = Time.time;
        }
      }
      else if(this.pos.HasValue) {
        var pos = (Vector2)Input.mousePosition;
        var localPos = GetPos(pos);
        this.swipe = GetPos(pos) - this.pos.Value;
      }
      this.prevButton = true;
    }
    else {
      if(this.pos.HasValue) {
        var v = GetPos((Vector2)Input.mousePosition) - this.pos.Value;
        var t = Time.time - this.time;
        if(t < this.flickTime && v.magnitude >= this.flickLength) {
          this.onFlick?.Invoke(v.normalized);
        }
        this.pos = null;
      }
      if(this.swipe.magnitude > 0.0f) {
        this.swipe *= this.swipeBrake;
        if(this.swipe.magnitude < 1.0f) {
          this.swipe = Vector2.zero;
        }
      }
      this.prevButton = false;
    }
  }

  /**
     ローカル座標を取得する
     @param[in] screenPos スクリーン座標
     @return ローカル座標
  */
  private Vector2 GetPos(Vector2 screenPos) {
    if(GetComponent<RectTransform>() is RectTransform rect) {
      Vector2 pos;
      if(RectTransformUtility.
         ScreenPointToLocalPointInRectangle(rect, screenPos, this.camera, out pos)) {
        return pos;
      }
      return Vector2.zero;
    }
    return screenPos;
  }

  /**
     スクリーン座標が自身内か調べる
     @param[in] screenPos スクリーン座標
     @return 自身内のとき真
  */
  private bool IsContain(Vector2 screenPos) {
    if(GetComponent<RectTransform>() is RectTransform rect) {
      return RectTransformUtility.RectangleContainsScreenPoint(rect, screenPos, this.camera);
    }
    return true;
  }
}
}
