using System;
using System.Data;
using System.Data.SqlClient;

namespace PatternsUncleBob.Façade
{
    public class Db
    {
        private static SqlConnection connection;

        public static void Init()
        {
            string connectionString = "Initial Catalog=QuickyMart;Data Source=marvin;User Id=sa;Password=abc;";
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public static void Store(ProductData pd)
        {
            SqlCommand command = BuildInsertionCommand(pd);
            command.ExecuteNonQuery();
        }

        private static SqlCommand BuildInsertionCommand(ProductData pd)
        {
            string sql = "INSERT INTO Products VALUES (@sku, @name, @price)";
            SqlCommand command = new(sql, connection);
            command.Parameters.Add(new SqlParameter("@sku", pd.sku));
            command.Parameters.Add(new SqlParameter("@name", pd.name));
            command.Parameters.Add(new SqlParameter("@price", pd.price));
            return command;
        }

        public static ProductData GetProductData(string sku)
        {
            SqlCommand command = BuildProductQueryCommand(sku);
            IDataReader reader = ExecuteQueryStatement(command);
            ProductData pd = ExtractProductDataFromReader(reader);
            reader.Close();
            return pd;
        }

        private static ProductData ExtractProductDataFromReader(IDataReader reader)
        {
            ProductData pd = new();
            pd.sku = reader["sku"].ToString();
            pd.name = reader["name"].ToString();
            pd.price = Convert.ToInt32(reader["price"]);
            return pd;
        }

        private static IDataReader ExecuteQueryStatement(SqlCommand command)
        {
            IDataReader reader = command.ExecuteReader();
            reader.Read();
            return reader;
        }

        private static SqlCommand BuildProductQueryCommand(string sku)
        {
            string sql = "SELECT * FROM Products Where sku = @sku";
            SqlCommand command = new(sql, connection);
            command.Parameters.Add(new SqlParameter("@sku", sku));
            return command;
        }

        public static void DeleteProductData(string sku)
        {
            BuildProductDeleteStatement(sku).ExecuteNonQuery();
        }

        private static SqlCommand BuildProductDeleteStatement(string sku)
        {
            string sql = "DELETE from Products Where sku = @sku";
            SqlCommand command = new(sql, connection);
            command.Parameters.Add(new SqlParameter("@sku", sku));
            return command;
        }

        public static void Close()
        {
            connection.Close();
        }
    }
}