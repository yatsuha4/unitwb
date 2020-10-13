namespace unitwb {
/**
   簡易バージョンクラス
*/
[System.Serializable]
public class Version {
  public int major;
  public int minor;

  /**
     コンストラクタ
  */
  public Version(int major = 0, int minor = 0) {
    this.major = major;
    this.minor = minor;
  }
}
}
