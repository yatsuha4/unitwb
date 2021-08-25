using UnityEngine;
using UnityEngine.UI;

namespace unitwb {
/**
   セーフエリア
*/
public class SafeArea
  : MonoBehaviour
{
  private Canvas canvas;
  private Rect safeArea;
  private float scale = 0.0f;

  /**
   */
  void Awake() {
    this.canvas = GetComponentInParent<Canvas>();
    UpdateLayout();
  }

  /**
   */
  void Update() {
    if(this.canvas.scaleFactor != this.scale || Screen.safeArea != this.safeArea) {
      UpdateLayout();
    }
  }

  /**
   */
  private void UpdateLayout() {
    this.scale = this.canvas.scaleFactor;
    this.safeArea = Screen.safeArea;
    var resolution = new Vector2(Screen.width, Screen.height);
    Debug.Log($"Scale = {this.scale}, SafeArea = {this.safeArea}, Resolution = {resolution}");
    var transform = GetComponent<RectTransform>();
    transform.offsetMin = this.safeArea.min / this.scale;
    transform.offsetMax = (this.safeArea.max - resolution) / this.scale;
  }
}
}
