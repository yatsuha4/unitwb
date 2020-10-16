using UnityEngine;
using UnityEngine.UI;

namespace unitwb {
/**
 */
public class DistortionMask
  : MonoBehaviour
{
  public float size = 64.0f;

  private Image image;

  /**
   */
  void Awake() {
    this.image = GetComponent<Image>();
    this.image.material = new Material(this.image.material);
  }

  /**
   */
  void Start() {
    UpdateMaterial();
  }

  /**
   */
  void Update() {
    UpdateMaterial();
  }

  /**
   */
  void UpdateMaterial() {
    var size = this.size;
    if(GetComponentInParent<Canvas>() is Canvas canvas) {
      var transform = canvas.GetComponent<RectTransform>();
      size *= Mathf.Lerp(transform.localScale.x, 
                         transform.localScale.y, 0.5f) * canvas.scaleFactor;
    }
    this.image.material.SetFloat("_Size", size);
  }
}
}
