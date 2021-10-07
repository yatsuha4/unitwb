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

    private static T instance;

    /**
     */
    protected virtual void Awake()
    {
      if(instance == null)
      {
        instance = this as T;
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
    protected virtual void OnDestroy()
    {
      if(this as T == instance)
      {
        instance = null;
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
