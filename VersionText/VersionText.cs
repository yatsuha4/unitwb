using UnityEngine;
using UnityEngine.UI;

namespace towerb.versiontext
{
  /**
     <summary>アプリバージョン表示</summary>
  */
  public class VersionText
    : MonoBehaviour
  {
    [SerializeField]
    private string format = "v{0}";

    /**
     */
    void Awake()
    {
      GetComponent<Text>().text = string.Format(this.format, Application.version);
    }
  }
}
