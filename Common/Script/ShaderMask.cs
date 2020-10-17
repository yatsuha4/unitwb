using UnityEngine;
using UnityEngine.UI;

namespace unitwb {
/**
   シェーダーを使ったマスク処理基底クラス
*/
public class ShaderMask
  : MonoBehaviour
{
  public Image image { private set; get; }

  /**
   */
  void Awake() {
    this.image = GetComponent<Image>();
    this.image.material = new Material(this.image.material);
  }
}
}
