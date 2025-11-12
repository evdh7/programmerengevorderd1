using RedoLegoTest_BL.Model;
using RedoLegoTest_BL.Manager;
using RedoLegoTestDL_File;
using RedoLegoTestDL_SQL;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RedoLegoTest_UI
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            ////for the purposes of the test we're not using factories or appsettings here. paths are hardcoded, and the console can access both BL and DL
            //string path = "C:\\Users\\emmy\\source\\repos\\lego_sets.csv";
            //FileReader fr = new FileReader();
            //var data = fr.ReadFile(path);

            //var q1 = data.OrderByDescending(x => x.LegoSets.Count()).Select(x => x.Name).Take(3).ToList();

            //foreach (var x in q1)
            //{
            //    Console.WriteLine(x);
            //}

            //var q2 = data.SelectMany(x => x.LegoSets).Where(x => x.MiniFigs >= 25 && x.Pieces > 100);
            //foreach (var x in q2) { Console.WriteLine(x); }

            //var q3 = data.OrderByDescending(x => x.Name.Length).First();
            //Console.WriteLine(q3.Name);
            string connectionString = "Data Source=DESKTOP-41D2QLA\\SQLEXPRESS;Initial Catalog=testLego;Integrated Security=True;Trust Server Certificate=True";
            string path = "C:\\Users\\emmy\\source\\repos\\lego_sets.csv";

            LegoManager legoManager = new LegoManager(new FileReader(), new LegoRepository(connectionString));
            legoManager.DBUpload(path);
        }
    }
}
