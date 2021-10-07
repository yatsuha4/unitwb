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
    [SerializeField]
    private bool dontDestroy = false;

    private static MonoBehaviour instance;

    /**
     */
    void Awake()
    {
      if(instance == null)
      {
        instance = this;
        if(this.dontDestroy)
        {
          this.transform.parent = null;
          DontDestroyOnLoad(this.gameObject);
        }
      }
      else
      {
        Debug.Assert(this.dontDestroy);
        Destroy(this.gameObject);
      }
    }

    /**
     */
    void OnDestroy()
    {
      if(instance == this)
      {
        instance = null;
      }
    }

    /**
     */
    public static T Instance
    {
      get { return instance as T; }
    }
  }
}
