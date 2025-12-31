using KlantenSimulatorBL.Manager;
using KlantenSimulatorBL.Model;
using KlantenSimulatorUI_WPF.Model;
using System.Collections.ObjectModel;
using System.Windows;

namespace KlantenSimulatorUI_WPF
{
    /// <summary>
    /// Interaction logic for ParametersWindow.xaml
    /// </summary>
    public partial class ParametersWindow : Window
    {
        private readonly SimulationService _service;
        private Dataset _selectedDataset;
        private readonly ObservableCollection<City> _allCities;
        private List<City> _selectedCities;
        private string _countryName;
        private readonly Client _client;
        private uint _amountOfCustomers;
        private uint _maxAge;
        private uint _minAge;
        private AddressParameterModel _addressParameters;
        public ParametersWindow(SimulationService service, Client client)
        {
            InitializeComponent();
            _service = service;
            _client = client;
            ComboBox_Countries.ItemsSource = service.GetCountries();

        }

        private void SelectCities_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck "Select All Cities" radio button
            RadioSelectAllCities.IsChecked = false;

            _countryName = (string)ComboBox_Countries.SelectedItem;

            CityWindow w = new(_service, _countryName);

            bool? result = w.ShowDialog();

            if (result == true)
            {
                var selectedCities = w.SelectedCities;
                Label_SelectedCitiesSummary.Content = $"{selectedCities.Count} cities selected";
                Label_SelectedCitiesSummary.Visibility = Visibility.Visible;
                Button_ViewSelectedCities.Visibility = Visibility.Visible;
                _selectedCities = [.. w.SelectedCities];
            }

        }
        private void RadioSelectAllCities_Checked(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_countryName))
            {
                return;
            }
            Button_ViewSelectedCities.Visibility = Visibility.Collapsed;

            // Reset selected cities to ALL cities for the selected country

            _selectedCities = _service.GetCities(_countryName).ToList();
            Label_SelectedCitiesSummary.Content = $"{_selectedCities.Count} cities selected";
            Label_SelectedCitiesSummary.Visibility = Visibility.Visible;


        }



        private void AddressParameters_Click(object sender, RoutedEventArgs e)
        {
            AddressParameters w = new();
            bool? result = w.ShowDialog();
            if (result == true)
            {
                _addressParameters = new AddressParameterModel
                {
                    MaxHouseNumber = w.MaxHouseNumber,
                    PercentageLetters = w.PercentageLetters
                };

                AddressInput.ItemsSource = new List<string>
                {
                    $"Max house number: {w.MaxHouseNumber}",
                    $"Percentage letters: {w.PercentageLetters}%"
                };
            }
        }
        private void Button_ViewSelectedCities_Click(object sender, RoutedEventArgs e)
        {
            SelectedCitiesWindow w = new(_selectedCities);
            bool? result = w.ShowDialog();
        }

        private SimulationParameters GetParameters()
        {
            return new SimulationParameters
            {
                Client = _client,
                Country = (string)ComboBox_Countries.SelectedItem,
                SelectedDataset = _selectedDataset,
                SelectedCities = _selectedCities,
                AmountOfCustomers = _amountOfCustomers,
                MaxHouseNumber = _addressParameters.MaxHouseNumber,
                PercentageLetters = _addressParameters.PercentageLetters,
                MaxAge = _maxAge,
                MinAge = _minAge
            };
        }

        private void TextBoxAmount_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            e.Handled = !e.Text.All(char.IsDigit);
        }

        private void Country_Selected(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            _countryName = (string)ComboBox_Countries.SelectedItem;
            _selectedCities = _service.GetCities(_countryName).ToList();
            Label_SelectedCitiesSummary.Content = $"{_selectedCities.Count} cities selected";
            Label_SelectedCitiesSummary.Visibility = Visibility.Visible;
            Button_ViewSelectedCities.Visibility = Visibility.Collapsed;

            DatasetWindow w = new(_service, _countryName);
            bool? result = w.ShowDialog();

            if (result == true)
            {
                _selectedDataset = w._selectedDataset;
            }

        }

        private void ButtonClick_StartWindow(object sender, RoutedEventArgs e)
        {
            try
            {
                _amountOfCustomers = uint.Parse(TextBoxAmount.Text);
                _maxAge = uint.Parse(TextBoxMaxAge.Text);
                _minAge = uint.Parse(TextBoxMinAge.Text);
            }

            catch
            {
                MessageBox.Show("Please enter a valid number.");
                return;
            }
            if (ComboBox_Countries.SelectedItem is null)
            {
                MessageBox.Show("Please select a country.");
                return;
            }

            if (_selectedDataset is null)
            {
                MessageBox.Show("Please select a dataset.");
                return;
            }

            if (_selectedCities is null || _selectedCities.Count == 0)
            {
                MessageBox.Show("Please select at least one city or choose 'Select all cities'.");
                return;
            }

            SimulationParameters parameters = GetParameters();

            try
            {
                _service.StartSimulation(parameters);

                MessageBox.Show("Simulation started successfully.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error starting simulation: {ex.Message}");
            }
       
        }
    }
}
