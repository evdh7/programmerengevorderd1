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
    //@"C:\Users\emmy\source\PG1\bedrijvenbelgie_18092025.txt",
    @"C:\Users\emmy\source\PG1\bedrijvenbelgie_LINQ_v4.txt",
    @"C:\Users\emmy\source\PG1\Errorlog.txt");


//string gemeente = "Gent";

BedrijvenDataBeheerder db = new BedrijvenDataBeheerder(data);

//var res = db.GeefPersoneelBedrijf("VLM Airlines");
//var resgemeente = db.GeefPersoneelGemeente("Gent");

//foreach (var p in res)
//{
//    Console.WriteLine(p);
//}

//foreach (var pers in resgemeente)
//{
//    Console.WriteLine(pers);
//}

var helper = new QueryHelper(db);

//var geordend = helper.GeefBedrijvenGeordend();

//foreach (var bedrijf in geordend)
//{
//    Console.WriteLine(bedrijf);
//}

//var bedrijvenEnJaar = helper.GeefBedrijvenMetJaar();

//foreach (var item in bedrijvenEnJaar)
//{
//    Console.WriteLine(item);
//}

//var bedrijvenEnAantalWN = helper.GeefBedrijvenMetAantal();

//foreach (var item in bedrijvenEnAantalWN)
//{
//    Console.WriteLine(item);
//}

//var aantalPersoneelPerGemeente = helper.GeefAantalPersoneelPerGemeente();

//foreach (var item in aantalPersoneelPerGemeente)
//{
//    Console.WriteLine(item);
//}
//Console.WriteLine("Geef een gemeente");
//string gemeente = Console.ReadLine();

//var personeelInGemeente = helper.GeefPersoneelPerGemeente(gemeente);

//foreach (var item in personeelInGemeente)
//{
//    Console.WriteLine(item);
//}

var sectorenBedrijven = helper.GeefSectorenAantalBedrijven();
foreach (var item in sectorenBedrijven)
{
    Console.WriteLine(item);
}

var namenBedrijvenPerIndustrie = helper.GeefNamenBedrijvenPerIndustrie();
foreach (var item in namenBedrijvenPerIndustrie)
{
    Console.WriteLine(item);
}

