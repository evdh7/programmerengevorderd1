using Microsoft.Extensions.Configuration;
using Microsoft.Win32;
using ProvinciesBL.Beheerders;
using ProvinciesUtil;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ProvinciesBL.Model;

namespace ProvinciesUI_WPF
{
    public partial class StatistiekenWindow : Window
    {
        List<string> messages = new();
        public StatistiekenWindow(Statistieken stats, string zipFile)//beste manier om hier om te zetten naar string
        {
            InitializeComponent();
            TextBoxZip.Text = zipFile;
            messages.AddRange(stats.ProvinciesAantalGemeentes.Select(x => $"{x.Key}, {x.Value} gemeenten"));
            messages.AddRange(stats.GemeentenAantalStraten.Select(x => $"{x.Key}, {x.Value} straten"));

            ListBoxStatistieken.ItemsSource = messages;
        }
        private void ButtonSluitVenster_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        /// <summary>
        /// Interaction logic for MainWindow.xaml
        /// </summary>

    }
}