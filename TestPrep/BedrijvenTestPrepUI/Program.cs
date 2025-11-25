using Microsoft.Extensions.Configuration;
using BedrijvenTestPrepUtil;
using BedrijvenTestPrepBL.Beheer;
using System.Runtime.CompilerServices;


var config = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();


string connectionString = config.GetConnectionString("SQLserver");
string bedrijveninfo = config.GetSection("AppSettings")["bedrijveninfo"];
string logBestand = @"C:\Users\emmy\source\PG1\Errorlog - Copy.txt";

BedrijvenBeheerder bedrijvenBeheerder = new BedrijvenBeheerder
    (BedrijfRepositoryFactory.GeefRepository
    (connectionString), BedrijfBestandslezerFactory.GeefBestandslezer());
    bedrijvenBeheerder.UploadNaarDatabank(bedrijveninfo, logBestand);


