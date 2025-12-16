using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using VakantieparkBL.Model;
using VakantieparkBL.Services;

namespace VakantieparkUI
{
    /// <summary>
    /// Interaction logic for WooneenhedenWindow.xaml
    /// </summary>
    public partial class WooneenhedenWindow : Window
    {
        private ObservableCollection<Wooneenheid> wooneenheden;

        public WooneenhedenWindow(VakantieparkService service)
        {
            InitializeComponent();
            wooneenheden = new ObservableCollection<Wooneenheid>();
            ListBoxWooneenheden.ItemsSource = wooneenheden;

            ComboBox_Status.ItemsSource = Enum.GetValues(typeof(HuisStatus)).Cast<HuisStatus>();
        }

        private void ButtonVoegWooneenheidToe_Click(object sender, RoutedEventArgs e)
        {
            HuisStatus huisStatus = (HuisStatus)ComboBox_Status.SelectedItem;
            Wooneenheid wooneenheid = new Wooneenheid(int.Parse(TextBoxCapaciteit.Text), TextBoxAdres.Text, huisStatus);
            wooneenheden.Add(wooneenheid);
        }

        private void ButtonSaveWooneenheid_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            Close();
        }



    }
}
