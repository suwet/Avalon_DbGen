using System;
using System.Data;
using System.Data.Common;

namespace Gen.ViewModels;

 public class ColumnModel
    {
		public string ColumnName;
		public string DotNetType;
		public bool Nullable;
		public bool IsIdentity;
		public bool IsPrimaryKey;

		public int Row_Index;

        public ColumnModel()
        {

        }
		public ColumnModel(DataRow row)
		{
			ColumnName = row["ColumnName"].ToString();
			DotNetType = row["DataType"].ToString();
			Nullable = ((bool)row["AllowDBNull"]);
			IsIdentity = ((bool)row["IsAutoIncrement"]);
			IsPrimaryKey = ((bool)row["IsKey"]);
		}

		public ColumnModel(DbDataReader reader)
		{
			ColumnName = reader["COLUMN_NAME"].ToString();
			DotNetType = reader["DOT_NET_TYPE"].ToString();
			Nullable = ((string)reader["IS_NULLABLE"]) == "YES";
			IsIdentity = ((long)reader["IS_IDENTITY"]) == 1;
			IsPrimaryKey = ((long)reader["IS_PK"]) == 1;
			/*
			IsIdentity = ((int)reader["IS_IDENTITY"]) == 1;
			IsPrimaryKey = ((int)reader["IS_PK"]) == 1;
			*/
		}

		public string PropertyCode
		{
			get
			{
				string v = string.Empty;
				if (Nullable)
				{
					v = string.Format("		public {0}?  {1}  {2}\r\n", DotNetType.Replace("System.", ""), ColumnName, "{get;set;}");
				}
				else
				{
					v = string.Format("		public {0}  {1}  {2}\r\n", DotNetType.Replace("System.", ""), ColumnName, "{get;set;}");
				}
				return v;
			}
		}

		public string TypeOfKey
		{
			get
			{
				if (IsPrimaryKey || IsIdentity)
					return DotNetType.Replace("System.", "");
				else
					return "Not Found";
			}


		}
		public string PropertyCode2
		{
			get
			{
				string prop = string.Empty;
				if (IsPrimaryKey || IsIdentity)
				{
					prop = "		[Key] \r\n";

				}
				prop += string.Format("		public {0}  {1}  {2}\r\n", DotNetType.Replace("System.", ""), ColumnName, "{get;set;}");
				return prop;
			}
		}

		public string MvvmPropertyCode
		{
			get
			{
				string prop = string.Empty;
				prop += string.Format("		public {0}  {1}  \n {2} \r\n", DotNetType.Replace("System.", ""), ColumnName
				, string.Format(@"		{{
		   get=> _{0};
		   set => this.RaiseAndSetIfChanged(ref _{1},value);
		}}"
				, ColumnName.ToLower(), ColumnName.ToLower()));
				return prop;
			}
		}
		public string PrivateFeild
		{
			get
			{
				string field = string.Empty;
				field = string.Format("		private {0}  _{1};  \r\n", DotNetType.Replace("System.", ""), ColumnName.ToLower());
				return field;
			}
		}

		public string PrivateListPropertyFeild(string table_name)
		{

			string field = string.Empty;
			field = string.Format("		private {0}  _{1};  \r\n", "BindingList<" + table_name + "ViewModel>", table_name.ToLower() + "s");
			return field;

		}

		public string ListViewModelPropertyCode(string table_name)
		{

			string prop = string.Empty;
			prop += string.Format("		public {0}  {1}  \n {2} \r\n", "BindingList<" + table_name + "ViewModel>", table_name + "s"
			, string.Format(@"		{{
		   get=> _{0};
		   set => this.RaiseAndSetIfChanged(ref _{1},value);
		}}"
			, table_name.ToLower() + "s", table_name.ToLower() + "s"));
			return prop;

		}

		public string UpdateListWithParam
        {
            get
            {
				string body = string.Empty;
				body = string.Format("{0}=@{1}",ColumnName,ColumnName);
				return body;
            }
        }

		public string ObjectParameter
        {
			get
			{
				string body = string.Empty;
				//body = string.Format("{0}=@{1}", ColumnName, ColumnName);
				body = string.Format("{0}=data.{1}", ColumnName, ColumnName);
				return body;
			}
        }

		public string GetPropertyOfObject
		{
			get
			{
				string body = string.Empty;

				body = $"		update_obj.{ColumnName}=entity.{ColumnName}; \r\n";
				return body;
			}
		}

        #region UI
		public string GetTextColumnOfDataGrid
        {
            get
            {
				string body = string.Empty;
				string tag =  $"	<DataGridTextColumn Header=\"{ColumnName}\"  Binding=\"{{Binding {ColumnName} }} \" />  \r\n";
				return tag;
            }
        }

		public string GetOneColRowDefinitions
		{
			get
			{
				return "auto";
			}
		}

		public string GetOneColWithLeftRowData()
		{
			// <!--  row 0 -->
			string comment = $"<!--  row {Row_Index} --> \r\n";
			string tag = comment+= $" <Label Content=\"{ColumnName}\" Grid.Column=\"0\" Grid.Row=\"{Row_Index}\" Margin=\"0,5\"/>   \r\n";
            tag +=  $"<TextBox Text=\"{{Binding {ColumnName} }}\" Grid.Column=\"1\" Grid.Row=\"{Row_Index}\" Margin=\"0,5\"/>  \r\n \r\n";
            return tag;
		}
		#endregion
	}