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
using EmployeeEP_BL;
using EmployeeEP_BL.Enums;
using EmployeeEP_BL.Manager;
using EmployeeEP_BL.Model;

namespace EmployeeEP_UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private EField Field;
        private Employer Employer;
        private EmployeeEPManager EmployeeEPManager;
        public MainWindow()
        {
            InitializeComponent();
        }
    }
}