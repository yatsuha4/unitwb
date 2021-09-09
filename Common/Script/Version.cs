using System;

namespace unitwb {
  /**
     <summary>簡易バージョンクラス</summary>
  */
  [Serializable]
  public struct Version
    : IComparable<Version>
  {
    public int major;
    public int minor;

    /**
       <summary>コンストラクタ</summary>
    */
    public Version(int major = 0, int minor = 0)
    {
      this.major = major;
      this.minor = minor;
    }

    /**
       <summary>比較</summary>
     */
    public int CompareTo(Version rhs)
    {
      int diff = this.major - rhs.major;
      if(diff == 0)
      {
        diff = this.minor - rhs.minor;
      }
      return diff;
    }

    public static bool operator<(Version lhs, Version rhs)
    {
      return lhs.CompareTo(rhs) < 0;
    }

    public static bool operator<=(Version lhs, Version rhs)
    {
      return lhs.CompareTo(rhs) <= 0;
    }

    public static bool operator>(Version lhs, Version rhs)
    {
      return lhs.CompareTo(rhs) > 0;
    }

    public static bool operator>=(Version lhs, Version rhs)
    {
      return lhs.CompareTo(rhs) >= 0;
    }

    public static bool operator==(Version lhs, Version rhs)
    {
      return lhs.CompareTo(rhs) == 0;
    }

    public static bool operator!=(Version lhs, Version rhs)
    {
      return lhs.CompareTo(rhs) != 0;
    }

    override public string? ToString()
    {
      return $"{this.major}.{this.minor}";
    }
  }
}
