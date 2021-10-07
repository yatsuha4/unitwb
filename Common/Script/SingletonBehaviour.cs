using UnityEngine;

namespace Towerb
{
  /**
     <summary>シングルトン</summary>
  */
  public class SingletonBehaviour<T>
    : MonoBehaviour
    where T : MonoBehaviour
  {
    private static T instance;

    /**
     */
    void Awake()
    {
      if(instance == null)
      {
        instance = this as T;
        this.transform.parent = null;
        DontDestroyOnLoad(this.gameObject);
      }
      else
      {
        Destroy(this.gameObject);
      }
    }

    /**
     */
    public static T Instance
    {
      get { return instance; }
    }
  }
}
