using System;
using System.IO.Compression;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Tar;
using ICSharpCode.SharpZipLib.Encryption;
using ICSharpCode.SharpZipLib.Core;

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using System.Diagnostics;
using System.Web;

namespace Utility.CompressHelper
{
    public class GZip
    {
        /// <summary>   
        /// 构造函数   
        /// </summary>   
        public GZip()
        {

        }
        #region 加密、压缩文件
        /// <summary>   
        /// 压缩文件   
        /// </summary>   
        /// <param name="fileNames">要打包的文件列表</param>   
        /// <param name="GzipFileName">目标文件名</param>   
        /// <param name="CompressionLevel">压缩品质级别（0~9）</param>   
        /// <param name="SleepTimer">休眠时间（单位毫秒）</param>        
        public static void Compress(List<FileInfo> fileNames, string GzipFileName, int CompressionLevel, int SleepTimer)
        {
            ZipOutputStream s = new ZipOutputStream(File.Create(GzipFileName));
            try
            {
                s.SetLevel(CompressionLevel);   //0 - store only to 9 - means best compression   
                foreach (FileInfo file in fileNames)
                {
                    FileStream fs = null;
                    try
                    {
                        fs = file.Open(FileMode.Open, FileAccess.ReadWrite);
                    }
                    catch
                    { continue; }
                    //  方法二，将文件分批读入缓冲区   
                    byte[] data = new byte[2048];
                    int size = 2048;

                    ZipEntry entry = new ZipEntry(Path.GetFileName(file.Name));
                    entry.DateTime = (file.CreationTime > file.LastWriteTime ? file.LastWriteTime : file.CreationTime);
                    s.PutNextEntry(entry);
                    while (true)
                    {
                        size = fs.Read(data, 0, size);
                        if (size <= 0) break;
                        s.Write(data, 0, size);
                    }
                    fs.Close();
                    file.Delete();
                    Thread.Sleep(SleepTimer);
                }
            }
            finally
            {
                s.Finish();
                s.Close();
            }
        }
        #endregion

        #region 解密、解压缩文件
        /// <summary>   
        /// 解压缩文件   
        /// </summary>   
        /// <param name="GzipFile">压缩包文件名</param>   
        /// <param name="targetPath">解压缩目标路径</param>          
        public static string Decompress(string GzipFile, string targetPath)
        {
            //string directoryName = Path.GetDirectoryName(targetPath + "\\") + "\\";   
            string directoryName = targetPath;
            if (!Directory.Exists(directoryName)) Directory.CreateDirectory(directoryName);//生成解压目录   
            string CurrentDirectory = directoryName;
            byte[] data = new byte[2048];
            int size = 2048;

            ZipEntry theEntry = null;
            string _theEntry = "";

            using (ZipInputStream s = new ZipInputStream(File.OpenRead(GzipFile)))
            {
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    if (theEntry.IsDirectory)
                    {
                        // 该结点是目录
                        if (!Directory.Exists(CurrentDirectory + theEntry.Name))
                            Directory.CreateDirectory(CurrentDirectory + theEntry.Name);
                        //保存路径名
                        if (!string.IsNullOrEmpty(theEntry.Name) && theEntry.Name.Contains("/"))
                            _theEntry = theEntry.Name.Substring(0, theEntry.Name.IndexOf('/'));
                    }
                    else
                    {
                        if (theEntry.Name != String.Empty)
                        {
                            //解压文件到指定的目录   
                            using (FileStream streamWriter = File.Create(CurrentDirectory + theEntry.Name))
                            {
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size <= 0) break;

                                    streamWriter.Write(data, 0, size);
                                }
                                streamWriter.Close();
                            }
                        }
                    }
                }
                s.Close();
                return CurrentDirectory + _theEntry;//返回压缩文件的路径
            }
        }
        #endregion



        /// <summary>
        /// 解压缩文件
        /// </summary>
        /// <param name="sourceFile"></param>
        /// <param name="destinationFile"></param>
        public static void DeCompressFile(string sourceFile, string destinationFile)
        {
            if (!File.Exists(sourceFile)) throw new FileNotFoundException();
            using (FileStream sourceStream = new FileStream(sourceFile, FileMode.Open))
            {
                byte[] quartetBuffer = new byte[4];
                const int bufferLength = 1024 * 64;
                /*压缩文件的流的最后四个字节保存的是文件未压缩前的长度信息，
                 * 把该字节数组转换成int型，可获取文件长度。
                 * */
                int position = (int)sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);
                sourceStream.Position = 0;
                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                byte[] buffer = new byte[1024 * 64];
                using (GZipStream decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true))
                {
                    using (FileStream destinationStream = new FileStream(destinationFile, FileMode.Create))
                    {
                        int bytesRead = 0;
                        while ((bytesRead = decompressedStream.Read(buffer, 0, bufferLength)) >= bufferLength)
                        {
                            destinationStream.Write(buffer, 0, bufferLength);
                        }
                        destinationStream.Write(buffer, 0, bytesRead);
                        destinationStream.Flush();
                    }
                }
            }
        }

        /// <summary>
        /// 利用 WinRAR 进行解压缩（注意：需要安装WINrar程序）
        /// </summary>
        /// <param name="path">文件解压路径（绝对）</param>
        /// <param name="rarPath">将要解压缩的 .rar 文件的存放目录（绝对路径）</param>
        /// <param name="rarName">将要解压缩的 .rar 文件名（包括后缀）</param>
        /// <returns>true 或 false。解压缩成功返回 true，反之，false。</returns>
        public bool UnRAR(string path, string rarPath, string rarName)
        {
            bool flag = false;
            string rarexe;
            RegistryKey regkey;
            Object regvalue;
            string cmd;
            ProcessStartInfo startinfo;
            Process process;
            try
            {
                regkey = Registry.ClassesRoot.OpenSubKey(@"Applications\WinRAR.exe\shell\open\command");
                regvalue = regkey.GetValue("");
                rarexe = regvalue.ToString();
                regkey.Close();
                rarexe = rarexe.Substring(1, rarexe.Length - 7);

                Directory.CreateDirectory(path);
                //解压缩命令，相当于在要压缩文件(rarName)上点右键->WinRAR->解压到当前文件夹
                cmd = string.Format("x {0} {1} -y",
                                    rarName,
                                    path);
                startinfo = new ProcessStartInfo();
                startinfo.FileName = rarexe;
                startinfo.Arguments = cmd;
                startinfo.WindowStyle = ProcessWindowStyle.Hidden;

                startinfo.WorkingDirectory = rarPath;
                process = new Process();
                process.StartInfo = startinfo;
                process.Start();
                process.WaitForExit();
                if (process.HasExited)
                {
                    flag = true;
                }
                process.Close();
            }
            catch (Exception e)
            {
                throw e;
            }
            return flag;
        }
    }
}