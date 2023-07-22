
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SchemaInfomation.Models;
using SchemaInfomation;
using MySqlConnector;

namespace Gencode.GenDAL
{
    public class PrepareDal
    {
        List<ColumnModel> Columns;
        private string connection_str = System.Configuration.ConfigurationManager.
                                        ConnectionStrings["Test"].ConnectionString;
        private string sql = string.Empty;

        public PrepareDal(string sql)
        {
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

        #region UI

      
        public string LableForColumn
        {
            get
            {
                string lbls = string.Empty;
                lbls = string.Join("    \r\n", Columns.Select(x => x.LableForColumn));
                return lbls;
            }
        }

        public string TextBoxForColumn
        {
            get
            {
                string lbls = string.Empty;
                lbls = string.Join("    \r\n", Columns.Select(x => x.TextBoxForColumn));
                return lbls;
            }
        }


        public string TextBoxInStant
        {
            get
            {
                string lbls = string.Empty;
                lbls = string.Join("    \r\n", Columns.Select(x => x.TextBoxInStant));
                return lbls;
            }
        }

        public string LableInStant
        {
            get
            {
                string lbls = string.Empty;
                lbls = string.Join("    \r\n", Columns.Select(x => x.LableInStant));
                return lbls;
            }
        }
        /*
        public string ConfigTexboxAndLable
        {
            get
            {
                string lbls = string.Empty;
                lbls = string.Join("    \r\n", Columns.Select(x => x.ConfigTexboxAndLable));
                return lbls;
            }
        }

        public string AddTextBoxToGroupBox(string groupboxname)
        {
            return string.Join("   \r\n", Columns.Select(x => x.AddTextBoxToGroupBox(groupboxname)));
        }

        public string AddLableToGroupBox(string groupboxname)
        {
            return string.Join("   \r\n", Columns.Select(x => x.AddLableToGroupBox(groupboxname)));
        }
        */

        public string BindingTextBox(string viewModelName)
        {
            return string.Join("    \r\n", Columns.Select(x => x.TextBoxBinding(viewModelName)));
        }
        #endregion
    }
}
