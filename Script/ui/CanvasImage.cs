using UnityEngine;
using UnityEngine.UI;

namespace unitwb.ui {
/**
   キャンバスを埋める画像
*/
public class CanvasImage
  : MonoBehaviour
{
  private Rect canvasRect;
  private Vector2 size;

  /**
   */
  void Awake() {
    this.size = GetComponent<RectTransform>().rect.size;
  }

  /**
   */
  void Update() {
    if(GetComponentInParent<Canvas>() is Canvas canvas) {
      var canvasTransform = canvas.GetComponent<RectTransform>();
      if(this.canvasRect != canvasTransform.rect) {
        this.canvasRect = canvasTransform.rect;
        var transform = GetComponent<RectTransform>();
        transform.sizeDelta = this.size * 
          Mathf.Max(this.canvasRect.width / this.size.x, 
                    this.canvasRect.height / this.size.y);
      }
    }
  }
}
}
