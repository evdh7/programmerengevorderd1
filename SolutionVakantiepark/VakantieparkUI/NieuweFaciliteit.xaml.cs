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
using VakantieparkBL.Interfaces;
using VakantieparkBL.Model;
using VakantieparkBL.Services;
using VakantieparkUtils;

namespace VakantieparkUI
{
    /// <summary>
    /// Interaction logic for NieuweFaciliteit.xaml
    /// </summary>
    public partial class NieuweFaciliteit : Window
    {


        public Faciliteit faciliteit;
        public NieuweFaciliteit()
        {
            InitializeComponent();
            IVakantieparkRepository repo = VakantieparkRepositoryFactory.GetVakantieRepository();
        }
        private void ButtonVoegToe_Click(object sender, RoutedEventArgs e)
        {
            Faciliteit faciliteit = new Faciliteit(TextBoxFaciliteit.Text);
            DialogResult = true;
            Close();
        }
    }
}

