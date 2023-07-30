using System;
using System.IO;

namespace Gencode.GenModel
{
    public class GenModelsClass
    {
        public static string connection_str = string.Empty;
        
        
        public static void GenClass(string nameSpace,string className,string sql_query)
        {
            try
            {
                string output_path = Path.Combine("Outputs","Models");
                //string query_path = Path.Combine("GenModel", "Custom.sql");
                string base_model_template_path = Path.Combine("GenModel", "BaseModel.txt");
                string model_template_path = Path.Combine("GenModel", "ModelClass.txt");
                
                string base_model_template = File.ReadAllText(base_model_template_path);
                string model_template = File.ReadAllText(model_template_path);

                string base_model_class_str = base_model_template.Replace("[{NAMESPACE}]",nameSpace);
                
                PreparePocoModel gp = new PreparePocoModel(sql_query,connection_str);
                string propertys = gp.GetProperty;
                string model_class_str = model_template.Replace("[{NAMESPACE}]", nameSpace)
                                                            .Replace("[{PROPERTIES}]", propertys)
                                                            .Replace("[{CLASS_NAME}]", className);

                Directory.CreateDirectory(output_path);

                File.WriteAllText(Path.Combine(output_path, "ModelBase" + ".cs"), base_model_class_str);
                File.WriteAllText(Path.Combine(output_path, className + ".cs"), model_class_str);
                
                Console.WriteLine("Success GenClass");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            
        }

        public static void GenMvvmClass(string nameSpace, string className,string query_path)
        {
            try
            {
                string output_path = Path.Combine("Outputs", "ViewModels");
                //string query_path = Path.Combine("GenModel", "Custom.sql");
                string list_model_template_path = Path.Combine("GenModel", "ListViewModel.txt");
                string base_model_template_path = Path.Combine("GenModel", "ViewModelBase.txt");
                string model_template_path = Path.Combine("GenModel", "ViewModel.txt");

                string base_model_template = File.ReadAllText(base_model_template_path);
                string model_template = File.ReadAllText(model_template_path);
                string list_view_model_template = File.ReadAllText(list_model_template_path);
                string base_model_class_str = base_model_template.Replace("[{NAMESPACE}]", nameSpace);

                PreparePocoModel gp = new PreparePocoModel(query_path,connection_str);
                string propertys = gp.GetMvvmProperty;
                string privete_feild = gp.GetPrivateFeild;
                string model_class_str = model_template.Replace("[{NAMESPACE}]", nameSpace)
                                                            .Replace("[{PROPERTIES}]", propertys)
                                                            .Replace("[{PRIVATE_FEILD}]", privete_feild)
                                                            .Replace("[{CLASS_NAME}]", className);


                string list_view_model_class_str = list_view_model_template.Replace("[{NAMESPACE}]", nameSpace)
                                        .Replace("[{CLASS_NAME}]", "List"+className)
                                        .Replace("[{ViewModel}]", className);


                Directory.CreateDirectory(output_path);

                File.WriteAllText(Path.Combine(output_path, "ViewModelBase" + ".cs"), base_model_class_str);
                File.WriteAllText(Path.Combine(output_path, className + ".cs"), model_class_str);
                File.WriteAllText(Path.Combine(output_path, "List"+className + ".cs"), list_view_model_class_str);
                Console.WriteLine("Success GenClass");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static void GenAvaloniaDataGridUI(string collectionName, string dataGridName,string query_path)
        {
            try
            {
                string output_path = Path.Combine("Outputs", "UIs");
                string data_grid_template_path = Path.Combine("GenModel", "DataGrid.txt");

                string data_grid__template = File.ReadAllText(data_grid_template_path);
                PreparePocoModel gp = new PreparePocoModel(query_path,connection_str);
                string text_column_grid = gp.GetTextColumnOfDataGrid;
                string data_grid_str_tag = data_grid__template.Replace("[{DATA_GRID_NAME}]",dataGridName)
                                                          .Replace("[{COLLECTION_NAME}]",collectionName)
                                                          .Replace("[{DATA_GRID_TEXT_COLUMN_NAME}]",text_column_grid);
                Directory.CreateDirectory(output_path);
                File.WriteAllText(Path.Combine(output_path, dataGridName + ".xaml"), data_grid_str_tag);
            }
            catch (System.Exception exe)
            {
                
                throw;
            }
        }

        public static void GenAvaloniaOneColWithLeftLable(string headerText, string query_path)
        {
            try
            {
                string output_path = Path.Combine("Outputs", "UIs");
                string one_col_template_path = Path.Combine("GenModel", "OneColWithLeftLable.txt");

                string data_grid__template = File.ReadAllText(one_col_template_path);
                PreparePocoModel gp = new PreparePocoModel(query_path,connection_str);
                string one_col_text = gp.GetOneColWithLeftRowData;
                string row_def = gp.GetOneColRowDefinitions;

                string one_col_tag_data = data_grid__template.Replace("[{HEADER_TEXT}]",headerText)
                                                          .Replace("[{ROW_DEF}]",row_def)
                                                          .Replace(" [{ROW_DATA}]",one_col_text);
                Directory.CreateDirectory(output_path);
                File.WriteAllText(Path.Combine(output_path, headerText + "one_col.xaml"), one_col_tag_data);
            }
            catch (System.Exception exe)
            {
                
                throw;
            }
        }

        public static void GenAvaloniaTwoColWithLeftLable(string headerText, string query_path)
        {
            try
            {
                string output_path = Path.Combine("Outputs", "UIs");
                string two_col_template_path = Path.Combine("GenModel", "OneColWithLeftLable.txt");

                string data_grid__template = File.ReadAllText(two_col_template_path);
                PreparePocoModel gp = new PreparePocoModel(query_path,connection_str);
                string two_col_text = gp.GetOneColWithLeftRowData;
                string row_def = gp.GetOneColRowDefinitions;

                string tow_col_tag_data = data_grid__template.Replace("[{HEADER_TEXT}]",headerText)
                                                          .Replace("[{ROW_DEF}]",row_def)
                                                          .Replace(" [{ROW_DATA}]",two_col_text);
                Directory.CreateDirectory(output_path);
                File.WriteAllText(Path.Combine(output_path, headerText + "two_col.xaml"), tow_col_tag_data);
            }
            catch (System.Exception exe)
            {
                
                throw;
            }
        }
    }
}
