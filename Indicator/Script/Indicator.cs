using System.Collections;
using UnityEngine;

namespace unitwb.indicator {
  /**
     インジケーター
  */
  public class Indicator
    : MonoBehaviour
  {
    public GameObject Image;
    public float Time = 0.1f;
    public int Step = 9;

    /**
     */
    IEnumerator Start() {
      for(;;) {
        yield return new WaitForSeconds(this.Time);
        this.Image.transform.eulerAngles -= new Vector3(0.0f, 0.0f, 360.0f / this.Step);
      }
    }
  }
}
