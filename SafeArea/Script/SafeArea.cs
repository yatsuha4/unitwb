using UnityEngine;
using UnityEngine.UI;

namespace unitwb {
/**
   セーフエリア
*/
public class SafeArea
  : MonoBehaviour
{
  private RectTransform canvas;
  private Rect rect;

  /**
   */
  void Awake() {
    this.canvas = GetComponentInParent<Canvas>().GetComponent<RectTransform>();
    UpdateLayout();
  }

  /**
   */
  void Update() {
    if(this.canvas.rect != this.rect) {
      this.rect = this.canvas.rect;
      UpdateLayout();
    }
  }

  /**
   */
  private void UpdateLayout() {
    var resolution = new Vector2(Screen.width, Screen.height);
    var safeArea = Screen.safeArea;
    var scale = (Vector2)this.canvas.localScale;
    var transform = GetComponent<RectTransform>();
    transform.offsetMin = safeArea.min / scale;
    transform.offsetMax = (safeArea.max - resolution) / scale;
  }
}
}
