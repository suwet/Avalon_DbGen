
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;
using SchemaInfomation;
using SchemaInfomation.Models;

namespace Gencode.GenModel
{
    public class PreparePocoModel
    {
        List<ColumnModel> Columns;
        private string connection_str =
                                        System.Configuration.ConfigurationManager.
                                        ConnectionStrings["Test"].ConnectionString;
        //private string connection_str = "Data Source=suwet_phr2\\SQL2014;Initial Catalog = MyTestdb; Persist Security Info=True;User ID = sa; Password=1234qwer";
        private string sql = string.Empty;
        public PreparePocoModel(string sql)
        {
            //sql = @"
            //                 SELECT SaleOrderNo
            //                  ,CustomerNo
            //                  ,MixdesignNo
            //                  ,Amount
            //                  ,LoadNumber
            //                  ,ShipToNo
            //                  ,Load_Status
            //                  ,50 as UserCount
            //                  ,(3.14*6) as Area
            //                    FROM[MyTestDb].[dbo].[Loads]
            //                ";

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
