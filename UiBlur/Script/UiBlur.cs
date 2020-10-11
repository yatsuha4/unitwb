using UnityEngine;
using UnityEngine.UI;

namespace unitwb {
/**
 */
public class UiBlur
  : MonoBehaviour
{
  public float blurSize = 2.0f;

  private Material material;
  private RectTransform canvasTransform;

  /**
   */
  void Awake() {
    this.material = GetComponent<Image>().material;
    this.canvasTransform = GetComponentInParent<Canvas>()?.GetComponent<RectTransform>();
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
    var size = this.blurSize;
    if(this.canvasTransform != null) {
      size *= Mathf.Lerp(canvasTransform.localScale.x, 
                         canvasTransform.localScale.y, 0.5f);
    }
    this.material.SetFloat("_Size", size);
  }
}
}
