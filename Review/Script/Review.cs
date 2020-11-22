using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace unitwb {
/**
 */
public class Review
  : MonoBehaviour
{
  public UnityEvent onShow;

  /**
     レビューを促す
  */
  public void Show() {
#if !UNITWB_REVIEW
    Debug.LogError("Must be define UNITWB_REVIEW");
#elif UNITY_EDITOR
    Debug.Log("Service.RequestReview()");
    this.onShow.Invoke();
#elif UNITY_IOS
    UnityEngine.iOS.Device.RequestStoreReview();
    this.onShow.Invoke();
#elif UNITY_ANDROID
    StartCoroutine(DoShow());
#endif
  }

#if UNITY_ANDROID && UNITWB_REVIEW
  private IEnumerator DoShow() {
    var manager = new ReviewManager();
    var request = manager.RequestReviewFlow();
    yield return request;
    if(request.Error != ReviewErrorCode.NoError) {
      yield break;
    }
    var result = request.GetResult();
    var launch = manager.LaunchReviewFlow(result);
    yield return launch;
    if(launch.Error != ReviewErrorCode.NoError) {
      yield break;
    }
    this.onShow.Invoke();
  }
#endif
}
}
