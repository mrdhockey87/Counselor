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
            /*
            byte[] bytes = new byte[str.Length * sizeof(char)];
            System.Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
            */

            byte[] bytes = System.Text.Encoding.Unicode.GetBytes(str);
            return bytes;
 
            /*
            UTF8Encoding utf = new UTF8Encoding(false);
            //byte[] bytes = System.Text.Encoding.UTF8.GetBytes(str);
            byte[] bytes = utf.GetBytes(str);
            return bytes;
             * */
        }

        internal static string GetString(byte[] bytes)
        {
            /*
            char[] chars = new char[bytes.Length / sizeof(char)];
            System.Buffer.BlockCopy(bytes, 0, chars, 0, bytes.Length);
            return new string(chars);
             * */

            string str = System.Text.Encoding.Unicode.GetString(bytes);
            return str;

            /*
            UTF8Encoding utf = new UTF8Encoding(false);
            string result = utf.GetString(bytes);
            return result;
             * */
        }

        internal static byte[] GenerateIV()
        {
            //using (RijndaelManaged rijAlg = new RijndaelManaged())
            using (AesCryptoServiceProvider aes = new AesCryptoServiceProvider())
            {
                //rijAlg.GenerateIV();
                aes.GenerateIV();
                return aes.IV; 
            }
        }

        /*internal static string EncryptString(string plainText, byte[] IV)
        {

        }*/


        /*
        internal static byte[] EncryptString(string plainText, byte[] IV)
        {
            byte[] empty = GetBytes("");

            if (plainText == "")
                return empty;

            byte[] encrypted;
            //using (RijndaelManaged rijAlg = new RijndaelManaged())
            using (AesManaged aes = new AesManaged())
            {
                aes.GenerateKey();
                byte[] keybytes = GetBytes(key);
                aes.Key = keybytes;
                aes.IV = IV;
                aes.Padding = PaddingMode.PKCS7;
                //rijAlg.Key = GetBytes(key);
                //rijAlg.IV = IV;

                //ICryptoTransform encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                // Create the streams used for encryption. 
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(GetBytes(plainText), 0, plainText.Length);
                    }
                    encrypted = ms.ToArray();
                }

                
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                //}
            }

            //return new string[] { IV, GetString(encrypted) };
            //return GetString(encrypted);
            return encrypted;
        }*/


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
                //throw new ArgumentNullException("plainText");
            /*if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");*/
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

            /*
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
             * */

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


        /*
        internal static string DecryptString(string cipherText, byte[] IV)
        {
            // Check arguments. 
            if (cipherText == null || cipherText.Length <= 0)
                return cipherText;
                //throw new ArgumentNullException("cipherText");

            //string IV = cipherText.Substring(0, 16);
            //cipherText = cipherText.Substring(16);

            if (key == null || key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            // Declare the string used to hold 
            // the decrypted text. 
            string plaintext = null;

            // Create an RijndaelManaged object 
            // with the specified key and IV. 
            //using (RijndaelManaged rijAlg = new RijndaelManaged())
            using(AesManaged aes = new AesManaged())
            {
                //rijAlg.Key = GetBytes(key);
                //rijAlg.IV = IV;
                //rijAlg.Padding = PaddingMode.PKCS7;
                //rijAlg.IV = GetBytes(IV);

                aes.Key = GetBytes(key);
                aes.IV = IV;
                aes.Padding = PaddingMode.PKCS7;

                // Create a decrytor to perform the stream transform.
                //ICryptoTransform decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                // Create the streams used for decryption. 
                byte[] bytes = GetBytes(cipherText);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(bytes, 0, bytes.Length);
                    }
                    //plaintext = GetString(bytes);
                    plaintext = GetString(ms.ToArray());
                }

                /*
                using (MemoryStream msDecrypt = new MemoryStream(bytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        csDecrypt.Write(bytes, 0, bytes.Length);
                        
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            // Read the decrypted bytes from the decrypting stream 
                            // and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }

                    plaintext = GetString(msDecrypt.ToArray());
                }
            }

            return plaintext;
        }*/
        

        internal static void EncryptFile(string inputFile, string outputFile, byte[] IV)
        {
            try
            {
                //using (RijndaelManaged aes = new RijndaelManaged())
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
                                //cs.FlushFinalBlock();
                                cs.Close();
                            }
                        }
                    }
                }
                // write the IV to the beginning
                /*
                FileStream stream = new FileStream(outputFile, FileMode.Open);
                stream.Write(IV, 0, IV.Count());
                stream.Flush();
                stream.Close();
                 * */
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
                //using (RijndaelManaged aes = new RijndaelManaged())
                using (AesManaged aes = new AesManaged())
                {
                    //byte[] key = ASCIIEncoding.UTF8.GetBytes(key);
                    byte[] keyBytes = GetBytes(key);
                    aes.Padding = PaddingMode.PKCS7;

                    /* This is for demostrating purposes only.
                     * Ideally you will want the IV key to be different from your key and you should always generate a new one for each encryption in other to achieve maximum security*/
                    //byte[] IV = ASCIIEncoding.UTF8.GetBytes(key);

                    // get the IV
                    //byte[] IV = new byte[16];

                    //FileStream stream = new FileStream(inputFile, FileMode.Open);
                    //stream.Read(IV, 0, 16);
                    //stream.Close();


                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {
                        //fsCrypt.Seek(15, SeekOrigin.Begin);
                        using (FileStream fsOut = new FileStream(outputFile, FileMode.Create))
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    //int i = 0;
                                    //cs.Seek(16, SeekOrigin.Begin);
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        /*if (i < 16)
                                        {
                                            i++;
                                            continue;
                                        }*/

                                        fsOut.WriteByte((byte)data);
                                        //fsOut.Flush();
                                    }
                                    //cs.FlushFinalBlock();
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


        /*
        internal static void DecryptToMemory(string inputFile, byte[] IV)
        {
            try
            {
                using (AesManaged aes = new AesManaged())
                {
                    byte[] keyBytes = GetBytes(key);

                    using (FileStream fsCrypt = new FileStream(inputFile, FileMode.Open))
                    {

                        long filesize = fsCrypt.Length;

                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            using (ICryptoTransform decryptor = aes.CreateDecryptor(keyBytes, IV))
                            {
                                using (CryptoStream cs = new CryptoStream(fsCrypt, decryptor, CryptoStreamMode.Read))
                                {
                                    int data;
                                    while ((data = cs.ReadByte()) != -1)
                                    {
                                        fsCrypt.WriteByte((byte)data);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // failed to decrypt file
            }
        }*/
        
    }

}
