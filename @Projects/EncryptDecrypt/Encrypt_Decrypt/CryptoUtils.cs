using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

// Encryption algorithm reference materials source : https://web.ntnu.edu.tw/~algo/Encryption.html
// Encryption Algorithm Reference Materials source : https://hitcon.org/2018/CMT/slide-files/d1_s2_r4.pdf

namespace Encrypt_Decrypt
{
    internal class CryptoUtils
    {


        
        internal class Simple {

            // Caesar , Excess-3 Single character transformation
            public static string Encrypt_Caesar3(string Plaintext) {
                string encrypt_string = "";
                foreach (char c in Plaintext){
                    if (char.IsLetter(c)){
                        char offset = char.IsUpper(c) ? 'A' : 'a';
                        char encryptedChar = (char)(((c - offset + 3) % 26) + offset);
                        encrypt_string += encryptedChar;
                    }
                    else if (char.IsDigit(c)) {
                        char encryptedChar =  (char)( ( ((c - '0') + 3) % 10) + '0');
                        encrypt_string += encryptedChar;
                    }
                    else encrypt_string += c;
                }
                return encrypt_string;
            }


            // Caesar , Excess-3
            public static string Decrypt_Caesar3(string Plaintext) {
                string decrypt_string = "";
                foreach (char c in Plaintext) {
                    if (char.IsLetter(c)){
                        char offset = char.IsUpper(c) ? 'A' : 'a';
                        char decryptedChar = (char)(((c - offset - 3 + 26) % 26) + offset);
                        decrypt_string += decryptedChar;
                    }else if (char.IsDigit(c)){
                        char decryptedChar = (char)((((c - '0') - 3 + 10) % 10) + '0');
                        decrypt_string += decryptedChar;
                    }
                    else decrypt_string += c;
                }
                return decrypt_string;
            }


            // Vigenere : 
            //      Block encryption: Divide the input text into fixed-length blocks, and encrypt the offset of each block using a key.
            //      The secret string is used as the key: this string (e.g., "KEY") is used to control the offset of each block.
            //      The input text is segmented into blocks, and the key is used as a sliding window for encryption, step KEY.Length
            //      Each block uses the corresponding letter of the Key as an offset and is encrypted using a Caesar cipher.
            //      # It will be attacked by Kasiski Examination
            //
            public static string Vigenere_key = "KEY";
            public static int Vigenere_blockSize = Vigenere_key.Length;

            private static int GetOffset(char keyChar) {
                if (char.IsLetter(keyChar)) return char.ToUpper(keyChar) - 'A';
                return 0;
            }
            // Encryption x -> Engima, changed to Vigenere
            public static string Encrypt_Vigenere(string plaintext)
            {
                var result = new StringBuilder();
                int keyLen = Vigenere_key.Length;

                for (int i = 0; i < plaintext.Length; i++)
                {
                    char c = plaintext[i];
                    int keyIndex = i % keyLen; 
                    int offset = GetOffset(Vigenere_key[keyIndex]);

                    if (char.IsLetter(c))
                    {
                        char baseChar = char.IsUpper(c) ? 'A' : 'a';
                        char encryptedCh = (char)(((c - baseChar + offset) % 26) + baseChar);
                        result.Append(encryptedCh);
                    }
                    else if (char.IsDigit(c))
                    {
                        char encryptedCh = (char)((((c - '0') + offset) % 10) + '0');
                        result.Append(encryptedCh);
                    }
                    else
                        result.Append(c);
                    
                }
                return result.ToString();
            }

            public static string Decrypt_Vigenere(string ciphertext)
            {
                var result = new StringBuilder();
                int keyLen = Vigenere_key.Length;

                for (int i = 0; i < ciphertext.Length; i++)
                {
                    char c = ciphertext[i];
                    int keyIndex = i % keyLen;
                    int offset = GetOffset(Vigenere_key[keyIndex]);

                    if (char.IsLetter(c))
                    {
                        char baseChar = char.IsUpper(c) ? 'A' : 'a';
                        char decryptedCh = (char)(((c - baseChar - offset + 26) % 26) + baseChar);
                        result.Append(decryptedCh);
                    }
                    else if (char.IsDigit(c))
                    {
                        char decryptedCh = (char)((((c - '0') - offset + 10) % 10) + '0');
                        result.Append(decryptedCh);
                    }
                    else
                        result.Append(c);
                    
                }
                return result.ToString();
            }


