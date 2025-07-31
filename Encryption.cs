using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.IO;

namespace CounselQuickPlatinum
{
    class Encryption
    {
        const string key = "鰿螝큩늣怷襹㨊㐡⟊㒈ૄ奆◢쑞ⶨ";

        internal static byte[] GetBytes(string str)
        {
            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(str);
            return bytes;
        }

        internal static string GetString(byte[] bytes)
        {
            string str = System.Text.Encoding.Unicode.GetString(bytes);
            return str;
        }

        internal static byte[] GenerateIV()
        {
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                aes.GenerateIV();
                return aes.IV; 
            }
        }

        internal static string Base64EncryptString(string plainText, byte[] IV)
        {
            return Convert.ToBase64String(EncryptString(plainText, IV));
        }


        internal static string Base64DecryptString(string base64Encrypted, byte[] IV)
        {
            byte[] bytes = Convert.FromBase64String(base64Encrypted);
            return DecryptString(bytes, IV);
        }


        internal static byte[] EncryptString(string plainText, byte[] IV)
        {
            // Check arguments. 
            byte[] empty = GetBytes("");

            if (plainText == null || plainText.Length <= 0)
                return empty;
            byte[] encrypted;
            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = GetBytes(key);
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


        internal static string DecryptString(byte[] cipherText, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                return "";
            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;
            // Create an AesCryptoServiceProvider object 
            // with the specified key and IV. 
            using (AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider())
            {
                aesAlg.Key = GetBytes(key);
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

        internal static void EncryptFile(string inputFile, string outputFile, byte[] IV)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] keybytes = GetBytes(key);
                    aes.IV = IV;
                    aes.Padding = PaddingMode.PKCS7;

                    using (FileStream fsCrypt = new FileStream(outputFile, FileMode.Create))
                    {
                        using (ICryptoTransform encryptor = aes.CreateEncryptor(keybytes, aes.IV))
                        {
                            using (CryptoStream cs = new CryptoStream(fsCrypt, encryptor, CryptoStreamMode.Write))
                            {
                                using (FileStream fsIn = new FileStream(inputFile, FileMode.Open))
                                {
                                    int data;
                                    while ((data = fsIn.ReadByte()) != -1)
                                    {
                                        cs.WriteByte((byte)data);
                                    }
                                }
                                cs.Close();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        internal static void DecryptFile(string inputFile, string outputFile, byte[] IV)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] keyBytes = GetBytes(key);
                    aes.Padding = PaddingMode.PKCS7;

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsOut.WriteByte((byte)data);
                                    }
                                    cs.Close();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //internal static void DecryptToMemory(string inputFile, byte[] IV)
        internal static byte[] DecryptToMemory(string inputFile, byte[] IV)
        {
            byte[] keyBytes = GetBytes(key);
            byte[] file = File.ReadAllBytes(inputFile);
            AesManaged aes = new AesManaged();

            using (MemoryStream ms = new MemoryStream())
            {
                using (CryptoStream cs = new CryptoStream(ms, aes.CreateDecryptor(keyBytes, IV), CryptoStreamMode.Read))
                {
                    cs.Write(file, 0, file.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }

        }        
    }

}
