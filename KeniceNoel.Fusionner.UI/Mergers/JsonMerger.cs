using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using KeniceNoel.Fusionner.UI.Models;

namespace KeniceNoel.Fusionner.UI.Mergers
{
    public class JsonMerger
    {
        public event EventHandler<List<string>> FilesCompletedEvent;
        public static string RemoveBrackets(string content)
        {
            var openBracket = content.IndexOf("[");
            content = content.Substring(openBracket + 1, content.Length - openBracket - 1);

            var closeBracket = content.LastIndexOf("]");
            content = content.Substring(0, closeBracket);

            return content;
        }

        public void MergeFiles(ObservableCollection<DirectoryItem> items)
        {
            var jsonFiles = new List<string>();
            items.ToList()
                .ForEach(item =>
                {
                    jsonFiles.Add(Globals.ReadFile(item.FullPathToItem));
                });
            FilesCompletedEvent?.Invoke(this, jsonFiles);
        }

        public void BuildAndExport(List<string> jsonData, string directory)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("[");
                jsonData.ForEach(data =>
                {
                        var json = data;
                        var cleared = RemoveBrackets(json);
                        stringBuilder.AppendLine(cleared);
                        stringBuilder.Append(",");
                });
                

                stringBuilder.AppendLine("]");
                Globals.WriteFile(stringBuilder.ToString(), directory );
        }
    }
}
