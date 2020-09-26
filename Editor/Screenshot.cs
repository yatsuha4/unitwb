using System.IO;
using UnityEditor;
using UnityEngine;

namespace unitwb {
/**
 */
public class Screenshot {
  /**
   */
  [MenuItem("Unitwb/Screenshot")]
  public static void Capture() {
    var assetDir = new DirectoryInfo(Application.dataPath);
    var dir = assetDir.Parent.CreateSubdirectory("Screenshots");
    var name = System.DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".png";
    var path = Path.Combine(dir.FullName, name);
    ScreenCapture.CaptureScreenshot(path);
  }
}
}
