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
    }
}
