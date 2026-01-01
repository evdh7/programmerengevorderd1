using KlantenSimulatorBL.Model;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
    /// Interaction logic for SimulationDataWindow.xaml
    /// </summary>
    public partial class SimulationDataWindow : Window
    {
        private readonly List<Address> _addresses;
        public SimulationDataWindow(List<Address> addresses)
        {
            InitializeComponent();
            _addresses = addresses;
            SimulationDataGrid.ItemsSource = _addresses;
            
        }
     
    private void SaveData_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new SaveFileDialog
        {
            Title = "Save simulation data",
            Filter = "JSON file (*.json)|*.json|Text file (*.txt)|*.txt",
            FileName = "simulation-data"
        };

        if (dialog.ShowDialog() == true)
        {
            string output;

            if (dialog.FilterIndex == 1) // JSON
            {
                output = JsonSerializer.Serialize(_addresses, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                });
            }
            else // TXT
            {
                output = string.Join(Environment.NewLine, _addresses.Select(s => s.ToString()));
            }

            File.WriteAllText(dialog.FileName, output);
        }
    }



}
}
