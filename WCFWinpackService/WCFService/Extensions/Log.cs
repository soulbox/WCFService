using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Extensions
{
    public static class Log
    {
        private const string Folder = "WinpackWcfServiceLog";
        private static string Path => System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), Folder);
        private static string File => System.IO.Path.Combine(Path, "Log.txt");

        public static string Append(string msg)
        {
            var Lines = msg.Split('\n');
            System.IO.File.AppendAllLines(File, Lines, Encoding.UTF8);
            return msg;
        }
        public static string AppendLog(this string source)
        {
            var Lines = source.Split('\n');
            List<string> Line = new List<string>()
            { "-------►" +DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")+"◄-------" };
            Line.AddRange(Lines);
            Line.Add( new string ('-',35));
            CheckPath(File);
            System.IO.File.AppendAllLines(File, Line, Encoding.UTF8);
            return source;
        }
        static void CheckPath(string path)
        {
            System.IO.FileInfo file = new System.IO.FileInfo(path);
            file.Directory.Create();
        }
    }
}