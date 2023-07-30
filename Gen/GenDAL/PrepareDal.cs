using System.Collections.Generic;
using System.Linq;
using Gen;
using Gen.ViewModels;
using Microsoft.Extensions.Configuration;
using MySqlConnector;

namespace Gencode.GenDAL
{
    public class PrepareDal
    {
        List<ColumnModel> Columns;
        private string connection_str = string.Empty;//Program.configuration.GetConnectionString("Test");
        private string sql = string.Empty;

        public PrepareDal(string sql,string connectionStr)
        {
            this.connection_str = connectionStr;
            using (var connection = new MySqlConnection(connection_str))
            {
                Columns = ColumnInfo.GetSchemaInfoFromCustomSQl(connection, sql);
            }
        }

        public string ListOfColumnName()
        {
            string cols = string.Empty;

                cols = string.Format(" {0} "
                                            ,string.Join(",",Columns.Select(x=>x.ColumnName))
                                            );
            return cols;
        }


        public string ListOfColumnNameWithParam()
        {
            string cols = string.Empty;

            cols = string.Format(" @{0} "
                                       , string.Join(",@", Columns.Select(x => x.ColumnName))
                                       );
            return cols;
        }

        public string UpdateColumnListWithParam()
        {
            string cols = string.Empty;

            cols = string.Format(" {0} "
                                       , string.Join(",", Columns.Select(x => x.UpdateListWithParam))
                                       );
            return cols;
        }

        public string ObjectParameters()
        {
            string param = string.Empty;
            param = "new { \n";

            param+= string.Join(",", Columns.Select(x => x.ObjectParameter));
            
            param += " } \n";
            return param;
        }

       
    }
}
