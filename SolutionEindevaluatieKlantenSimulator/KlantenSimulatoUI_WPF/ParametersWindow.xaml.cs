using System;
using System.Collections.Generic;
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
    /// Interaction logic for ParametersWindow.xaml
    /// </summary>
    public partial class ParametersWindow : Window
    {
        public ParametersWindow()
        {
            InitializeComponent();
        }

        private void SelectCities_Click(object sender, RoutedEventArgs e)
        {
            // Uncheck "Select All Cities" radio button
            RadioSelectAllCities.IsChecked = false;

            new CityWindow().ShowDialog();
        }
    }
}
