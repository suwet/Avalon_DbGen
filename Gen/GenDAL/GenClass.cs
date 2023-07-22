using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gencode.GenDAL
{
    public class GenClass
    {

        public static void GenDataAccessClass(string nameSpace, string className,string result_model,string table_name,string sql_query)
        {
            try
            {
                string output_path = Path.Combine("Outputs", "Dals");
                //string query_path = Path.Combine("GenDAL", "Custom.sql");
                string base_dal_template_path = Path.Combine("GenDAL", "BaseDal.txt");
                string dal_template_path = Path.Combine("GenDAL", "DalClass.txt");

                string base_dal_template = File.ReadAllText(base_dal_template_path);
                string dal_template = File.ReadAllText(dal_template_path);

                string base_dal_class_str = base_dal_template.Replace("[{NAMESPACE}]", nameSpace);

                PrepareDal gp = new PrepareDal(sql_query);
                string col_list = gp.ListOfColumnName();
                string col_list_param = gp.ListOfColumnNameWithParam();
                string object_param = gp.ObjectParameters();
                string update_list_param = gp.UpdateColumnListWithParam();

                string dal_class_str = dal_template.Replace("[{NAMESPACE}]", nameSpace)
                                                            .Replace("[{MODEL_NAME}]", result_model)
                                                            .Replace("[{CLASS_NAME}]", className)
                                                            .Replace("[{COLUMN_LIST}]", col_list)
                                                            .Replace("[{COLUMN_LIST_PARAM}]", col_list_param)
                                                            .Replace("[{TABLE_NAME}]", table_name)
                                                            .Replace("[{UPDATE_LIST_PARAM}]", update_list_param)
                                                            .Replace("[{OBJECT_PARAM}]", object_param);

                Directory.CreateDirectory(output_path);

                File.WriteAllText(Path.Combine(output_path, "BaseDal" + ".cs"), base_dal_class_str);
                File.WriteAllText(Path.Combine(output_path, className + ".cs"), dal_class_str);

                Console.WriteLine("Success GenClass");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }

        public static void GenServiceClass(string nameSpace,string className,string modelName,string dalClassName,string viewModelName)
        {
            try
            {
                string output_path = Path.Combine("Outputs", "Services");
                string base_service_template_path = Path.Combine("GenDAL", "BaseService.txt");
                string service_template_path = Path.Combine("GenDAL", "ServiceClass.txt");

                string base_service_template = File.ReadAllText(base_service_template_path);
                string service_template = File.ReadAllText(service_template_path);

                string base_service_class_str = base_service_template.Replace("[{NAMESPACE}]", nameSpace)
                                                                     .Replace("[{VIEW_MODEL_NAME}]", viewModelName)
                                                                     .Replace("[{MODEL_NAME}]", modelName);

                string service_class_str = service_template.Replace("[{NAMESPACE}]", nameSpace)
                                                            .Replace("[{MODEL_NAME}]", modelName)
                                                            .Replace("[{CLASS_NAME}]", className)
                                                            .Replace("[{VIEW_MODEL_NAME}]", viewModelName)
                                                            .Replace("[{DAL_CLASS}]", dalClassName);

                Directory.CreateDirectory(output_path);

                File.WriteAllText(Path.Combine(output_path, "BaseService" + ".cs"), base_service_class_str);
                File.WriteAllText(Path.Combine(output_path, className + ".cs"), service_class_str);

                Console.WriteLine("Success GenClass");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }

        public static void GenEditForm(string nameSpace,string className,string viewModelName,string serviceName)
        {
            try
            {
                //EditMUserForm

                string output_path = Path.Combine("Outputs", "UIViews");
                string query_path = Path.Combine("GenDAL", "Custom.sql");
                string design_form_template_path = Path.Combine("GenDAL","UI", "EditForm.Designer.txt");
                string form_template_path = Path.Combine("GenDAL","UI", "EditForm.txt");
                string resource_template_path = Path.Combine("GenDAL", "UI", "EditForm.txt.resx");

                string design_form_template = File.ReadAllText(design_form_template_path);
                string form_template = File.ReadAllText(form_template_path);

                PrepareDal gp = new PrepareDal(File.ReadAllText(query_path));

                /*
                string declare_textbox = gp.TextBoxForColumn;
                string declare_label = gp.LableForColumn;
                string textbox_instant = gp.TextBoxInStant;
                string lable_instant = gp.LableInStant;
                string textbox_to_groupbox = gp.AddTextBoxToGroupBox(className.ToLower());
                string lable_to_groupbox = gp.AddLableToGroupBox(className.ToLower());
                string config_control = gp.ConfigTexboxAndLable;
                string design_form_class_str = design_form_template.Replace("[{NAMESPACE}]", nameSpace)
                                                                     .Replace("[{CLASS_NAME}]", className)
                                                                     .Replace("[{GROUPBOX_NAME}]", className.ToLower())
                                                                     .Replace("[{DECLARE_TEXTBOX_VARIABLE}]", declare_textbox)
                                                                     .Replace("[{DECLARE_TEXTBOX_VARIABLE}]", declare_textbox)
                                                                     .Replace("[{DECLARE_LABEL_VARIABLE}]", declare_label)
                                                                     .Replace("[{CREATE_TEXTBOX_INSTANT}]", textbox_instant)
                                                                     .Replace("[{ADD_TEXTBOX_TO_GROUPBOX}]", textbox_to_groupbox)
                                                                     .Replace("[{ADD_LABLE_TO_GROUPBOX}]", lable_to_groupbox)
                                                                     .Replace("[{CREATE_LABEL_INSTANT}]", lable_instant)
                                                                     .Replace("[{CONFIG_TEXTBOX_LABEL}]", config_control);
                */

                string binding_str = gp.BindingTextBox(viewModelName);


                string form_class_str = form_template.Replace("[{NAMESPACE}]", nameSpace)
                                                            .Replace("[{CLASS_NAME}]", className)
                                                            .Replace("[{VIEW_MODEL_NAME}]", viewModelName)
                                                            .Replace("[{SERVICE_NAME}]", serviceName)
                                                            .Replace("[{BINDING_TEXTBOX}]", binding_str);




                Directory.CreateDirectory(output_path);

                File.WriteAllText(Path.Combine(output_path, className + ".cs"), form_class_str);
                //File.WriteAllText(Path.Combine(output_path, className + ".Designer.cs"), design_form_class_str);

                Console.WriteLine("Success GenClass");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
        }
    }
}
