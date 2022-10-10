using System.IO;
using System.Reflection;
using UnityEngine;

namespace Utility
{
    public static class FileWriterManager
    {
        public static string SaveFile(string folder, string fileName, string content)
        {
            string file = Path.Combine(folder, fileName);
            using (FileStream fs = new FileStream(file, FileMode.Create))
            {
                using (StreamWriter str = new StreamWriter(fs))
                {
                    str.WriteLine(content);
                    str.Flush();
                }
            }
            return file;
        }

        public static string GetFullSourceFilePath()
        {
            string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Directory.GetParent(path).Parent.FullName;
            path += "\\Assets\\Source";
            return path;
        }
    }
}
