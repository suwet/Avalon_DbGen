using System;
using System.Collections.Generic;
using System.Linq;
using Gen;
using Gen.ViewModels;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Gencode.GenModel
{
    public class PreparePocoModel
    {
        List<ColumnModel> Columns;

        private string connection_str = string.Empty;
                                            /*Program.configuration.GetConnectionString("Test");*/
        
        private string sql = string.Empty;
        public PreparePocoModel(string sql,string connectionStr)
        {
            this.connection_str = connectionStr;

            using (var connection = new MySqlConnection(connection_str))
            {
                Columns = ColumnInfo.GetSchemaInfoFromCustomSQl(connection, sql);
            }
        }
        public string GetProperty
        {
            get
            {
                string prop = string.Empty;

                prop = string.Join("    \r\n", Columns.Select(t => t.PropertyCode));

                return prop;
            }
        }

        public string GetMvvmProperty
        {
            get
            {
                string prop = string.Empty;

                prop = string.Join("    \r\n", Columns.Select(t => t.MvvmPropertyCode));

                return prop;
            }
        }


        public string GetPrivateFeild
        {
            get
            {
                return string.Join("    \r\n", Columns.Select(t => t.PrivateFeild));
            }
        }

         #region UI

      
        public string GetTextColumnOfDataGrid
        {
            get
            {
                string grid_text_column_tag = string.Empty;
                grid_text_column_tag = string.Join("    \r\n", Columns.Select(x => x.GetTextColumnOfDataGrid));
                return grid_text_column_tag;
            }
        }

        public string GetOneColRowDefinitions
        {
            get
            {
                string result = string.Join(" , ",Columns.Select(x=>x.GetOneColRowDefinitions));
                return result;
            }
        }

        public string GetOneColWithLeftRowData
        {
            get
            {
                string result = string.Join(" \r\n",Columns.Select(x=>x.GetOneColWithLeftRowData()));
                return result;
            }
        }

        public string GetTwoColRowDefinitions
        {
            get
            {
                string row_def=string.Empty;

                int num_of_row = (int)Math.Ceiling(((double)Columns.Count/2)+0.1);
                for(int i=0;i<num_of_row;i++)
                {
                    row_def += "auto,";
                }

                return row_def.Remove(row_def.Length-1,1);
            }
        }

        public string GetTwoColWithLeftRowData
        {
            get
            {
                string result = string.Empty;
               
                return result;
            }
        }
        #endregion
    }
}
