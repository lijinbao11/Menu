using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SystemMenu.Helper
{
    /// <summary>
    /// DES加密帮助类
    /// </summary>
    public class DesEncryptHelper
    {
        /// <summary>
        /// 字符串加密
        /// </summary>
        /// <param name="input">加密字符串</param>
        /// <param name="key">加密key(有默认值，可自定义)</param>
        /// <returns></returns>
        public static string DesEncrypt(string input, string key = "sblw-3hn8-sqoy19")
        {

            byte[] inputArray = Encoding.UTF8.GetBytes(input);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key);
            tripleDES.Key = byteKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateEncryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// 字符串解密
        /// </summary>
        /// <param name="input">解密字符串</param>
        /// <param name="key">加密key(有默认值，可自定义)</param>
        /// <returns></returns>
        public static string DesDecrypt(string input, string key = "sblw-3hn8-sqoy19")
        {
            byte[] inputArray = Convert.FromBase64String(input);
            var tripleDES = TripleDES.Create();
            var byteKey = Encoding.UTF8.GetBytes(key);
            tripleDES.Key = byteKey;
            tripleDES.Mode = CipherMode.ECB;
            tripleDES.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tripleDES.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(inputArray, 0, inputArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
