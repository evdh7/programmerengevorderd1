using KlantenSimulatorBL.DTOs;
using KlantenSimulatorBL.Model;
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
    /// Interaction logic for SelectedCities.xaml
    /// </summary>
    public partial class SelectedCitiesWindow : Window
    {
        public SelectedCitiesWindow(IEnumerable<CityDTO> cities)
        {
            InitializeComponent();
            ListBox_SelectedCitiesWindow.ItemsSource = cities; 
        }
    }
    
}
