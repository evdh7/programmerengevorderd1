using Microsoft.Data.SqlClient;
using ProvinciesBL.Interfaces;
using ProvinciesBL.Model;
using System.Data;

namespace ProvinciesDL_SQL
{
    public class ProvincieRepository : IProvincieRepository
    {
        private string connectionString;

        public ProvincieRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void UploadToDatabase(List<Provincie> data)
        {

            string SQLprovincie = "INSERT INTO provincie(naam) output INSERTED.ID VALUES(@naam)";
            string SQLgemeente = "INSERT INTO gemeente(naam,provincieid) output INSERTED.ID VALUES(@naam, @provincieid)";
            string SQLstraat = "INSERT INTO straat(naam,gemeenteid) VALUES(@naam, @gemeenteid)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdProvincie = conn.CreateCommand())
            using (SqlCommand cmdGemeente = conn.CreateCommand())
            using (SqlCommand cmdStraat = conn.CreateCommand())

            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                cmdProvincie.Transaction = sqlTransaction;
                cmdGemeente.Transaction = sqlTransaction;
                cmdStraat.Transaction = sqlTransaction;
                cmdProvincie.CommandText = SQLprovincie;
                cmdGemeente.CommandText = SQLgemeente;
                cmdStraat.CommandText = SQLstraat;

                cmdProvincie.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                cmdGemeente.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                cmdGemeente.Parameters.Add(new SqlParameter("@provincieid", SqlDbType.Int));
                cmdStraat.Parameters.Add(new SqlParameter("@naam", SqlDbType.NVarChar));
                cmdStraat.Parameters.Add(new SqlParameter("@gemeenteid", SqlDbType.Int));
                int provincieId, gemeenteId;
                try
                {
                    foreach (Provincie provincie in data)
                    {
                        cmdProvincie.Parameters["@naam"].Value = provincie.Naam;
                        provincieId = (int)cmdProvincie.ExecuteScalar();
                        cmdGemeente.Parameters["@provincieid"].Value = provincieId;

                        foreach (Gemeente gemeente in provincie.Gemeentes)
                        {
                            cmdGemeente.Parameters["@naam"].Value = gemeente.Naam;
                            gemeenteId = (int)cmdGemeente.ExecuteScalar();
                            cmdStraat.Parameters["@gemeenteid"].Value = gemeenteId;
                            foreach (Straat straat in gemeente.Straten)
                            {
                                cmdStraat.Parameters["@naam"].Value = straat.Naam;
                                cmdStraat.ExecuteNonQuery();
                            }
                        }
                    }
                    sqlTransaction.Commit();
                }
                catch (Exception ex) { sqlTransaction.Rollback(); }
            }
        }
    }
}
