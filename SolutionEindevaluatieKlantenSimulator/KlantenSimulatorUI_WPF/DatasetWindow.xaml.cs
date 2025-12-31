using KlantenSimulatorBL.Manager;
using KlantenSimulatorBL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace KlantenSimulatorUI_WPF
{
    /// <summary>
    /// Interaction logic for DatasetWindow.xaml
    /// </summary>
    public partial class DatasetWindow : Window
    {
        private readonly ObservableCollection<Dataset> _allDatasets;
        public  Dataset _selectedDataset;

        private readonly SimulationService _service;

        public DatasetWindow(SimulationService service, string countryName)
        {
            InitializeComponent();
            _service = service;
            _allDatasets = new ObservableCollection<Dataset>(_service.GetDataset(countryName));
            ComboBoxAllDataSets.ItemsSource = _allDatasets;
        }

        private void SelectedDataset_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedDataset = ComboBoxAllDataSets.SelectedItem as Dataset;
            DialogResult = true;
            Close();
        }
    }
}