            // Introducing the concept of bits: using bits as the basic unit.
             // The meaning of XOR: whether a bit needs to be changed.
             // A binary integer is agreed upon beforehand as a secret.
            public static char XOR_Key = 'K';
            public static string Encrypt_XOR(string plaintext) {
                string ciphertext = "";
                foreach (char c in plaintext)
                    ciphertext += (char)(c ^ XOR_Key);
                return ciphertext;
            }

            //The 64-bit base version handles uppercase letters better.
            public static string Encrypt_XOR_Base64(string plaintext) {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plaintext);
                byte[] encryptedBytes = new byte[plainBytes.Length];
                for (int i = 0; i < plainBytes.Length; i++)
                    encryptedBytes[i] = (byte)(plainBytes[i] ^ (byte)XOR_Key);
                return Convert.ToBase64String(encryptedBytes);  
            }
            public static string Decrypt_XOR_Base64(string base64Cipher) {
                byte[] cipherBytes = Convert.FromBase64String(base64Cipher);
                byte[] decryptedBytes = new byte[cipherBytes.Length];
                for (int i = 0; i < cipherBytes.Length; i++)
                    decryptedBytes[i] = (byte)(cipherBytes[i] ^ (byte)XOR_Key);
                return Encoding.UTF8.GetString(decryptedBytes);
            }






            // The pre-agreed secret is the same length as the plaintext, 
            // introducing the concept of multiple rounds to make the ciphertext "appear very messy".
            // Using Engima as F
            // Plaintext => split into left half L and right half R
            // for N rounds:
            // L_new = R_old
            // R_new = L_old XOR F(R_old, round_key)
            // Final ciphertext = R_N + L_N
            // Pad the string to an even length
            private static string PadToEven(string input) {
                return input.Length % 2 == 0 ? input : input + "\0";
            }
            // Disassembled into left and right halves
            private static void Split(string input, out string left, out string right) {
                int half = input.Length / 2;
                left = input.Substring(0, half);
                right = input.Substring(half);
            }

            // Enigma encryption functions as F
            private static string F(string input){
                string enigmaEncrypted = Encrypt_Vigenere(input);
                return Encrypt_XOR(enigmaEncrypted);
            }

            // String XOR operation: One-to-one XOR of two strings
            private static string XOR(string a, string b) { 
                int len = Math.Min(a.Length, b.Length);
                char[] result = new char[len];
                for (int i = 0; i < len; i++)
                    result[i] = (char)(a[i] ^ b[i]);
                return new string(result);
            }

            public static string Encrypt_Feistel(string plaintext)
            {
                string padded = PadToEven(plaintext);
                string L, R;
                Split(padded, out L, out R);

                for (int round = 0; round < 4; round++){
                    string newL = R;
                    string fR = F(R);
                    string newR = XOR(L, fR);
                    L = newL;
                    R = newR;
                }

                return L + R;
            }

            public static string Decrypt_Feistel(string ciphertext) { 
                string padded = PadToEven(ciphertext);
                string L, R;
                Split(padded, out L, out R);

                for (int round = 0; round < 4; round++){
                    string newR = L;
                    string fL = F(L);
                    string newL = XOR(R, fL);
                    L = newL;
                    R = newR;
                }

                string combined = L + R;
                return combined.TrimEnd('\0'); 
            }

            // Play Fair Method
            /*
                1. Construct a 5x5 letter matrix:
                Use the encryption key to fill in the letters (merging 'I' and 'J' into the same cell) to avoid repetition.
                Then fill in the remaining unseen letters.

                2. Decompose the plaintext into pairs of letters (digraphs):
                - If a pair of letters is the same, insert an 'X' in the middle.
                - If the plaintext string is of odd length, add an 'X' at the end.

                3. Encryption rules:
                - If in the same column: replace each letter with the letter to its right (cyclic replacement).
                - If in the same row: replace each letter with the letter below it (cyclic replacement).
                - Otherwise: swap the columns of the two letters, but keep the rows unchanged (forming a diagonal swap).

                4. Decryption rules are the opposite of encryption:
                - If in the same column: move one cell to the left for each letter.
                - If in the same row: move one cell up for each letter.
                - Otherwise: also use diagonal swap.

                5. The final decryption result can be customized by removing the added 'X' as needed.

                Example: Plaintext: HELLO
                Decrypted: HE LX LO
                Encrypted: XM CN NM
            */

