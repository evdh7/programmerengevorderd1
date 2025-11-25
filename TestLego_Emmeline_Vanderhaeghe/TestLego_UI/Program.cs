using Microsoft.Extensions.Configuration;
using TestLego_BL.Beheer;
using TestLego_BL.Interfaces;


var builder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var configuration = builder.Build();

string connectionString = configuration.GetConnectionString("SQLserver");
string legoSets = configuration.GetSection("AppSettings")["lego_sets"];

ILegoRepository legoRepository = new LegoRepository(connectionString);
ILegoFileReader FileReader = new FileReader();

LegoBeheerder legoManager = new LegoBeheerder(legoRepository, legoFileReader);

legoManager.UploadNaarDatabank(legoSets);

Console.WriteLine("Hello, World! Het is Emmeline Vanderhaeghe hier.");

//var res = legoManager.GetLegoTheme("Vikings");
//Console.WriteLine(res);
