using TestLego_BL.Interfaces;
using TestLego_BL;
using TestLego_BL.Beheer;
using System.Data;
using Microsoft.Data.SqlClient;

public class LegoRepository : ILegoRepository
{
    private string connectionString;
    public LegoRepository(string connectionString)
    {
        this.connectionString = connectionString;
    }
    public LegoTheme GetLegoTheme(string themeName)
    {
        LegoTheme theme = new(themeName);
        return theme;
    }
}
    //init db
//    public void WriteLegoThemes(List<LegoTheme> legoThemes)
//    {
//        string SQLLegoTheme = "INSERT INTO LegoTheme(name) output INSERTED.ID VALUES(@name)";
//        string SQLLegoSet = "INSERT INTO LegoSet(name) output INSERTED.ID VALUES(@name)";
//        using (SqlConnection conn = new SqlConnection(connectionString))
//        using (SqlCommand cmdLegoTheme = conn.CreateCommand())
//        using (SqlCommand cmdLegoSet = conn.CreateCommand())
//        {
//            conn.Open();
//            SqlTransaction sqlTransaction = conn.BeginTransaction();
//            cmdLegoTheme.Transaction = sqlTransaction;
//            cmdLegoSet.Transaction = sqlTransaction;
//            cmdLegoTheme.CommandText = SQLLegoTheme;
//            cmdLegoSet.CommandText = SQLLegoSet;

//            cmdLegoTheme.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar));
//            cmdLegoSet.Parameters.Add(new SqlParameter("@year", SqlDbType.Int));
//            cmdLegoSet.Parameters.Add(new SqlParameter("@pieces", SqlDbType.Int));
//            cmdLegoSet.Parameters.Add(new SqlParameter("@minifigs", SqlDbType.Int));
//            cmdLegoSet.Parameters.Add(new SqlParameter("@minage", SqlDbType.Int));
//            cmdLegoSet.Parameters.Add(new SqlParameter("@imageURL", SqlDbType.NVarChar));
//            cmdLegoSet.Parameters.Add(new SqlParameter("@retailPrice", SqlDbType.Float));
//            cmdLegoSet.Parameters.Add(new SqlParameter("@themeId", SqlDbType.Int));
//            int legoThemeId, legoSetId;
//            try
//            {
//                foreach (LegoTheme legoTheme in legoThemes)
//                    cmdLegoTheme.Parameters["@name"].Value = legoTheme.Name;
//                    legoThemeId = (int)cmdLegoTheme.ExecuteScalar();
//                    cmdLegoSet.Parameters["@themeId"].Value = legoThemeId;

//                foreach (LegoSets legoSet in legoTheme.LegoSet)
//                    cmdLegoSet.Parameters["@name"].Value = legoSet.Name;





//            }
//        }


//    }


//}