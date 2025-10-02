using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BedrijvenDataLaag;
using BedrijvenDomein;

//hier de dingen toevoegen

BedrijvenBestandslezer b1 = new BedrijvenBestandslezer();
var data = b1.LeesBestand(
    @"C:\Users\emmy\source\PG1\bedrijvenbelgie_18092025.txt",
    @"C:\Users\emmy\source\PG1\Errorlog.txt");


//string gemeente = "Gent";

BedrijvenDataBeheerder db = new BedrijvenDataBeheerder(data);

var res = db.GeefPersoneelBedrijf("VLM Airlines");
var resgemeente = db.GeefPersoneelGemeente("Gent");

foreach (var p in res)
{
    Console.WriteLine(p);
}

foreach (var pers in resgemeente)
{
    Console.WriteLine(pers);
}

