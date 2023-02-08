using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Backend_Escaperoom_2.Application.Helpers
{
    public static class HashHelper
    {
        private static readonly string _key = "pktNrfjnWH8rNO6bPuAYS6To9ojznPbWHQ";
        private static readonly int _keySize = 256;
        private static readonly int _keyBlock = 128;

        /// <summary>
        /// Encriptar Cadena.
        /// </summary>
        /// <param name="encryptedText">String to be encrypted</param>
        /// <exception cref="FormatException"></exception>
        public static string Encrypt(string plainText)
        {
            if (plainText == null)
            {
                return null;
            }

            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(plainText);
            var cadenaEncrypted = Encoding.UTF8.GetBytes(_key);

            cadenaEncrypted = SHA512.Create().ComputeHash(cadenaEncrypted);

            var bytesEncrypted = Encrypt(bytesToBeEncrypted, cadenaEncrypted);

            return (Convert.ToBase64String(bytesEncrypted)).Replace("=","¬").Replace("/", "\\");
        }

        /// <summary>
        /// Desencriptar cadena.
        /// </summary>
        /// <param name="encryptedText">String to be decrypted</param>
        /// <exception cref="FormatException"></exception>
        public static string Decrypt(string encryptedText)
        {
            if (encryptedText == null)
            {
                return null;
            }

            encryptedText = encryptedText.Replace("¬", "=").Replace("\\", "/");

            var bytesToBeDecrypted = Convert.FromBase64String(encryptedText);
            var cadenaDencrypted = Encoding.UTF8.GetBytes(_key);

            cadenaDencrypted = SHA512.Create().ComputeHash(cadenaDencrypted);

            var bytesDecrypted = Decrypt(bytesToBeDecrypted, cadenaDencrypted);

            return Encoding.UTF8.GetString(bytesDecrypted);
        }


        //Metodos privados para encriptar
        private static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] cadenaEncripted)
        {
            byte[] encryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(cadenaEncripted, saltBytes, 1000);

                    AES.KeySize = _keySize;
                    AES.BlockSize = _keyBlock;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }

                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        private static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] cadenaDencripted)
        {
            byte[] decryptedBytes = null;
            var saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    var key = new Rfc2898DeriveBytes(cadenaDencripted, saltBytes, 1000);

                    AES.KeySize = _keySize;
                    AES.BlockSize = _keyBlock ;
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);
                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }

                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }
    }
}
