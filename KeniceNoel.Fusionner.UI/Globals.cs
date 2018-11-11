using System;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;

namespace KeniceNoel.Fusionner.UI
{
    public static class Globals
    {
        public static event EventHandler FileWrittenEvent;
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

        public static string ReadFile(string pathToFile)
        {
            return File.ReadAllText(pathToFile);
        }

        public static void WriteFile(string content, string pathToSaveFile)
        {           
            var folderBrowser = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = "Choose the directory to store the merged files."
            };
            using (folderBrowser)
            {
                var result = folderBrowser.ShowDialog();
                if (result != DialogResult.OK ||
                    string.IsNullOrWhiteSpace(folderBrowser.SelectedPath)) { return; }
               var savePath = folderBrowser.SelectedPath;
                // Write the string array to a new file ".
                using (var outputFile = new StreamWriter(Path.Combine(savePath, "merged_documents.json")))
                {
                    outputFile.Write(content);
                };
                FileWrittenEvent?.Invoke(null, EventArgs.Empty);
            }
            
        }
    }
}
