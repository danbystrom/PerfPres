using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JimmyPresentation
{
    public static class SqlPersistance
    {
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

        public static void Save(TestStruct[] items)
        {
            // Open a sourceConnection to the AdventureWorks database. 
            using (var conn = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=PerfPres;Integrated Security=SSPI;"))
            {
                conn.Open();

                //  Delete all from the destination table.         
                var commandDelete = new SqlCommand {Connection = conn, CommandText = "DELETE FROM dbo.TestStructTable"};
                commandDelete.ExecuteNonQuery();

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

        public static TestStruct[] Load(int size)
        {
            // Open a sourceConnection to the AdventureWorks database. 
            using (var conn = new SqlConnection(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=PerfPres;Integrated Security=SSPI;"))
            {
                conn.Open();
                using (var cmd = new SqlCommand())
                {
                    var result = new TestStruct[size];

                    cmd.Connection = conn;
                    cmd.CommandText = "select id,a,b,c,d,e,f,g,h FROM dbo.TestStructTable";

                    using (var reader = cmd.ExecuteReader())
                        while (reader.Read())
                            result[reader.GetInt32(0)] = new TestStruct
                            {
                                A = reader.GetDecimal(1),
                                B = reader.GetDecimal(2),
                                C = reader.GetDecimal(3),
                                D = reader.GetDecimal(4),
                                E = reader.GetDecimal(5),
                                F = reader.GetDecimal(6),
                                G = reader.GetDecimal(7),
                                H = reader.GetDecimal(8),
                            };
                    return result;
                }
            }

        }

    }

}