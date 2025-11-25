using BedrijvenTestPrepBL.Interfaces;
using BedrijvenTestPrepBL.Model;
using Microsoft.Data.SqlClient;
using System.Data;



namespace BedrijvenTestPrepDL_SQL
{
    public class BedrijfRepository : IBedrijfRepository
    {
        private string connectionString;

        public BedrijfRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void UploadToDatabase(List<Bedrijf> data)
        {
            string SQLbedrijf = "INSERT INTO bedrijf(name,industrie,sector,location,year) output INSERTED.ID VALUES(@name, @industrie, @sector, @location, @year)";
            string SQLpersoon = "INSERT INTO persoon(lastName, firstName, dateOfBirth, email) output INSERTED.ID VALUES(@lastName, @firstName, @dateOfBirth, @email)";
            string SQLadres = "INSERT INTO adres(gemeente, postcode, straat, huisnummer) output INSERTED.ID VALUES(@gemeente, @postcode, @straat, @huisnummer)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdBedrijf = conn.CreateCommand())
            using (SqlCommand cmdPersoon = conn.CreateCommand())
            using (SqlCommand cmdAdres = conn.CreateCommand())



            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                cmdBedrijf.Transaction = sqlTransaction;
                cmdPersoon.Transaction = sqlTransaction;
                cmdAdres.Transaction = sqlTransaction;

                cmdBedrijf.CommandText = SQLbedrijf;
                cmdPersoon.CommandText = SQLpersoon;
                cmdAdres.CommandText = SQLadres;

                cmdBedrijf.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@industrie", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@sector", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@location", SqlDbType.NVarChar));
                cmdBedrijf.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));


                cmdPersoon.Parameters.Add(new SqlParameter("@lastName", SqlDbType.NVarChar));
                cmdPersoon.Parameters.Add(new SqlParameter("@firstName", SqlDbType.NVarChar));
                cmdPersoon.Parameters.Add(new SqlParameter("@dateOfBirth", SqlDbType.DateTime));
                cmdPersoon.Parameters.Add(new SqlParameter("@email", SqlDbType.NVarChar));

                cmdAdres.Parameters.Add(new SqlParameter("@gemeente", SqlDbType.NVarChar));
                cmdAdres.Parameters.Add(new SqlParameter("@postcode", SqlDbType.Int));
                cmdAdres.Parameters.Add(new SqlParameter("@straat", SqlDbType.NVarChar));
                cmdAdres.Parameters.Add(new SqlParameter("@huisnummer", SqlDbType.NVarChar));

                int persoonId, adresId, bedrijfId;

                try
                {
                    foreach(Bedrijf bedrijf in data)
                    {
                        cmdBedrijf.Parameters["@name"].Value = bedrijf.Name;
                        cmdBedrijf.Parameters["@industrie"].Value = bedrijf.Industrie;
                        cmdBedrijf.Parameters["@sector"].Value = bedrijf.Sector;
                        cmdBedrijf.Parameters["@location"].Value = bedrijf.Location;
                        cmdBedrijf.Parameters["@year"].Value = bedrijf.Year;
                        bedrijfId = (int)cmdBedrijf.ExecuteScalar();
                        cmdBedrijf.Parameters.Clear();

                        foreach (Persoon persoon in bedrijf.Personeel)
                        {
                            Adres adres = persoon.Adres; //personeel is een lijst van persoon, elke persoon heeft 1 adres

                            cmdAdres.Parameters["@gemeente"].Value = adres.Gemeente;
                            cmdAdres.Parameters["@postcode"].Value = adres.Postcode;
                            cmdAdres.Parameters["@straat"].Value = adres.Straat;
                            cmdAdres.Parameters["@huisnummer"].Value = adres.Huisnummer;
                            adresId = (int)cmdAdres.ExecuteScalar();


                            cmdPersoon.Parameters["@lastName"].Value = persoon.LastName;
                            cmdPersoon.Parameters["@firstName"].Value = persoon.FirstName;
                            cmdPersoon.Parameters["@dateOfBirth"].Value = persoon.DateOfBirth;
                            cmdPersoon.Parameters["@email"].Value = persoon.Email;
                            cmdPersoon.Parameters["adresId"].Value = adresId;

                            persoonId = (int)cmdPersoon.ExecuteScalar();

                        }
                    }
                    sqlTransaction.Commit();

                }catch (Exception ex) { sqlTransaction.Rollback(); }


            }

        }

    }
}
