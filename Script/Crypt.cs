using System.Security.Cryptography;

namespace unitwb {
/**
   暗号化
*/
public class Crypt {
  private SymmetricAlgorithm algorithm;

  /**
     コンストラクタ
  */
  public Crypt(string password, string salt) {
    this.algorithm = CreateRijndaelManaged(password, salt);
  }

  /**
     暗号化する
     @param[in] input 入力
     @return 暗号化されたバイト列
  */
  public byte[] Encrypt(byte[] input) {
    using(var encryptor = algorithm.CreateEncryptor()) {
      return encryptor.TransformFinalBlock(input, 0, input.Length);
    }
  }

  /**
     復号化する
     @param[in] input 入力
     @return 復号化されたバイト列
  */
  public byte[] Decrypt(byte[] input) {
    using(var decryptor = algorithm.CreateDecryptor()) {
      return decryptor.TransformFinalBlock(input, 0, input.Length);
    }
  }

  /**
   */
  private static RijndaelManaged CreateRijndaelManaged(string password, string salt) {
    var rijndael = new RijndaelManaged();
    var deriveBytes = 
      new Rfc2898DeriveBytes(password, System.Text.Encoding.ASCII.GetBytes(salt));
    rijndael.Key = deriveBytes.GetBytes(rijndael.KeySize / 8);
    rijndael.IV = deriveBytes.GetBytes(rijndael.BlockSize / 8);
    return rijndael;
  }

  /**
     シード値からパスワードを生成する
     @param[in] seed シード値
     @param[in] length 長さ
  */
  public static string CreatePassword(int seed, int length) {
    var random = new System.Random(seed);
    var bytes = new byte[length];
    random.NextBytes(bytes);
    return System.Convert.ToBase64String(bytes);
  }
}
}
