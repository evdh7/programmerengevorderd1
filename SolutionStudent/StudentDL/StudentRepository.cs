using Microsoft.Data.SqlClient;
using StudentBL.Interfaces;
using StudentBL.Model;
using System.Data;

namespace StudentDL
{
    public class StudentRepository : IStudentRepository
    {
        private string connectionString = "Data Source=DESKTOP-41D2QLA\\SQLEXPRESS;Initial Catalog=studentenCursussen;Integrated Security=True;Trust Server Certificate=True"; //dit is voorlopig

        public List<Cursus> GeefCursussen(string voorwaarde)
        {
            List<Cursus> data = new List<Cursus>();
            string SQL;
            if (string.IsNullOrWhiteSpace(voorwaarde))
                SQL = "SELECT * FROM cursus";
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
                    data.Add(new Cursus((int)dr["id"], (string)dr["cursusnaam"]));
                }
                dr.Close();
            }
            return data;
        }

        public bool HeeftStudent(string naam)
        {
            string SQL = "SELECT count(*) FROM student WHERE naam=@naam";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.AddWithValue("@naam", naam);
                int n = (int)cmd.ExecuteScalar();
                if (n == 0) return false;
                return true;
            }
        }

        public void VoegCursussenToe(List<Cursus> cursussen)
        {
            string SQL = "INSERT INTO cursus(cursusnaam) output INSERTED.ID VALUES(@naam)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmd = conn.CreateCommand())
            {
                conn.Open();
                cmd.CommandText = SQL;
                cmd.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));//altijd met parameters werken

                foreach (Cursus cursus in cursussen)
                {
                    cmd.Parameters["@naam"].Value = cursus.Naam;
                    int id = (int)(cmd.ExecuteScalar());
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
                    cmd.Parameters.AddWithValue("@lokaal", DBNull.Value);

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

                    //cmd.ExecuteNonQuery();//hij gaat iets uitvoeren maar het is geen query, het is gewoon toevoegen
                    int id = (int)cmd.ExecuteScalar();
                    student.Id = id;
                    cmd.CommandText = SQLkoppel;
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@student_id", student.Id);
                    cmd.Parameters.Add(new SqlParameter("cursus_id", SqlDbType.Int));

                    foreach (Cursus cursus in student.Cursussen)
                    {
                        cmd.Parameters["cursus_id"].Value = cursus.Id;
                        cmd.ExecuteNonQuery();
                    }
                    sqlTransaction.Commit();
                }
                catch (Exception ex)
                {
                    sqlTransaction.Rollback();
                }

            }
        }

        

        

        
    }
}
