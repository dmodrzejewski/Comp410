﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileCopy
{
    class Program
    {
        static void Main(string[] red)
        {

            red = new string[3];

            for (int i = 0; i < 3; i++)
            {
                red[i] = Console.ReadLine();
            }
            var bufferSize = int.Parse(red[0]);
            var sourceFile = new FileInfo(red[1]);
            var destFile = new FileInfo(red[2]);
            var start = DateTime.Now;
            CopyFile(sourceFile, destFile, bufferSize);
            var finish = DateTime.Now;

            var totalMiliSeconds = (finish - start).TotalMilliseconds;
            var fileSize = sourceFile.Length;

            var bytesPerMiliSeond = fileSize / totalMiliSeconds;
            var bytesPerSecond = bytesPerMiliSeond / 1000;
            Console.WriteLine("Wrote {0} bytes in {1} MS, at a rate of {2} bytes/second", fileSize, totalMiliSeconds, bytesPerSecond);
            Console.ReadLine();

        }

        private static void CopyFile(FileInfo sourceFile, FileInfo destFile, int bufferSize)
        {
            using (var source = OpenSourceFile(sourceFile))
            {
                using (var dest = OpenDestFile(destFile))
                {
                    CopyData(source, dest, bufferSize);
                }
            }
        }

        private static void CopyData(Stream source, Stream dest, int bufferSize)
        {
            var buffer = new byte[bufferSize];
            int readLen;
            while ((readLen = source.Read(buffer, 0, buffer.Length)) != 0)
            {
                dest.Write(buffer, 0, readLen);
            }
        }

        private static Stream OpenSourceFile(FileInfo file)
        {
            return new FileStream(file.FullName, FileMode.Open, FileAccess.Read);
        }

        private static Stream OpenDestFile(FileInfo file)
        {
            return new FileStream(file.FullName, FileMode.Create, FileAccess.Write);

        }
    }
}