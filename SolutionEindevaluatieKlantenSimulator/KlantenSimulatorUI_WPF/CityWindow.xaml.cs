using KlantenSimulatorBL.Manager;
using KlantenSimulatorBL.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for CityWindow.xaml
    /// </summary>
    public partial class CityWindow : Window
    {
        private readonly SimulationService _service;

        private readonly ObservableCollection<City> _allCities;
        private readonly ObservableCollection<City> _selectedCities;
        public ObservableCollection<City> SelectedCities => _selectedCities;
        public CityWindow(SimulationService service, string countryName)
        {
            InitializeComponent();
            _service = service;
            _allCities = new ObservableCollection<City>(_service.GetCities(countryName));
            _selectedCities = [];
            ListBoxAllCities.ItemsSource = _allCities;
            ListBoxSelectedCities.ItemsSource = _selectedCities;
        }

        private void ButtonAddAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (City city in _allCities)
            {
                _selectedCities.Add(city);
            }
            _allCities.Clear();

        }

        private void ButtonAddSelected_Click(object sender, RoutedEventArgs e)
        {
            List<City> data = new();

            foreach (City city in ListBoxAllCities.SelectedItems)
            {
                data.Add(city);
            }

            foreach (City city in data)
            {
                _selectedCities.Add(city);
                _allCities.Remove(city);
            }

        }

        private void ButtonRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            List<City> data = new();

            foreach (City city in ListBoxSelectedCities.SelectedItems)
            {
                data.Add(city);
            }

            foreach (City city in data)
            {
                _allCities.Add(city);
                _selectedCities.Remove(city);
            }
        }

        private void ButtonRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (City city in _selectedCities)
            {
                _allCities.Add(city);
            }
            _selectedCities.Clear ();

        }
        private void ButtonClick_Confirm(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
    }
}
