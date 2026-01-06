using KlantenSimulatorBL.Manager;
using KlantenSimulatorBL.Model;
using KlantenSimulatorUI_WPF;
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
        private readonly SimulationService _service;
        public MainWindow()
        {

            InitializeComponent();
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var config = builder.Build();

            string? connectionString = config.GetConnectionString("SQLserver");

            var repo = KlantenSimulatorSQLFactory.GetRepository(connectionString);
            _service = new SimulationService(repo);
        }
        private void OpenSimulationWindow_Click(object sender, RoutedEventArgs e)
        {
            List<Client> clientNames = [];
            string clientName = TextBox_Name.Text;

            Client client = new            (
                Name = clientName
            );

            clientNames.Add(client);

            //DataManager manager.AddCustomer(domainCustomer);

            ParametersWindow w = new(_service, client);
            w.ShowDialog();

        }
    }
}
