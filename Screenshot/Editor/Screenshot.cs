using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

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
  public static void Capture(string postfix) {
    var assetDir = new DirectoryInfo(Application.dataPath);
    var dir = assetDir.Parent.CreateSubdirectory("Screenshots");
    var name = Application.productName + "-" + 
      SceneManager.GetActiveScene().name + "-" + 
      Screen.width + "x" + Screen.height + "-" + 
      DateTime.Now.ToString("yyyyMMdd_HHmmss") + 
      postfix + ".png";
    var path = Path.Combine(dir.FullName, name);
    ScreenCapture.CaptureScreenshot(path);
    Debug.Log($"Capture {path}");
  }
}
}
