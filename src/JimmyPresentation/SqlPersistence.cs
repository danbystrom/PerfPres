using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JimmyPresentation
{
    public static class SqlPersistence
    {
        private const string ConnectionString =
            //@"Data Source=(localdb)\Projects;Initial Catalog=PerfPres;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False";
            @"Data Source=localhost\SQLEXPRESS;Initial Catalog=PerfPres;Integrated Security=SSPI;";

        public static DataTable AsDataTable<T>(this IEnumerable<T> data)
        {
            var fields = typeof (T).GetFields();
            var table = new DataTable();
            table.Columns.Add("id", typeof (int));
            foreach (var prop in fields)
                table.Columns.Add(prop.Name, prop.FieldType);
            var i = 0;
            foreach (var item in data)
            {
                var row = table.NewRow();
                row["id"] = i++;
                foreach (var prop in fields)
                    row[prop.Name] = prop.GetValue(item);
                table.Rows.Add(row);
            }
            return table;
        }

        public static void CleanUp()
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var commandDelete = new SqlCommand {Connection = conn, CommandText = "DELETE FROM dbo.TestStructTable"};
                commandDelete.ExecuteNonQuery();
            }
        }

        public static void Save(LargeValueObjectAsStruct[] items)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();

                var tx = conn.BeginTransaction();
                using (var bulkCopy = new SqlBulkCopy(conn, SqlBulkCopyOptions.Default, tx))
                {
                    bulkCopy.BatchSize = 100;
                    bulkCopy.DestinationTableName = "dbo.TestStructTable";
                    bulkCopy.WriteToServer(items.AsDataTable());
                }
                tx.Commit();
            }
        }

        public static LargeValueObjectAsStruct[] Load(int size)
        {
            using (var conn = new SqlConnection(ConnectionString))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    var result = new LargeValueObjectAsStruct[size];

                    cmd.Connection = conn;
                    cmd.CommandText = "select id,a,b,c,d,e,f,g,h FROM dbo.TestStructTable";

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                            result[reader.GetInt32(0)] = new LargeValueObjectAsStruct(
                                reader.GetDouble(1),
                                reader.GetDouble(2),
                                reader.GetDouble(3),
                                reader.GetDouble(4),
                                reader.GetDouble(5),
                                reader.GetDouble(6),
                                reader.GetDouble(7),
                                reader.GetDouble(8));
                    return result;
                }
            }

        }

    }

}