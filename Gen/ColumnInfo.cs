using System;
using System.Collections.Generic;
using System.Data;
using Gen.ViewModels;
using MySqlConnector;

namespace Gen;

public class ColumnInfo
{
     public static List<ColumnModel> GetSchemaInfoFromCustomSQl(string connection_string,string sql)
        {
            try
            {
                using(MySqlConnection conn = new MySqlConnection(connection_string))
                {
                    if(conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var reader = cmd.ExecuteReader(System.Data.CommandBehavior.SchemaOnly);
                        var table = reader.GetSchemaTable();
                        var nameCol = table.Columns["ColumnName"];
                        List<ColumnModel> resutls = new List<ColumnModel>();
                        foreach (DataRow row in table.Rows)
                        {
                           
                            //Console.WriteLine(row["AllowDBNull"].ToString());
                            var item = new ColumnModel();
                            item.DotNetType = row["DataType"].ToString();
                            item.DotNetType = row["DataType"].ToString();
                            item.ColumnName = row["ColumnName"].ToString();
                            item.Nullable = (row["AllowDBNull"].ToString().ToLower() == "true") ? true : false;
                            item.IsPrimaryKey = (row["IsKey"].ToString().ToLower() == "true") ? true : false;
                            resutls.Add(item);

                        }
                        return resutls;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static List<ColumnModel> GetSchemaInfoFromCustomSQl(MySqlConnection conn, string sql)
        {
            try
            {
                using (conn)
                {
                    if (conn.State != System.Data.ConnectionState.Open)
                    {
                        conn.Open();
                    }

                    using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                    {
                        var reader = cmd.ExecuteReader(System.Data.CommandBehavior.SchemaOnly);
                        var table = reader.GetSchemaTable();
                        var nameCol = table.Columns["ColumnName"];
                        List<ColumnModel> resutls = new List<ColumnModel>();
                        foreach (DataRow row in table.Rows)
                        {

                            //Console.WriteLine(row["AllowDBNull"].ToString());
                            var item = new ColumnModel();
                            item.DotNetType = row["DataType"].ToString();
                            item.DotNetType = row["DataType"].ToString();
                            item.ColumnName = row["ColumnName"].ToString();
                            item.Nullable = (row["AllowDBNull"].ToString().ToLower() == "true") ? true : false;
                            item.IsPrimaryKey = (row["IsKey"].ToString().ToLower() == "true") ? true : false;
                            resutls.Add(item);

                        }
                        return resutls;
                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
}