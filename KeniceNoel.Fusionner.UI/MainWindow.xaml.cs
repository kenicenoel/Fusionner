using System;
using System.Collections.Generic;
using KeniceNoel.Fusionner.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using KeniceNoel.Fusionner.UI.Mergers;
using MessageBox = System.Windows.MessageBox;

namespace KeniceNoel.Fusionner.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string _chosenDirectory;
        private readonly DataLayer _dataLayer;
        private JsonMerger _jsonMerger;
        public ObservableCollection<DirectoryItem> DirectoryItems
        {
            get => (ObservableCollection<DirectoryItem>)GetValue(DirectoryItemsProperty);
            set => SetValue(DirectoryItemsProperty, value);
        }

        public static readonly DependencyProperty DirectoryItemsProperty = DependencyProperty.Register(
            "DirectoryItems",
            typeof(ObservableCollection<DirectoryItem>),
            typeof(MainWindow),
            new PropertyMetadata(null));


        public MainWindow()
        {
            InitializeComponent();
            _dataLayer = new DataLayer();
            _jsonMerger = new JsonMerger();
            SubscribeToEvents();

        }

      


        private void ChooseDirectoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog
            {
                ShowNewFolderButton = true,
                Description = "Choose the directory that contains the files that you want to merge."
            };
            using (folderBrowser)
            {
                var result = folderBrowser.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK ||
                    string.IsNullOrWhiteSpace(folderBrowser.SelectedPath)) { return; }
                _chosenDirectory = folderBrowser.SelectedPath;
                StatusTextBlock.Text = _chosenDirectory;

            }

            DirectoryItems = _dataLayer.GetDirectoryItems(_chosenDirectory);
        }

        private void DirectoryItemsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (DirectoryItem)e.AddedItems[0];
            var beautifiedJson = JToken.Parse(Globals.ReadFile(item.FullPathToItem)).ToString(Formatting.Indented);
            ItemPreview.Text = beautifiedJson;
            ItemPreview.Visibility = Visibility.Visible;
        }

        private void MergeFilesButton_OnClick(object sender, RoutedEventArgs e)
        {
            var promptMessage =
                MessageBox.Show($"Are you sure you want to merge the files in the directory {_chosenDirectory}?",
                    "BuildAndExport Files?", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (promptMessage != MessageBoxResult.Yes) return;
//            Task.Run(() =>
//            {

               _jsonMerger.MergeFiles(DirectoryItems); 
//            });
           

        }

        #region Events subscriptions and support methods

        private void SubscribeToEvents()
        {
            _dataLayer.ItemsRetrieved += OnItemsRetrieved;
            _jsonMerger.FilesCompletedEvent += OnFilesCompletedEvent;
            Globals.FileWrittenEvent += OnFileWrittenEvent;
        }

       

        private void OnFilesCompletedEvent(object sender, List<string> data)
        {
//            Dispatcher.Invoke(() =>
//            {
                _jsonMerger.BuildAndExport(data, _chosenDirectory);
//            });
        }

        private void OnItemsRetrieved(object sender, int e)
        {
            MergeFilesButton.IsEnabled = true;
        }

        private void OnFileWrittenEvent(object sender, EventArgs e)
        {
            MessageBox.Show("File saved successfully", "Merge complete", MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        #endregion
    }


}
