using System;
using UnityEditor;
using UnityEngine;

namespace unitwb {
/**
   バージョン更新ヘルパー
*/
public class VersionHelper {
  public enum Code {
    Major, 
    Minor, 
    Build
  }

  /**
   */
  [MenuItem("Unitwb/Version/Major++")]
  public static void IncrementMajor() {
    var version = GetVersion();
    version[(int)Code.Major]++;
    version[(int)Code.Minor] = 0;
    SetVersion(version);
  }

  /**
   */
  [MenuItem("Unitwb/Version/Minor++")]
  public static void IncrementMinor() {
    var version = GetVersion();
    version[(int)Code.Minor]++;
    SetVersion(version);
  }

  /**
   */
  [MenuItem("Unitwb/Version/Build++")]
  public static void IncrementBuild() {
    var version = GetVersion();
    version[(int)Code.Build]++;
    SetVersion(version);
  }

  /**
   */
  private static int[] GetVersion() {
    return Array.ConvertAll(PlayerSettings.bundleVersion.Split('.'), int.Parse);
  }

  /**
   */
  private static void SetVersion(int[] version) {
    var text = String.Join(".", version);
    PlayerSettings.bundleVersion = text;
    var build = version[(int)Code.Build];
    PlayerSettings.Android.bundleVersionCode = build;
    PlayerSettings.iOS.buildNumber = build.ToString();
    Debug.Log($"version {text}");
  }
}
}
