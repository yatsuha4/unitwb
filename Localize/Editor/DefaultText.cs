using System.Linq;
using TMPro;
using UnityEditor;
using UnityEditor.Localization;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

namespace unitwb {
/**
   ローカライズされたテキストを設定する
*/
public class DefaultText {
  /**
   */
  [MenuItem("Unitwb/Localize/DefaultText")]
  public static void SetDefaultText() {
    foreach(var obj in Resources.FindObjectsOfTypeAll<LocalizeStringEvent>()) {
      if(obj.GetComponent<Text>() is Text text) {
        text.text = GetText(obj.StringReference);
      }
      else if(obj.GetComponent<TextMeshProUGUI>() is TextMeshProUGUI textMesh) {
        textMesh.text = GetText(obj.StringReference);
      }
    }
  }

  /**
   */
  private static string GetText(LocalizedString src) {
    if(!src.IsEmpty) {
      var collection = 
        LocalizationEditorSettings.GetStringTableCollection(src.TableReference);
      if(collection != null) {
        var entry = collection.GetRowEnumerator().
          First(row => row.KeyEntry.Id == src.TableEntryReference.KeyId);
        for(int i = 0; i < entry.TableEntriesReference.Length; i++) {
          var locale = 
            LocalizationEditorSettings.GetLocale(entry.TableEntriesReference[i].Code);
          if(locale.SortOrder == 0) {
            return entry.TableEntries[i].LocalizedValue;
          }
        }
      }
    }
    return "";
  }
}
}
