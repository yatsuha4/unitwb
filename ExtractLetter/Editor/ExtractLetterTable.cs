using System.IO;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.Localization.Tables;
using UnityEngine;

namespace unitwb {
/**
   変換テーブル
*/
[CreateAssetMenu(menuName="unitwb/ExtractLetter/Table", fileName="ExtractLetterTable")]
public class ExtractLetterTable
  : ScriptableObject
{
  public bool ascii = true;
  public StringTable[] stringTables;

  /**
   */
  public void Extract() {
    var letters = new List<char>();
    if(this.ascii) {
      for(char c = '\x21'; c < '\x7f'; c++) {
        letters.Add(c);
      }
    }
    foreach(var table in this.stringTables) {
      foreach(var value in table.Values) {
        foreach(var letter in value.Value) {
          if(letter >= ' ' && !letters.Contains(letter)) {
            letters.Add(letter);
          }
        }
      }
    }
    letters.Sort();
    var path = Path.ChangeExtension(AssetDatabase.GetAssetPath(this), ".txt");
    File.WriteAllText(path, new string(letters.ToArray()));
    Debug.Log($"write '{path}'");
    AssetDatabase.Refresh();
  }
}

/**
   インスペクタ拡張
*/
[CustomEditor(typeof(ExtractLetterTable))]
public class ExtractLetterTableEditor
  : Editor
{
  public override void OnInspectorGUI() {
    base.OnInspectorGUI();
    if(GUILayout.Button("Extract")) {
      ((ExtractLetterTable)target).Extract();
    }
  }
}
}
