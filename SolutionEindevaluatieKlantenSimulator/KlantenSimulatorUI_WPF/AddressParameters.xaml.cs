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
    /// Interaction logic for AddressParameters.xaml
    /// </summary>
    public partial class AddressParameters : Window
    {
        public int MaxHousenumber { get; private set; }
        public int PercentageLetters { get; private set; }

        public AddressParameters()
        {
            InitializeComponent();
        }

        private void ButtonAddAddressParameters_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(TextHouseNumber.Text, out int maxHousenumber))
            {
                MaxHousenumber = maxHousenumber;

            }
            if (int.TryParse(PercentageNumber.Text, out int percentage))
            {
                PercentageLetters = percentage;
            }
     
            DialogResult = true;
            Close();
        }

        private void CheckBoxYes_Checked(object sender, RoutedEventArgs e)
        {
            Percentage.Visibility = Visibility.Visible;
            PercentageNumber.Visibility = Visibility.Visible;
        }
    }
}
