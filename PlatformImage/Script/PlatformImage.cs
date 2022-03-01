using UnityEngine;
using UnityEngine.UI;

namespace towerb.platformimage
{
  /**
     <summary>プラットフォームごとに違う画像</summary>
  */
  public class PlatformImage
    : MonoBehaviour
  {
    [SerializeField]
    private Sprite android;

    [SerializeField]
    private Sprite ios;

    /**
     */
    void Awake()
    {
      GetComponent<Image>().sprite = 
#if PLATFORM_ANDROID
        this.android
#elif PLATFORM_IOS
        this.ios
#else
        null
#endif
        ;
    }
  }
}
