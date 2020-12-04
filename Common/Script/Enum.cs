using System.Linq;

namespace unitwb {
/**
 */
public static class Enum<T> {
  /**
   */
  public static T[] Values {
    get {
      return System.Enum.GetValues(typeof(T)).Cast<T>().ToArray();
    }
  }

  /**
   */
  public static int Length {
    get {
      return System.Enum.GetValues(typeof(T)).Length;
    }
  }
}
}
