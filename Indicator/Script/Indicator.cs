using UnityEngine;

namespace towerb.indicator
{
  /**
     インジケーター
  */
  public class Indicator
    : SingletonBehaviour<Indicator>
  {
    /**
     */
    public void Show(bool show)
    {
      GetComponent<Animator>().SetBool("Show", show);
    }
  }
}
