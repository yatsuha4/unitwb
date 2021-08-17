using System.Collections;
using System.IO;
using System;
using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace unitwb.editor {
/**
 */
public class Screenshot {
  /**
   */
  [MenuItem("Unitwb/Screenshot")]
  public static void Capture() {
    Capture("");
  }

  /**
   */
  public static string Capture(string postfix) {
    var assetDir = new DirectoryInfo(Application.dataPath);
    var dir = assetDir.Parent.CreateSubdirectory("Screenshots");
    var name = Application.productName + "-" + 
      DateTime.Now.ToString("yyyyMMdd_HHmmss") + "-" + 
      SceneManager.GetActiveScene().name + "-" + 
      Screen.width + "x" + Screen.height +
      postfix + ".png";
    var path = Path.Combine(dir.FullName, name);
    ScreenCapture.CaptureScreenshot(path);
    Debug.Log($"Capture {path}");
    return path;
  }

  /**
   */
  public static IEnumerator DoCapture(string postfix) {
    var path = Capture(postfix);
    while(!File.Exists(path)) {
      yield return null;
    }
  }
}
}
