using KlantenSimulatorBL.Manager;
using KlantenSimulatorUI_WPF.Mapper;
using KlantenSimulatorUI_WPF.Model;
using KlantenSimulatorUtils;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;

namespace KlantenSimulatoUI_WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            string? connectionString = config.GetConnectionString("SQLserver");

            DataManager manager = new DataManager(KlantenSimulatorSQLFactory.GetRepository(connectionString));
        }
        private void Click_OpenSimulationWindow(object sender, RoutedEventArgs e)
        {
            List <CustomerUI> customers = new List<CustomerUI>();
            string customerName = TextBox_Email.Text;

            // Store it in a UI model
            var newCustomer = new CustomerUI(customerName);

            // Optionally keep it in a collection for binding
            customers.Add(newCustomer);

            // Later: map to domain and save to DB
            var domainCustomer = CustomerMapper.MapToDomain(newCustomer);

            DataManager manager.AddCustomer(domainCustomer);
        }
    }
}
}