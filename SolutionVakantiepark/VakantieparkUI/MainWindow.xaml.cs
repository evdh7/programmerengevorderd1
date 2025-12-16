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
using VakantieparkBL;
using VakantieparkBL.Interfaces;
using VakantieparkBL.Model;
using VakantieparkBL.Services;
using VakantieparkUtils;

namespace VakantieparkUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<VakantieparkDTO> vakantieparken;

        private VakantieparkService service;


        public MainWindow()
        {
            InitializeComponent();
            IVakantieparkRepository repo = VakantieparkRepositoryFactory.GetVakantieRepository();
            service = new VakantieparkService(repo);
            vakantieparken = new ObservableCollection<VakantieparkDTO>(service.GeefVakantieparken());
            DataGridVakantieparken.ItemsSource = vakantieparken;

        }

        private void ToonWooneenheden_DoubleClick(object sender, RoutedEventArgs e)
        {
            VakantieparkDTO vakpark = (VakantieparkDTO)DataGridVakantieparken.SelectedItem;
            DataGridWooneenheden.ItemsSource = service.GeefWooneenheden(vakpark.Id);

        }
        private void MenuItemMaakVakantieparkNew_Click(object sender, RoutedEventArgs e)
        {

            MaakVakantieparkWindow w = new MaakVakantieparkWindow(service);
            bool? result = w.ShowDialog();
            if (result == true)
            {
                var nieuwVakantiepark = w.Vakantiepark;
                vakantieparken.Add(nieuwVakantiepark);
            }

        }
        

    }
}
