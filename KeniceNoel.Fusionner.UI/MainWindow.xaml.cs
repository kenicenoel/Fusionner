using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using KeniceNoel.Fusionner.UI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace KeniceNoel.Fusionner.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private string _chosenDirectory;
        private readonly DataLayer _dataLayer;
        public MainWindow()
        {
            InitializeComponent();
            _dataLayer = new DataLayer();
            _dataLayer.ItemsRetrieved += OnItemsRetrieved;

        }

        private void OnItemsRetrieved(object sender, int e)
        {
            
        }


        public ObservableCollection<DirectoryItem> DirectoryItems
        {
            get => (ObservableCollection<DirectoryItem>) GetValue(DirectoryItemsProperty);
            set => SetValue(DirectoryItemsProperty, value);
        }

        public static readonly DependencyProperty DirectoryItemsProperty = DependencyProperty.Register(
            "DirectoryItems", 
            typeof(ObservableCollection<DirectoryItem>), 
            typeof(MainWindow), 
            new PropertyMetadata(null));

     
        private void ChooseDirectoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            var folderBrowser = new FolderBrowserDialog();
            folderBrowser.ShowNewFolderButton = true;
            folderBrowser.Description = "Choose the directory that contains the files that you want to merge.";
            using (folderBrowser)
            {
                var result = folderBrowser.ShowDialog();
                if (result != System.Windows.Forms.DialogResult.OK ||
                    string.IsNullOrWhiteSpace(folderBrowser.SelectedPath)) { return;}
                _chosenDirectory = folderBrowser.SelectedPath;
                StatusTextBlock.Text = _chosenDirectory;
                
            }

            DirectoryItems = _dataLayer.GetDirectoryItems(_chosenDirectory);
        }

        private void DirectoryItemsList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var item = (DirectoryItem) e.AddedItems[0];
            var beautifiedJson =  JToken.Parse(Globals.ReadFile(item.FullPathToItem)).ToString(Formatting.Indented);
            ItemPreview.Text = beautifiedJson;
        }
    }
}
