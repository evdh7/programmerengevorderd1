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
using VakantieparkBL.Interfaces;
using VakantieparkBL.Services;
using VakantieparkUtils;

namespace VakantieparkUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            IVakantieparkRepository repo = VakantieparkRepositoryFactory.GetVakantieRepository();
            var service = new VakantieparkService(repo);
            DataGridVakantieparken.ItemsSource = service.GeefVakantieparken();
        }
    }
}