            private static string Playfair_key = "playfair";
            public static string Encrypt_Playfair(string input)
            {
                var matrix = BuildMatrix(Playfair_key);
                var digraphs = ToDigraphs(input);
                var sb = new StringBuilder();

                foreach (var (a, b) in digraphs)
                {
                    (int r1, int c1) = Find(matrix, a);
                    (int r2, int c2) = Find(matrix, b);

                    if (r1 == r2)
                    {
                        sb.Append(matrix[r1][(c1 + 1) % 5]);
                        sb.Append(matrix[r2][(c2 + 1) % 5]);
                    }
                    else if (c1 == c2)
                    {
                        sb.Append(matrix[(r1 + 1) % 5][c1]);
                        sb.Append(matrix[(r2 + 1) % 5][c2]);
                    }
                    else
                    {
                        sb.Append(matrix[r1][c2]);
                        sb.Append(matrix[r2][c1]);
                    }
                }

                return sb.ToString();
            }

            public static string Decrypt_Playfair(string input)
            {
                var matrix = BuildMatrix(Playfair_key);
                var digraphs = ToDigraphs(input);
                var sb = new StringBuilder();

                foreach (var (a, b) in digraphs)
                {
                    (int r1, int c1) = Find(matrix, a);
                    (int r2, int c2) = Find(matrix, b);

                    if (r1 == r2)
                    {
                        sb.Append(matrix[r1][(c1 + 4) % 5]);
                        sb.Append(matrix[r2][(c2 + 4) % 5]);
                    }
                    else if (c1 == c2)
                    {
                        sb.Append(matrix[(r1 + 4) % 5][c1]);
                        sb.Append(matrix[(r2 + 4) % 5][c2]);
                    }
                    else
                    {
                        sb.Append(matrix[r1][c2]);
                        sb.Append(matrix[r2][c1]);
                    }
                }
                var result = sb.ToString();
                if (result.Length % 2 == 0 && result[result.Length-1] == 'X')
                {
                    return result.Substring(0, result.Length - 1);
                }
                // Remove the X inserted during encryption
                return RemoveInsertedX(sb.ToString());
            }

            // PlayFair tool

            private static List<(char, char)> ToDigraphs(string input)
            {
                var cleaned = input.ToUpper().Replace("J", "I").Where(char.IsLetter).ToList();
                var result = new List<(char, char)>();

                for (int i = 0; i < cleaned.Count; i++)
                {
                    char first = cleaned[i];
                    char second = (i + 1 < cleaned.Count) ? cleaned[i + 1] : 'X';

                    if (first == second)
                    {
                        result.Add((first, 'X'));
                    }
                    else
                    {
                        result.Add((first, second));
                        i++;
                    }
                }

                if (result.Count > 0 && result.Last().Item2 == '\0')
                {
                    result[result.Count - 1] = (result.Last().Item1, 'X');
                }

                return result;
            }

            private static string RemoveInsertedX(string text)
            {
                var result = new StringBuilder();
                for (int i = 0; i < text.Length; i += 2)
                {
                    char a = text[i];
                    char b = (i + 1 < text.Length) ? text[i + 1] : 'X';

                    // If a == b and b is 'X', it means it might be used for filling.
                    if (a == b && b == 'X') continue;

                    // If a + b is 'AX' and the next is 'A', it could also be an inserted X, which is ignored.
                    if (b == 'X' && i + 2 < text.Length && text[i] == text[i + 2])
                    {
                        result.Append(a);
                        continue;
                    }

                    result.Append(a);
                    if (i + 1 < text.Length) result.Append(b);
                }
                return result.ToString();
            }

