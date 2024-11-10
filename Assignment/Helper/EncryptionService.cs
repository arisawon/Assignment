namespace Assignment.Helper
{
    public class EncryptionService
    {
        private readonly string _encryptionKey;

        public EncryptionService(string encryptionKey)
        {
            _encryptionKey = encryptionKey;
        }

        public string EncryptData(string plainText)
        {
            return EncryptionHelper.Encrypt(plainText, _encryptionKey);
        }

        public string DecryptData(string cipherText)
        {
            return EncryptionHelper.Decrypt(cipherText, _encryptionKey);
        }
    }
}
