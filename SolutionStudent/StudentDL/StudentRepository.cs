using Microsoft.Data.SqlClient;
using StudentBL.Interfaces;
using StudentBL.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDL
{
    public class StudentRepository : IStudentRepository
    {
        private string connectionString;
        public StudentRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }


        public List<Cursus> GeefCursussen(string voorwaarde)
        {
            List<Cursus> data = new List<Cursus>();
            string SQL;
            if (string.IsNullOrWhiteSpace(voorwaarde)) //als voorwaarde null is
                SQL = "SELECT * FROM cursus";//zonder voorwaarde is alles weergeven en met is enkel de voorwaarde (dus enkel de opgegeven cursus)
            else SQL = "SELECT * FROM cursus WHERE cursusnaam like @voorwaarde";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                if (!string.IsNullOrWhiteSpace(voorwaarde))
                    cmd.Parameters.AddWithValue("@voorwaarde", $"%{voorwaarde}%");
                IDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    data.Add(new Cursus((int)dr["id"], (string)dr["cursusnaam"]));//mag ook met index, maar misschien duidelijker als je kolomnaam gebruikt
                }
                dr.Close();
            }
            return data;
        }

        public Student GeefStudent(int id)
        {
            string SQL = "SELECT t1.*, t2.klasnaam klasnaam, t2.lokaal, t4.id cursusid, t4.cursusnaam FROM student t1 LEFT JOIN klas t2 ON t1.klasid = t2.id     LEFT JOIN student_cursus t3 ON t3.student_id = t1.id LEFT JOIN cursus t4 ON t3.cursus_id = t4.id WHERE t1.id = @id";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@id",id );// parameter aanmaken en direct invullen
                using (IDataReader dr = cmd.ExecuteReader())
                {
                    dr.Read();
                    Student student = new Student(id, (string)dr["naam"]);
                    
                    if (!dr.IsDBNull(dr.GetOrdinal("klasId")))
                    {
                        Klas klas = new Klas((int)dr["klasId"], (string)dr["klasnaam"]);
                        if (!dr.IsDBNull(dr.GetOrdinal("lokaal")))
                        {
                            klas.Lokaal = (string)dr["lokaal"];
                        }
                        student.Klas = klas;
                    }
                    if (!dr.IsDBNull(dr.GetOrdinal("cursus_id")))
                    {
                        student.Cursussen.Add(new Cursus((int)dr["cursus_id"], (string)dr["cursusnaam"]));
                    }
                    while (dr.Read())
                    {
                        student.Cursussen.Add(new Cursus((int)dr["cursus_id"], (string)dr["cursusnaam"]));
                    }
                    return student;
                }
            }
        }

        public bool HeeftStudent(string naam)
        {
            string SQL = "SELECT count(*) FROM student WHERE naam=@naam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@naam", naam);//je maakt parameter aanmaken en direct invullen
                int n = (int)cmd.ExecuteScalar();
                if (n == 0) return false;
                return true;
            }
        }

        public void VoegCursussenToe(List<Cursus> cursussen)
        {
            string SQL = "INSERT INTO cursus(cursusnaam) output INSERTED.ID VALUES(@naam)";//schrijf je sql statement
            using (SqlConnection conn = new SqlConnection(connectionString)) // //maak connectie
            using (SqlCommand cmd = conn.CreateCommand())//schrijf een opdracht
            {
                conn.Open(); //open de connectie
                cmd.CommandText = SQL; //voer de query uit (dat je in de string hierboven schreef)
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));//maak parameters aan - altijd met parameters werken

                foreach (Cursus cursus in cursussen)
                {
                    cmd.Parameters["@naam"].Value = cursus.Naam;
                    int id = (int)(cmd.ExecuteScalar()); //1rij 1 kolom
                    cursus.Id = id;
                }
            }
        }

        public void VoegKlasToe(Klas klas)
        {
            string SQL = "INSERT INTO klas(klasnaam, lokaal) output INSERTED.id VALUES(@klasnaam, @lokaal)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@klasnaam", klas.Naam);//altijd met parameters werken

                if (klas.Lokaal == null) //is het in C# null, dan moet je expliciet null waarde doorgeven zoals in de if structuur hieronder
                {
                    cmd.Parameters.AddWithValue("@lokaal", DBNull.Value); //het moet niet null schrijven als in c# maar null van de database

                }
                else
                {
                    cmd.Parameters.AddWithValue("@lokaal", klas.Lokaal);
                }
                //cmd.ExecuteNonQuery();//hij gaat iets uitvoeren maar het is geen query, het is gewoon toevoegen
                int id = (int)cmd.ExecuteScalar();
                klas.Id = id;
            }
        }
        public void VoegStudentToe(Student student)
        {
            string SQL = "INSERT INTO student(naam,klasId) output INSERTED.ID VALUES(@naam,@klasid)";
            string SQLkoppel = "INSERT INTO student_cursus(student_id, cursus_id) VALUES (@student_id,@cursus_id)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())

            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();

                try
                {
                    cmd.Transaction = sqlTransaction;
                    cmd.CommandText = SQL;
                    cmd.Parameters.AddWithValue("@naam", student.Naam);//altijd met parameters werken
                    cmd.Parameters.AddWithValue("@klasId", student.Klas.Id);//altijd met parameters werken

                    //cmd.ExecuteNonQuery();//hij gaat iets ui tvoeren maar het is geen query, het is gewoon toevoegen
                    int id = (int)cmd.ExecuteScalar();//1 rij 1 kolom wordt teruggestuurd
                    student.Id = id;

                    cmd.CommandText = SQLkoppel; //welke cursus moet student volgen (cursussen bestaan al), dit is de statement hierboven
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@student_id", student.Id);
                    cmd.Parameters.Add(new SqlParameter("cursus_id", SqlDbType.Int)); //parameter aanmaken en niet meteen invullen met een waarde

                    foreach (Cursus cursus in student.Cursussen)
                    {
                        cmd.Parameters["cursus_id"].Value = cursus.Id;
                        cmd.ExecuteNonQuery();//iets uitvoeren dat geen waarden terug geeft
                    }
                    sqlTransaction.Commit();//zijn alle acties uitgevoerd? commit bevestigd alles en slaat op
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback(); //ga terug naar de situatie voor we zijn begonnen
                }

            }
        }






    }
}
