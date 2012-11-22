using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Emprestae
{
    public class SecurityHelper
    {
        private static string key = "m0b1l3@@";
        private AesManaged rijAlg;

        public byte[] IV { get; private set; }
        public byte[] KEY { get; private set; }
        
        public SecurityHelper()
        {
            rijAlg = new AesManaged();
            rijAlg.GenerateIV();
            IV = rijAlg.IV;
            KEY = Convert.FromBase64String(EncodeTo64(key));
            rijAlg.Key = KEY;

            //try
            //{

            //    string original = "Here is some data to encrypt!";

            //    // Create a new instance of the AesManaged 
            //    // class.  This generates a new key and initialization  
            //    // vector (IV). 
            //    using (AesManaged myAes = new AesManaged())
            //    {
            //        myAes.Key = Convert.FromBase64String(EncodeTo64(key));
            //        // Encrypt the string to an array of bytes. 
            //        byte[] encrypted = EncryptStringToBytes_Aes(original, myAes.Key, myAes.IV);

            //        // Decrypt the bytes to a string. 
            //        string roundtrip = DecryptStringFromBytes_Aes(encrypted, myAes.Key, myAes.IV);

            //        //Display the original data and the decrypted data.
            //        Debug.WriteLine("Original:   {0}", original);
            //        Debug.WriteLine("Round Trip: {0}", roundtrip);
            //    }

            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine("Error: {0}", e.Message);
            //}
        }

        //public byte[] Encrypt(String plainText)
        //{
        //    byte[] encrypted;

        //    using (rijAlg)
        //    {
        //        //rijAlg.Key = KEY;
        //        //rijAlg.GenerateIV();
        //        //iv = rijAlg.IV;

        //        // Create a decrytor to perform the stream transform.
        //        ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

        //        // Create the streams used for encryption. 
        //        using (MemoryStream msEncrypt = new MemoryStream())
        //        {
        //            using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
        //            {
        //                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
        //                {
        //                    //Write all data to the stream.
        //                    swEncrypt.Write(plainText);
        //                }
        //                encrypted = msEncrypt.ToArray();
        //            }
        //        }

        //    }
        //    return encrypted;
        //}   

        //public String Decrypt(byte[] cipherText)
        //{
        //    string plaintext = null;
        //    using (rijAlg)
        //    {
        //        //rijAlg.GenerateIV();
        //        //iv = rijAlg.IV;

        //        // Create a decrytor to perform the stream transform.
        //        ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

        //        // Create the streams used for decryption. 
        //        using (MemoryStream msDecrypt = new MemoryStream(cipherText))
        //        {
        //            using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
        //            {
        //                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
        //                {
        //                    // Read the decrypted bytes from the decrypting stream 
        //                    // and place them in a string.
        //                    plaintext = srDecrypt.ReadToEnd();
        //                }
        //            }
        //        }
        //    }
        //    return plaintext;
        //}

        public byte[] Encrypt(String plainText)
        {
            return EncryptStringToBytes_Aes(plainText, rijAlg.Key, rijAlg.IV);
        }

        public String Decrypt(byte[] cipherText)
        {
            return DecryptStringFromBytes_Aes(cipherText, rijAlg.Key, rijAlg.IV);
        }


        public string EncodeTo64(string toEncode)
        {
            byte[] arrayToEncode = System.Text.Encoding.Unicode.GetBytes(toEncode);
            return System.Convert.ToBase64String(arrayToEncode);
        }

        //public string DecodeFrom64(string toDecode)
        //{
        //    byte[] arrayToDecode = System.Convert.FromBase64String(toDecode);
        //    return System.Text.Encoding.Unicode.GetString(arrayToDecode);
        //}

        static byte[] EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");
            byte[] encrypted;
            // Create an AesManaged object 
            // with the specified key and IV. 
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption. 
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }


            // Return the encrypted bytes from the memory stream. 
            return encrypted;

        }

        static string DecryptStringFromBytes_Aes(byte[] cipherText, byte[] Key, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an AesManaged object 
            // with the specified key and IV. 
            using (AesManaged aesAlg = new AesManaged())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption. 
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;

        }
    


    //    static string DecryptStringFromBytes(byte[] cipherText, byte[] Key, byte[] IV)
    //    {
    //        // Check arguments. 
    //        if (cipherText == null || cipherText.Length <= 0)
    //            throw new ArgumentNullException("cipherText");
    //        if (Key == null || Key.Length <= 0)
    //            throw new ArgumentNullException("Key");
    //        if (IV == null || IV.Length <= 0)
    //            throw new ArgumentNullException("IV");

    //        // Declare the string used to hold 
    //        // the decrypted text. 
    //        string plaintext = null;

    //        // Create an Rijndael object 
    //        // with the specified key and IV. 
    //        using (Aes rijAlg = new AesManaged())
    //        {
    //            rijAlg.Key = Key;
    //            rijAlg.IV = IV;

    //            // Create a decrytor to perform the stream transform.
    //            ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);

    //            // Create the streams used for decryption. 
    //            using (MemoryStream msDecrypt = new MemoryStream(cipherText))
    //            {
    //                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
    //                {
    //                    using (StreamReader srDecrypt = new StreamReader(csDecrypt))
    //                    {

    //                        // Read the decrypted bytes from the decrypting stream 
    //                        // and place them in a string.
    //                        plaintext = srDecrypt.ReadToEnd();
    //                    }
    //                }
    //            }

    //        }

    //        return plaintext;

    //    }


    //    static byte[] EncryptStringToBytes(string plainText, byte[] Key, byte[] IV)
    //    {
    //        // Check arguments. 
    //        if (plainText == null || plainText.Length <= 0)
    //            throw new ArgumentNullException("plainText");
    //        if (Key == null || Key.Length <= 0)
    //            throw new ArgumentNullException("Key");
    //        if (IV == null || IV.Length <= 0)
    //            throw new ArgumentNullException("IV");
    //        byte[] encrypted;
    //        // Create an Rijndael object 
    //        // with the specified key and IV. 
    //        using (Aes rijAlg = new AesManaged())
    //        {
    //            rijAlg.Key = Key;
    //            rijAlg.IV = IV;

    //            // Create a decrytor to perform the stream transform.
    //            ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);

    //            // Create the streams used for encryption. 
    //            using (MemoryStream msEncrypt = new MemoryStream())
    //            {
    //                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
    //                {
    //                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
    //                    {

    //                        //Write all data to the stream.
    //                        swEncrypt.Write(plainText);
    //                    }
    //                    encrypted = msEncrypt.ToArray();
    //                }
    //            }
    //        }


    //        // Return the encrypted bytes from the memory stream. 
    //        return encrypted;

    //    }
    }
}
