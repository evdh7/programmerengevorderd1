using Microsoft.Extensions.Configuration;
using StudentBL.Beheerder;
using StudentBL.Interfaces;
using StudentBL.Model;
using StudentUtils;



static void Main()
{


    var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

    var configuration = builder.Build();

    //IStudentRepository repo = new StudentRepository();
    string connectionString = configuration.GetConnectionString("SQLserver");
    
    Student student = new Student("Janic");

    StudentBeheerder studentBeheerder = new StudentBeheerder(RepoFactory.GeefRepo(connectionString));

    //Klas klas = new Klas("1G");
    //klas.Lokaal = "C.2.051";
    //studentBeheerder.VoegStudentToe(student);

    //studentBeheerder.VoegKlasToe(klas);

    //List<Cursus> cursussen = new List<Cursus>();
    //cursussen.Add(new Cursus("PG1"));
    //cursussen.Add(new Cursus("DW2"));
    //studentBeheerder.VoegCursussenToe(cursussen);
    //var data = studentBeheerder.GeefCursussen(null);

    //student.Cursussen = data;
    //student.Klas = new Klas(1, "xxxx");
    //studentBeheerder.VoegStudentToe(student);

    var s = studentBeheerder.GeefStudent(1);
    Console.WriteLine();

}
