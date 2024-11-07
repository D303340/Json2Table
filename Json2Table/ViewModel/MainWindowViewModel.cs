using Json2Table.MVVM;
using Json2Table.ViewModel.Commands;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Shapes;

namespace Json2Table.ViewModel
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<dynamic> _items;
        public ObservableCollection<dynamic> Items
        {
            get => _items;
            set
            {
                _items = value;
                OnPropertyChanged(nameof(Items));
            }
        }

        public OpenFileCommand OpenJsonCommand { get; private set; }


        public MainWindowViewModel()
        {
            Items = new ObservableCollection<dynamic>();
            OpenJsonCommand = new OpenFileCommand(OpenJsonFile);
        }


        public void OpenJsonFile()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "JSON files (*.json)|*.json|All files (*.*)|*.*",
                Title = "Open JSON File"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string path = openFileDialog.FileName;


                Items.Clear();
                dynamic stuff = GetJsonData(path);

                foreach (var item in stuff)
                {
                    Items.Add(item);
                }
            }

        }

        public dynamic GetJsonData(string path)
        {
            return JsonConvert.DeserializeObject(File.ReadAllText(path));
        }
    }
}
