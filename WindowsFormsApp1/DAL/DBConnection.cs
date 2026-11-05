using System.Data.SqlClient;

namespace WindowsFormsApp1.DAL
{
    public class DBConnection
    {
        public static string connectionString =
            @"Data Source=ADMIN-PC\SQLEXPRESS;
              Initial Catalog=SalesDB;
              Integrated Security=True;
              TrustServerCertificate=True";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}