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
		public string LableForColumn
        {
            get
            {
				string body = string.Empty;
				return string.Format("	private System.Windows.Forms.Label lbl_{0};", ColumnName.ToLower());
            }
        }

		public string TextBoxForColumn
		{
			get
			{
				string body = string.Empty;
				return string.Format("	private System.Windows.Forms.TextBox txt_{0};", ColumnName.ToLower());
			}
		}
		public string TextBoxInStant
        {
            get
            {
				string body = string.Empty;
				return string.Format("	this.txt_{0} = new System.Windows.Forms.TextBox();", ColumnName.ToLower());
			}
        }

		public string LableInStant
        {
			get
			{
				string body = string.Empty;
				return string.Format("	this.lbl_{0} = new System.Windows.Forms.Label();", ColumnName.ToLower());
			}
		}
		public string TextBoxBinding(string viewModelName)
        {

				string body = string.Empty;
				return string.Format("	this.Bind({0},vm=>vm.{1},v=>v.{2}.Text);", viewModelName, ColumnName.ToLower(), "txt_"+ColumnName.ToLower());
			
		}
		#endregion
	}