using Microsoft.Data.SqlClient;
using RedoLegoTest_BL;
using RedoLegoTest_BL.Interfaces;
using RedoLegoTest_BL.Model;
using System.Data;

namespace RedoLegoTestDL_SQL
{
    public class LegoRepository : ILegoRepository
    {
        private string connectionString;

        public LegoRepository(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public LegoTheme GetLegoTheme(string name)
        {
            //we doen een left join want er zijn ook themas zonder set

            string SQL = "SELECT t1.id legoThemeId, t1.name legoThemeName, t2.*FROM LegoTheme t1 Left Join LegoSet t2 on t1.id = t2.themeId
            //een data reader aanmaken
                
                
        }

        //init db
        public void WriteLegoThemes(List<LegoTheme> legoThemes)
        {
            string SQLtheme = "INSERT INTO LegoTheme(name) output INSERTED.ID VALUES(@name)";
            string SQLset = "INSERT INTO LegoSet(id, name, year, pieces, minifigs, minage, imageURL, retailPrice, themeId) output INSERTED.ID VALUES(@id, @name, @year,@pieces,@minifigs,@minage,@imageURL,@retailPrice,@themeId)";
            using (SqlConnection conn = new SqlConnection(connectionString))
            using (SqlCommand cmdTheme = conn.CreateCommand())
            using (SqlCommand cmdSet = conn.CreateCommand())
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                cmdTheme.Transaction = transaction;
                cmdSet.Transaction = transaction;
                cmdTheme.CommandText = SQLtheme;
                cmdSet.CommandText = SQLset;
                try
                {
                    cmdTheme.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdSet.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar));
                    cmdSet.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
                    cmdSet.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
                    cmdSet.Parameters.Add(new SqlParameter("@pieces", SqlDbType.Int));
                    cmdSet.Parameters.Add(new SqlParameter("@minifigs", SqlDbType.Int));
                    cmdSet.Parameters.Add(new SqlParameter("@minage", SqlDbType.Int));
                    cmdSet.Parameters.Add(new SqlParameter("@imageURL", SqlDbType.NVarChar));
                    cmdSet.Parameters.Add(new SqlParameter("@retailPrice", SqlDbType.Float)); //double in c# is float in db
                    cmdSet.Parameters.Add(new SqlParameter("@themeId", SqlDbType.Int));

                    foreach (LegoTheme legoTheme in legoThemes)
                    {
                        cmdTheme.Parameters["@name"].Value = legoTheme.Name;
                        int legoThemeId = (int)cmdTheme.ExecuteScalar();
                        cmdSet.Parameters["@themeId"].Value = legoThemeId;

                        foreach (LegoSet legoSet in legoTheme.LegoSets)
                        {
                            cmdSet.Parameters["@id"].Value = legoSet.Id;
                            cmdSet.Parameters["@name"].Value = legoSet.Name;
                            cmdSet.Parameters["@year"].Value = legoSet.Year;
                            cmdSet.Parameters["@pieces"].Value = legoSet.Pieces;
                            cmdSet.Parameters["@minifigs"].Value = legoSet.MiniFigs;
                            //cmdSet.Parameters["@minage"].Value = legoSet.MinAge ?? (object)DBNull.Value;
                            if (legoSet.MinAge.HasValue)
                                cmdSet.Parameters["@minage"].Value = legoSet.MinAge;
                            else
                                cmdSet.Parameters["@minage"].Value = DBNull.Value;
                            if (legoSet.ImageUrl == null)
                                cmdSet.Parameters["@imageURL"].Value = DBNull.Value;
                            else
                                cmdSet.Parameters["@imageURL"].Value = legoSet.ImageUrl;
                            if (legoSet.RetailPrice.HasValue)
                                cmdSet.Parameters["@retailPrice"].Value = legoSet.RetailPrice;
                            else
                                cmdSet.Parameters["@retailPrice"].Value = DBNull.Value;

                            cmdSet.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();

                }
                catch (LegoException ex) { transaction.Rollback(); }
            }
        }
    }
}