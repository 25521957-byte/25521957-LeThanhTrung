using Microsoft.Data.SqlClient;

namespace MyProject
{
    public static class DbConnection
    {
        private static readonly string connectionString =
            @"Server=ADMIN\SQLEXPRESS;
              Database=QuanLyNguoiDung;
              Integrated Security=True;
              Encrypt=True;
              TrustServerCertificate=True;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
