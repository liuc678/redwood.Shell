using System;
using System.IO;
using System.Security.Cryptography;

namespace BaseClass
{
    public static class Utils
    {
        public static string ComputeFileHash(string filePath,bool checkFileExists=true)
        {
            if(checkFileExists)
            {
                if (!File.Exists(filePath))
                    return string.Empty;
            }
            using (FileStream stream = File.OpenRead(filePath))
            {
                using (SHA256 sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    stream.Close();
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToUpper();
                }
            }
        }
    }
}
