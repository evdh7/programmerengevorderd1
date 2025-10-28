using LegoTwo_BL.Interfaces;
using LegoTwo_BL.Model;
using Microsoft.Data.SqlClient;
using System.Data;

namespace LegoTwo_DL
{
    public class LegoRepository : ILegoTwoRepository
    {
        private string connectionString;

        public LegoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //public LegoTheme GetLegoTheme(string name)
        //{

        //}

        //init db
        public void WriteLegoThemes(List<LegoTheme> legoThemes)
        {
            string SQLlegoTheme = "INSERT INTO LegoTheme(name) output INSERTED.ID VALUES(@name)";
            string SQLlegoSet = "INSERT INTO LegoSet(id, name, year, pieces, minifigs, minage, imageURL, retailPrice, themeId) output INSERTED.ID VALUES(@id, @name, @year,@pieces,@minifigs,@minage,@imageURL,@retailPrice,@themeId)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdLegoTheme = conn.CreateCommand())
            using (SqlCommand cmdLegoSet = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction sqlTransaction = conn.BeginTransaction();
                cmdLegoTheme.Transaction = sqlTransaction;
                cmdLegoSet.Transaction = sqlTransaction;
                cmdLegoTheme.CommandText = SQLlegoTheme;
                cmdLegoSet.CommandText = SQLlegoSet;

                cmdLegoTheme.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                cmdLegoSet.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar));
                cmdLegoSet.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                cmdLegoSet.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                cmdLegoSet.Parameters.Add(new SqlParameter("@pieces", SqlDbType.Int));
                cmdLegoSet.Parameters.Add(new SqlParameter("@minifigs", SqlDbType.Int));
                cmdLegoSet.Parameters.Add(new SqlParameter("@minage", SqlDbType.Int));
                cmdLegoSet.Parameters.Add(new SqlParameter("@imageURL", SqlDbType.NVarChar));
                cmdLegoSet.Parameters.Add(new SqlParameter("@retailPrice", SqlDbType.Float));
                cmdLegoSet.Parameters.Add(new SqlParameter("@themeId", SqlDbType.Int));
                int legoThemeId, legoSetId;
                try
                {
                    foreach (LegoTheme legoTheme in legoThemes)
                    {
                        cmdLegoTheme.Parameters["@name"].Value = legoTheme.Name;
                        legoThemeId = (int)cmdLegoTheme.ExecuteScalar();
                        cmdLegoSet.Parameters["@themeId"].Value = legoThemeId;

                        foreach (LegoSet legoSet in legoTheme.LegoSets)
                        {
                            cmdLegoSet.Parameters["@id"].Value = legoSet.Id;
                            cmdLegoSet.Parameters["@name"].Value = legoSet.Name;
                            cmdLegoSet.Parameters["@year"].Value = legoSet.Year;
                            cmdLegoSet.Parameters["@pieces"].Value = legoSet.Pieces;
                            cmdLegoSet.Parameters["@minifigs"].Value = legoSet.MiniFigs;
                            cmdLegoSet.Parameters["@minage"].Value = legoSet.MinAge ?? (object)DBNull.Value;
                            cmdLegoSet.Parameters["@imageURL"].Value = legoSet.ImageUrl;
                            cmdLegoSet.Parameters["@retailPrice"].Value= legoSet.RetailPrice ?? (object)DBNull.Value;

                            cmdLegoSet.ExecuteNonQuery();
                        }

                    }
                    sqlTransaction.Commit();

                }
                catch (Exception ex) { sqlTransaction.Rollback(); }
                
              

            }
        }
    }
}