            private static char[][] BuildMatrix(string key)
            {
                var used = new HashSet<char>();
                var table = new List<char>();

                foreach (char c in key.ToUpper().Replace("J", "I"))
                {
                    if (char.IsLetter(c) && used.Add(c)) table.Add(c);
                }

                for (char c = 'A'; c <= 'Z'; c++)
                {
                    if (c != 'J' && used.Add(c)) table.Add(c);
                }

                var matrix = new char[5][];
                for (int i = 0; i < 5; i++)
                    matrix[i] = table.Skip(i * 5).Take(5).ToArray();

                return matrix;
            }

            private static (int, int) Find(char[][] matrix, char c)
            {
                for (int i = 0; i < 5; i++)
                    for (int j = 0; j < 5; j++)
                        if (matrix[i][j] == c)
                            return (i, j);
                throw new Exception($"Char '{c}' not found in matrix.");
            }



        };


        // AES, DES, 3DES, RC2
        internal class Symmetric {

            private static byte[] GetBytes(string str) => Encoding.UTF8.GetBytes(str);
            // data encryption standard （ DES ）
            // Introduce the concept of key scheduling: make the keys "appear messy" for each round.
            //A fixed 8-character key and an IV (which should be kept confidential and generated securely).
            private static readonly string DES_keyString = "MySecrK8"; // 8 字元
            private static readonly string DES_ivString = "InitVec8";  // 8 字元

            public static string Encrypt_DES(string plainText) {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = GetBytes(DES_keyString);
                    des.IV = GetBytes(DES_ivString);

                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    ICryptoTransform encryptor = des.CreateEncryptor();
                    byte[] encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                    return Convert.ToBase64String(encryptedBytes);
                }
            }

            public static string Decrypt_DES(string base64CipherText)
            {
                using (DESCryptoServiceProvider des = new DESCryptoServiceProvider())
                {
                    des.Key = GetBytes(DES_keyString);
                    des.IV = GetBytes(DES_ivString);

                    byte[] cipherBytes = Convert.FromBase64String(base64CipherText);
                    ICryptoTransform decryptor = des.CreateDecryptor();
                    byte[] decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

                    return Encoding.UTF8.GetString(decryptedBytes);
                }
            }


