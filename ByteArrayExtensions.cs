using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BytePatcherApp
{
    public static class ByteArrayExtensions
    {
        public static int FindPatternIndex(this byte[] buffer, byte[] pattern)
        {
            if (pattern == null || buffer == null)
            {
                Log.Warning("Fields cannot be empty");
                return 0;
            }
            else
            {
                for (int i = 0; i <= buffer.Length - pattern.Length; i++)
                {
                    if (buffer.Skip(i).Take(pattern.Length).SequenceEqual(pattern))
                    {
                        return i;
                    }
                }
                return -1;
            }
            }
            public static async Task WriteAllBytesAsync(string filePath, byte[] buffer)
        {
            if (filePath == "" || buffer == null)
            {
                Log.Warning("Fields cannot be empty");
            }
            else
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    await fileStream.WriteAsync(buffer, 0, buffer.Length);
                }
            }
        }
            public static async Task<byte[]> ReadAllBytesAsync(string filePath)
        {
            if (filePath == "")
            {
                Log.Warning("Filepath cannot be empty");
                return null;
            }
            else
            {
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 4096, useAsync: true))
                {
                    byte[] buffer = new byte[fileStream.Length];
                    await fileStream.ReadAsync(buffer, 0, (int)fileStream.Length);
                    return buffer;
                }
            }

        }
        public static byte[] ConvertHexStringToByteArray(string hexString)
        {
            hexString = hexString.Replace(" ", ""); // Remove any spaces
            int length = hexString.Length;

            byte[] byteArray = new byte[length / 2];
            for (int i = 0; i < length; i += 2)
            {
                byteArray[i / 2] = Convert.ToByte(hexString.Substring(i, 2), 16);
            }
            return byteArray;
        }

    }
}
