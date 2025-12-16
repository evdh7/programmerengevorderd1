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
using VakantieparkBL.Model;
using VakantieparkBL.Services;

namespace VakantieparkUI
{
    /// <summary>
    /// Interaction logic for FaciliteitenWindow.xaml
    /// </summary>
    public partial class FaciliteitenWindow : Window
    {
        private VakantieparkService service;
        private ObservableCollection<Faciliteit> alleFaciliteiten;
        private ObservableCollection<Faciliteit> selectedFaciliteiten;
        public List<Faciliteit> Faciliteiten;

        public FaciliteitenWindow(VakantieparkService service)
        {
            InitializeComponent();
            this.service = service;
            alleFaciliteiten = new ObservableCollection<Faciliteit>(service.GeefFaciliteiten());
            selectedFaciliteiten = new();
            ListBoxAlleFaciliteiten.ItemsSource = alleFaciliteiten;
            ListBoxSelectedFaciliteiten.ItemsSource = selectedFaciliteiten;
            Faciliteiten = new List<Faciliteit>();

        }
        private void ButtonAddAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Faciliteit faciliteit in alleFaciliteiten)
            {
                selectedFaciliteiten.Add(faciliteit);
            }
            alleFaciliteiten.Clear();
        }
        private void ButtonAddSelected_Click(object sender, RoutedEventArgs e)
        {
            List<Faciliteit> data = new();
            foreach (Faciliteit faciliteit in ListBoxAlleFaciliteiten.SelectedItems)
            {
                data.Add(faciliteit);
            }
            foreach (Faciliteit faciliteit in data)
            {
                selectedFaciliteiten.Add(faciliteit);
                alleFaciliteiten.Remove(faciliteit);
            }
        }
        private void ButtonRemoveAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (Faciliteit faciliteit in selectedFaciliteiten)
            {
                alleFaciliteiten.Add(faciliteit);
            }

          
        }
        private void ButtonRemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            List<Faciliteit> data = new();
            foreach (Faciliteit faciliteit in ListBoxSelectedFaciliteiten.SelectedItems)
            {
                data.Add(faciliteit);
            }
            foreach (Faciliteit faciliteit in data)
            {
                alleFaciliteiten.Add(faciliteit);
                selectedFaciliteiten.Remove(faciliteit);
            }
        }

        private void ButtonMaakNieuw_Click(object sender, RoutedEventArgs e)
        {
            NieuweFaciliteit w = new NieuweFaciliteit();

            bool? result = w.ShowDialog();

            if (result == true) 
            {
                var nieuweFaciliteit = w.TextBoxFaciliteit;
                ListBoxSelectedFaciliteiten.ItemsSource = nieuweFaciliteit.Text;
                Faciliteit faciliteitNew = new Faciliteit(nieuweFaciliteit.Text);
                service.VoegFaciliteitToe(faciliteitNew);
            }

        }
        private void ButtonSluiten_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }
      
    }
}
