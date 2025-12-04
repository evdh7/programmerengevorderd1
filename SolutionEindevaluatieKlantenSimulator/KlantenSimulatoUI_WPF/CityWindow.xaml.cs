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
    /// Interaction logic for CityWindow.xaml
    /// </summary>
    public partial class CityWindow : Window
    {
        public List<City> SelectedCitiesResult { get; private set; }
        public CityWindow()
        {
            InitializeComponent();
            SelectedCitiesResult = new List<CityViewModel>();
        }

        private void ButtonClick_Confirm(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;

        }
    }
}
