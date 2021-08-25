using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace unitwb.editor {
  /**
   */
  public class AutoSign {
    /**
     */
    [InitializeOnLoadMethod]
    private static void OnInitializeOnLoadMethod() {
      if(!string.IsNullOrEmpty(KeystoreName)) {
        if(string.IsNullOrEmpty(PlayerSettings.Android.keystorePass)) {
          PlayerSettings.Android.keystorePass = EditorPrefs.GetString(KeystoreName);
        }
        if(string.IsNullOrEmpty(PlayerSettings.Android.keyaliasPass)) {
          PlayerSettings.Android.keyaliasPass = EditorPrefs.GetString(KeyaliasName);
        }
      }
    }

    /**
     */
    [PostProcessBuild]
    private static void OnPostProcessBuild(BuildTarget target, string path) {
      if(target == BuildTarget.Android && !string.IsNullOrEmpty(KeystoreName)) {
        EditorPrefs.SetString(KeystoreName, PlayerSettings.Android.keystorePass);
        EditorPrefs.SetString(KeyaliasName, PlayerSettings.Android.keyaliasPass);
      }
    }

    /**
     */
    private static string KeystoreName {
      get {
        return PlayerSettings.Android.keystoreName;
      }
    }

    /**
     */
    private static string KeyaliasName {
      get {
        return $"{KeystoreName}/{PlayerSettings.Android.keyaliasName}";
      }
    }
  }
}
