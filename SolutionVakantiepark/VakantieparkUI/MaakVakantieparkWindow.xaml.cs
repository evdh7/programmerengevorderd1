using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
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
using VakantieparkBL.Model;
using VakantieparkBL.Services;

namespace VakantieparkUI
{
    /// <summary>
    /// Interaction logic for MaakVakantieparkWindow.xaml
    /// </summary>
    public partial class MaakVakantieparkWindow : Window
    {
        private VakantieparkService service;

        public VakantieparkDTO Vakantiepark { get; set; }
        public Wooneenheid Wooneenheid { get; set; }

        public MaakVakantieparkWindow(VakantieparkService service)
        {
            InitializeComponent();
            this.service = service;
            ComboBox_ContactList.ItemsSource = service.GeefContacten();
            
        }

        private void ButtonFaciliteiten_Click(object sender, RoutedEventArgs e)
        {
            FaciliteitenWindow w = new FaciliteitenWindow(service);
            bool? result = w.ShowDialog();
            if (result == true)
            {
                var faciliteiten = w.ListBoxSelectedFaciliteiten;
                ListBoxSelectedFaciliteiten.ItemsSource = faciliteiten.ItemsSource;
            }
        }

        private void ButtonWooneenheden_Click(object sender, RoutedEventArgs e)
        {
            WooneenhedenWindow w = new WooneenhedenWindow(service);
            bool? result = w.ShowDialog();
            if (result == true)
            {
                var wooneenheden = w.ListBoxWooneenheden;
                ListBoxSelectedWooneenheden.ItemsSource = wooneenheden.ItemsSource;
            }
        }
        private void ButtonMaakVakantiepark_Click(object sender, RoutedEventArgs e)
        {
            List<Faciliteit> listFaciliteiten = ListBoxSelectedFaciliteiten.Items.OfType<Faciliteit>().ToList();
            List<Wooneenheid> listWooneenheden = ListBoxSelectedFaciliteiten.Items.OfType<Wooneenheid>().ToList();

            Object contact = ComboBox_ContactList.SelectedItem;
            string contactSelected = contact.ToString();
            int capaciteit = listWooneenheden.Where(x => x.Status == HuisStatus.InGebruik).Sum(x => x.Capaciteit);

            Vakantiepark = new VakantieparkDTO(4, TextBoxNaamVakantiepark.Text, TextBoxLocatie.Text, capaciteit, listWooneenheden.Count(), listWooneenheden.Count(), listFaciliteiten.Count(), contactSelected);
            DialogResult = true;
            Close();
        }
    }
}
