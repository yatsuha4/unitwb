using UnityEditor;
using UnityEngine;

namespace unitwb {
/**
   設定
*/
[CreateAssetMenu(menuName="unitwb/ExtractLetter/Setting", fileName="ExtractLetterSetting")]
public class ExtractLetterSetting
  : ScriptableObject
{
  public ExtractLetterTable[] tables;

  /**
   */
  public void Extract() {
    foreach(var table in this.tables) {
      table.Extract();
    }
  }
}

/**
   インスペクタ拡張
*/
[CustomEditor(typeof(ExtractLetterSetting))]
public class ExtractLetterSettingEditor
  : Editor
{
  public override void OnInspectorGUI() {
    base.OnInspectorGUI();
    if(GUILayout.Button("Extract")) {
      ((ExtractLetterSetting)target).Extract();
    }
  }
}
}
