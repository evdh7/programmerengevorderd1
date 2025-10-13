using StudentBL.Beheerder;
using StudentBL.Interfaces;
using StudentBL.Model;
using StudentDL;



Console.WriteLine("Hello, World!");
Student student = new Student("Juules");
IStudentRepository repo = new StudentRepository();
StudentBeheerder studentBeheerder = new StudentBeheerder(repo);
//Klas klas = new Klas("1G");
//klas.Lokaal = "2.051";
//studentBeheerder.VoegStudentToe(student);
//studentBeheerder.VoegKlasToe(klas);
//List<Cursus> cursussen = new List<Cursus>();
//cursussen.Add(new Cursus("PG1"));
//cursussen.Add(new Cursus("DW2"));
//studentBeheerder.VoegCursussenToe(cursussen);
var data = studentBeheerder.GeefCursussen(null);

student.Cursussen = data;
student.Klas = new Klas(1, "xxxx");
studentBeheerder.VoegStudentToe(student);

Console.WriteLine();


