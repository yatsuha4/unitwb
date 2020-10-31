using TMPro;
using UnityEngine;

namespace unitwb {
/**
   FontAssetをロケールに合わせて変更する
*/
public class FontAssetLocalizer
  : Localizer<FontAsset>
{
  private TextMeshProUGUI text;

  /**
   */
  void Awake() {
    this.text = GetComponent<TextMeshProUGUI>();
  }

  /**
   */
  protected override void OnChangeValue(FontAsset value, FontAsset oldValue) {
    this.text.font = value.font;
    this.text.fontMaterial = value.material;
  }
}
}