            /* 3DES : 
                * Calculate a 128-bit hash using MD5 → keys
                * Use these keys as the TripleDES key
                * Encrypt using TripleDES + ECB mode + PKCS7 padding
                * → Return a Base64 string
            */
            public static string hash_3DES = "f0xle@rn";
            public static string Encrypt_TripleDES(string input_string){

                byte[] data = UTF8Encoding.UTF8.GetBytes(input_string);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash_3DES));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 }){
                        ICryptoTransform transform = tripDes.CreateEncryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return Convert.ToBase64String(results);
                    }
                }
 
            }

            public static string Decrypt_TripleDES(string input_string){

                byte[] data = Convert.FromBase64String(input_string);
                using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
                {
                    byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash_3DES));
                    using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { Key = keys, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 }){
                        ICryptoTransform transform = tripDes.CreateDecryptor();
                        byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                        return UTF8Encoding.UTF8.GetString(results);
                    }
                }

            }


            private static readonly string AES_key = "abcdefghijklmnop"; // 128 bits
            private static readonly string AES_iv = "abcdefghijklmnop";  // 128 bits

            public static string Encrypt_AES(string plainText){
                using (Aes aes = Aes.Create()){
                    aes.Key = Encoding.UTF8.GetBytes(AES_key);
                    aes.IV = Encoding.UTF8.GetBytes(AES_iv);
                    var ms = new MemoryStream();
                    var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write);
                    using (var sw = new StreamWriter(cs)) sw.Write(plainText);
                    return Convert.ToBase64String(ms.ToArray());
                }
            }

            public static string Decrypt_AES(string cipherText){
                using (Aes aes = Aes.Create()){
                    aes.Key = Encoding.UTF8.GetBytes(AES_key);
                    aes.IV = Encoding.UTF8.GetBytes(AES_iv);
                    var ms = new MemoryStream(Convert.FromBase64String(cipherText));
                    var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Read);
                    var sr = new StreamReader(cs);
                    return sr.ReadToEnd();
                }
            }


            private static readonly string RC2_key = "12345678"; // RC2 Key 8 bit at least
            private static readonly string RC2_iv = "87654321";  // IV 8 bit

            public static string Encrypt_RC2(string plainText)
            {
                using (var rc2 = RC2.Create())
                {
                    rc2.Key = Encoding.UTF8.GetBytes(RC2_key);
                    rc2.IV = Encoding.UTF8.GetBytes(RC2_iv);

                    byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                    using (var ms = new MemoryStream())
                    using (var cs = new CryptoStream(ms, rc2.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(plainBytes, 0, plainBytes.Length);
                        cs.FlushFinalBlock();
                        return Convert.ToBase64String(ms.ToArray());
                    }
                }
            }

            public static string Decrypt_RC2(string base64Cipher)
            {
                using (var rc2 = RC2.Create())
                {
                    rc2.Key = Encoding.UTF8.GetBytes(RC2_key);
                    rc2.IV = Encoding.UTF8.GetBytes(RC2_iv);

                    byte[] cipherBytes = Convert.FromBase64String(base64Cipher);
                    using (var ms = new MemoryStream(cipherBytes))
                    using (var cs = new CryptoStream(ms, rc2.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var sr = new StreamReader(cs, Encoding.UTF8))
                    {
                        return sr.ReadToEnd();
                    }
                }
            }


            // CBC 3DES
            public static string Encrypt_TripleDES_CBC(string plainText)
            {
                byte[] data = Encoding.UTF8.GetBytes(plainText);
                var md5 = MD5.Create();
                byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash_3DES)); // 16 bytes key

                var tripleDES = new TripleDESCryptoServiceProvider
                {
                    Key = key,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };

                tripleDES.GenerateIV();  
                byte[] iv = tripleDES.IV;

                var encryptor = tripleDES.CreateEncryptor();
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write);
                cs.Write(data, 0, data.Length);
                cs.FlushFinalBlock();

                byte[] encrypted = ms.ToArray();

                // 將 IV + EncryptedData 一起封裝成輸出
                byte[] result = new byte[iv.Length + encrypted.Length];
                Buffer.BlockCopy(iv, 0, result, 0, iv.Length);
                Buffer.BlockCopy(encrypted, 0, result, iv.Length, encrypted.Length);

                return Convert.ToBase64String(result);
            }

            public static string Decrypt_TripleDES_CBC(string base64CipherText)
            {
                byte[] combined = Convert.FromBase64String(base64CipherText);
                var md5 = MD5.Create();
                byte[] key = md5.ComputeHash(Encoding.UTF8.GetBytes(hash_3DES)); // 16 bytes key

                var tripleDES = new TripleDESCryptoServiceProvider
                {
                    Key = key,
                    Mode = CipherMode.CBC,
                    Padding = PaddingMode.PKCS7
                };

                byte[] iv = new byte[8]; // TripleDES IV 固定為 8 bytes
                byte[] encrypted = new byte[combined.Length - iv.Length];

                Buffer.BlockCopy(combined, 0, iv, 0, iv.Length);
                Buffer.BlockCopy(combined, iv.Length, encrypted, 0, encrypted.Length);

                tripleDES.IV = iv;

                var decryptor = tripleDES.CreateDecryptor();
                var ms = new MemoryStream(encrypted);
                var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
                var sr = new StreamReader(cs, Encoding.UTF8);

                return sr.ReadToEnd();
            }
        };


        internal class Asymmetric {

            // RSA
            private static RSA rsa = RSA.Create(2048);

            // Pre-generated public and private keys
            public static string PublicKey => rsa.ToXmlString(false);
            public static string PrivateKey => rsa.ToXmlString(true);

            // Single parameter encryption
            public static string Encrypt_RSA(string plainText){
                byte[] data = Encoding.UTF8.GetBytes(plainText);
                byte[] encrypted = rsa.Encrypt(data, RSAEncryptionPadding.Pkcs1);
                return Convert.ToBase64String(encrypted);
            }

            // Single parameter decryption
            public static string Decrypt_RSA(string base64Cipher){
                byte[] data = Convert.FromBase64String(base64Cipher);
                byte[] decrypted = rsa.Decrypt(data, RSAEncryptionPadding.Pkcs1);
                return Encoding.UTF8.GetString(decrypted);
            }


        };





    }
}
