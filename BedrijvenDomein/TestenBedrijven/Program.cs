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
    @"C:\Users\emmy\PG1\bedrijvenbelgie_18092025.txt",
    @"C:\Users\emmy\PG1\Errorlog.txt");


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

//hier moet nog iets komen

//var.data=Bmi1.LeesBestand(PaddingMode van ding)

//Adres adres1 = null;

//try
//{
//    adres1 = new Adres("Ge", null, "a45", 95000);
//}
//catch (BedrijfException ex)
//{
//    Console.WriteLine(ex.Message);
//    foreach (var e in ex.Errors) Console.WriteLine(e);
//}

//Personeel personeel1 = null;

//try
//{
//    personeel1 = new Personeel(1254, null, "Smith", new DateTime(1993,11,09), adres1);
//}

//catch (BedrijfException ex)
//{
//    Console.WriteLine(ex.Message);
//    foreach (var e in ex.Errors) Console.WriteLine(e);
//}