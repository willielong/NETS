/****************************
 * 目的：对象进行加密和解密
 * 创建人：Author
 * 创建时间：2018.04.22 17:04
 * 更新时间：
 * 更新内容：
 * 修改人：
 * 返回结果;
 * 修改后的结果： 
 *******************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Workflow.comm
{
    public class FileEncrypt
    {
        /// <summary>
        /// 加密字符
        /// </summary>
        private static string key = "WORKFLOW";

        /// <summary>
        /// 将密钥加密写入到文件
        /// </summary>
        /// <param name="sInputFilename">密密钥</param>
        /// <param name="sOutputFilename">密钥文件路径</param>
        /// <param name="sKey"></param>
        private static void EncryptFile(string sInputFilename, string sOutputFilename, string sKey)
        {

            using (FileStream fsEncrypted = new FileStream(sOutputFilename, FileMode.Create, FileAccess.Write))
            {
                DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
                DES.Key = Encoding.UTF8.GetBytes(sKey);
                DES.IV = Encoding.UTF8.GetBytes(sKey);
                ICryptoTransform desencrypt = DES.CreateEncryptor();
                using (CryptoStream cryptostream = new CryptoStream(fsEncrypted, desencrypt, CryptoStreamMode.Write))
                {
                    byte[] fsInput = Encoding.UTF8.GetBytes(sInputFilename);
                    cryptostream.Write(fsInput, 0, fsInput.Length);
                    cryptostream.FlushFinalBlock();
                }
            }
        }
        /// <summary>
        /// 打开密钥文件
        /// </summary>
        /// <param name="sInputFilename">密钥路径</param>
        /// <param name="sKey"></param>
        private static string DecryptFile(string sInputFilename, string sKey)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();

            DES.Key = Encoding.UTF8.GetBytes(sKey);
            DES.IV = Encoding.UTF8.GetBytes(sKey);

            string reft = string.Empty;

            using (FileStream fsread = new FileStream(sInputFilename, FileMode.Open, FileAccess.Read))
            {
                ICryptoTransform desdecrypt = DES.CreateDecryptor();
                using (CryptoStream cryptostreamDecr = new CryptoStream(fsread, desdecrypt, CryptoStreamMode.Read))
                {
                    StreamReader read = new StreamReader(cryptostreamDecr, Encoding.UTF8);
                    reft = read.ReadToEnd();
                    fsread.Flush();
                }
            }

            return reft;
        }

        /// <summary>
        /// 加密文件
        /// </summary>
        /// <param name="filename">文件存放路径</param>
        /// <param name="soce">加密内容</param>
        public static void Encrypt(string filename, string soce)
        {
            try
            {
                EncryptFile(soce, filename, key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 解密文件
        /// </summary>
        /// <param name="filename">打开文件路径</param>
        /// <returns>返回加密文件的内容</returns>
        public static string Decrypt(string filename)
        {
            try
            {
                return DecryptFile(filename, key);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
