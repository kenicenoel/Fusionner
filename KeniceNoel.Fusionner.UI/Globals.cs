using System;
using System.IO;

namespace KeniceNoel.Fusionner.UI
{
    public class Globals
    {
        public enum DirectoryItemType
        {
            Directory,
            File,
            Invalid
        }

        public enum SupportedFileTypes
        {
            Json,
            Csv,
            Doc
        }

        public static string ReadFile(string PathToFile)
        {
           return File.ReadAllText(PathToFile);
        }
    }
}
