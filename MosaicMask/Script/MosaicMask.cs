using UnityEngine;
using UnityEngine.UI;

namespace unitwb {
/**
 */
public class MosaicMask
  : ShaderMask
{
  public Vector2 size = new Vector2(4.0f, 4.0f);

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
  private void UpdateMaterial() {
    var size = GetComponentInParent<Canvas>().GetComponent<RectTransform>().rect.size;
    this.image.material.SetFloat("_SizeX", this.size.x / size.x);
    this.image.material.SetFloat("_SizeY", this.size.y / size.y);
  }
}
}
