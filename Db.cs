using System.Data;
using Microsoft.Data.SqlClient;

namespace Apteka;

public static class Db
{
    // LocalDB by default. For a full SQL Server instance use e.g. "Server=localhost;..." or "Server=.\\SQLEXPRESS;..."
    public const string ConnStr =
        "Server=(localdb)\\MSSQLLocalDB;Database=apteka;Trusted_Connection=True;TrustServerCertificate=True;";

    static SqlCommand Make(SqlConnection c, string sql, (string Name, object Value)[] ps)
    {
        var cmd = new SqlCommand(sql, c);
        foreach (var p in ps)
            cmd.Parameters.AddWithValue(p.Name, p.Value ?? DBNull.Value);
        return cmd;
    }

    public static DataTable Query(string sql, params (string Name, object Value)[] ps)
    {
        using var c = new SqlConnection(ConnStr);
        using var cmd = Make(c, sql, ps);
        var dt = new DataTable();
        new SqlDataAdapter(cmd).Fill(dt);
        return dt;
    }

    public static int Exec(string sql, params (string Name, object Value)[] ps)
    {
        using var c = new SqlConnection(ConnStr);
        using var cmd = Make(c, sql, ps);
        c.Open();
        return cmd.ExecuteNonQuery();
    }

    public static object Scalar(string sql, params (string Name, object Value)[] ps)
    {
        using var c = new SqlConnection(ConnStr);
        using var cmd = Make(c, sql, ps);
        c.Open();
        return cmd.ExecuteScalar();
    }
}
