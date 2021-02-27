using UnityEngine;
using UnityEngine.UI;

namespace unitwb.appversion {
  /**
     アプリバージョン
  */
  public class AppVersion
    : MonoBehaviour
  {
    void Awake() {
      GetComponent<Text>().text = "v" + Application.version;
    }
  }
}
