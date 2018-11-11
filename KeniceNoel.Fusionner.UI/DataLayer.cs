using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using KeniceNoel.Fusionner.UI.Models;

namespace KeniceNoel.Fusionner.UI
{
    public class DataLayer
    {
        public event EventHandler<int> ItemsRetrieved;
        public ObservableCollection<DirectoryItem> GetDirectoryItems(string directory)
        {
            var items = new ObservableCollection<DirectoryItem>();
            var contents = Directory.GetFiles(directory).ToList();

            contents.ForEach(content =>
            {
                var fileOrDirectory = IsFileOrDirectory(content);
                var directoryItem = new DirectoryItem
                {
                    FileImageUrl = GetImageAsset(fileOrDirectory),
                    Label = GetFileOrDirectoryName(fileOrDirectory, content),
                    FullPathToItem = content
                };
                items.Add(directoryItem);
            });
            ItemsRetrieved?.Invoke(this, items.Count);
            return items;
        }

        #region Support Methods

        private Globals.DirectoryItemType IsFileOrDirectory(string content)
        {
            if (File.Exists(content))
            {
                return Globals.DirectoryItemType.File;
            }

            return Directory.Exists(content) ? Globals.DirectoryItemType.Directory : Globals.DirectoryItemType.Invalid;
        }

        private static string GetImageAsset(Globals.DirectoryItemType type)
        {
            var imageAsset = string.Empty;
            switch (type)
            {
                case Globals.DirectoryItemType.File:
                    imageAsset = "Assets/Images/Feather/file.png";
                    break;
                case Globals.DirectoryItemType.Directory:
                    imageAsset = "Assets/Images/Feather/folder.png";
                    break;
            }
            return imageAsset;
        }

        

        private static string GetFileOrDirectoryName(Globals.DirectoryItemType type, string content)
        {
            var label = string.Empty;
            switch (type)
            {
                case Globals.DirectoryItemType.File:
                    label = Path.GetFileName(content);
                    break;
                case Globals.DirectoryItemType.Directory:
                    label = Path.GetDirectoryName(content);
                    break;
            }
            return label;
        }

        #endregion

    }


}
