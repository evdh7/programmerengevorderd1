using System.Linq;
using System.IO;
using System.Collections.Generic;
using AdressenOef;

class Program
{
    static void Main(string[] args)
    {
        AdresInfo ai= new AdresInfo(@"C:\Users\emmy\source\PG1\adresInfo.txt");
        ai.ReadData();

        //foreach (string p in ai.GetProvincies())
        //{
        //    Console.WriteLine($"Provincies: " + p);
        //}

        //foreach (string p in ai.GetStraatnamen("Gent"))
        //{
        //    Console.WriteLine($"Provincies: " + p);
        //}

        //Console.WriteLine(ai.LangsteStraatnaam());

        Console.WriteLine(ai.maxStraatnaam());

        //Console.ReadLine();




    }

}
