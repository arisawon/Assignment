using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;


/// <summary>
/// This helper class to implement all encryption logic 
/// to make any sensitive field encrypt or decrypt 
/// using AES and Base64.
/// </summary>
public static class EncryptionHelper
{
    /// <summary>
    /// To encrypt a sensitive string with a key
    /// </summary>
    /// <param name="plainText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Encrypt(string plainText, string key)
    {
        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(key);

            // Use a fixed IV for deterministic encryption
            aes.IV = new byte[16]; // Fixed IV: all zeros (16 bytes for AES)

            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(plainText);
                sw.Close();
                return Convert.ToBase64String(ms.ToArray());
            }
        }
    }


    /// <summary>
    /// To decrypt a encrypted string with a key
    /// </summary>
    /// <param name="cipherText"></param>
    /// <param name="key"></param>
    /// <returns></returns>
    public static string Decrypt(string cipherText, string key)
    {
        var fullCipher = Convert.FromBase64String(cipherText);

        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String(key);
            aes.IV = new byte[16]; // Fixed IV: all zeros, matching encryption IV

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            using (var ms = new MemoryStream(fullCipher))
            using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
            using (var sr = new StreamReader(cs))
            {
                return sr.ReadToEnd();
            }
        }
    }
}
