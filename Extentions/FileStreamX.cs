using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace RosettaMoviesDesktop.Extentions
{
   public static class FileStreamX
    {
       const int _64KB = 64 * 1024;
        public static string GenerateHashkey(this FileStream fileStream)
        {
            var bytes = fileStream.GetFirst64KB().Append(fileStream.GetLast64KB());
            return bytes.GetMD5HashKey();
        }

       #region Private Helper Methods

       private static byte[] GetFirst64KB(this FileStream fileStream)
       {
           var fileReader = new Byte[_64KB];

           fileStream.Read(fileReader, 0, _64KB);
           return fileReader;
       }

       private static byte[] GetLast64KB(this FileStream fileStream)
       {
           var fileReader = new Byte[_64KB];
           long fileSize = fileStream.Length;

           var offset = fileSize - _64KB;

           fileStream.Seek(offset, SeekOrigin.Begin);
           fileStream.Read(fileReader, 0, _64KB);
           return fileReader;
       }

       private static byte[] Append(this byte[] @this, byte[] other)
       {
           var returnVal = new List<byte>(@this);
           returnVal.AddRange(other);
           return returnVal.ToArray();
       }

       private static string GetMD5HashKey(this byte[] @this)
       {
           var md5 = new MD5CryptoServiceProvider();
           var hash = md5.ComputeHash(@this);

           return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
       }

       #endregion

    }
}