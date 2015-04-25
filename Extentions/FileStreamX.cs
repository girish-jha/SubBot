using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;

namespace RosettaMoviesDesktop.Extentions
{
   public static class FileStreamX
    {
        public static string GenerateHashkey(this FileStream fileStream)
        {
            const int readsize = 64*1024;
            long fileSize = fileStream.Length;

            var bytes = new List<byte>();
            var fileReader = new Byte[readsize];

            fileStream.Read(fileReader, 0, readsize);
            
            bytes.AddRange(fileReader);
            var offset = (int)fileSize - readsize;
            //fileReader = new Byte[readsize];

            fileStream.Seek(offset, SeekOrigin.Begin);
            fileStream.Read(fileReader,0, readsize);
            bytes.AddRange(fileReader);
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(bytes.ToArray());

            return BitConverter.ToString(hash).Replace("-", string.Empty).ToLower();
        }


    }
}