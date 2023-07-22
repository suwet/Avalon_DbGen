using System;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Media;
using MySqlConnector;
using ReactiveUI;

namespace Gen.ViewModels;

public class MainWindowViewModel : ViewModelBase
{
    public MainWindowViewModel()
    {
        TestConnectionCommand = ReactiveCommand.CreateFromTask<string>(TestConnectionString);
        GenCommand = ReactiveCommand.CreateFromTask(GenCode);
    }
    
    private string _connectionTestResultResult;
    private string _connection_str = "Server=127.0.0.1;Uid=root;Pwd=password;Database=avalon_clinic";
    private string _sql_query = "select * from users";
    private string _namespace = "Avalon.Models";
    private string _modelmodelclassname = "Users";
    private string _viewmodelmodelclassname = "UsersViewModel";
    private string _dal_class_name;
    private string _service_class_name;
    private string _table_name;
    private string _write_output;

    private bool _ischeck_dal = false;
    private bool _ischeck_service = false;

    private Brush _wrote_brush = new SolidColorBrush(Colors.Green);
    private Brush _connection_status_brush = new SolidColorBrush(Colors.Green);

    public string Connection_Str
    {
        get => _connection_str;
        set => this.RaiseAndSetIfChanged(ref _connection_str, value);
    }
    
    public string Connection_Test_Result
    {
        get => _connectionTestResultResult;
        set => this.RaiseAndSetIfChanged(ref _connectionTestResultResult, value);
    }
    
    public string Sql_Query
    {
        get => _sql_query;
        set => this.RaiseAndSetIfChanged(ref _sql_query, value);
    }
    
    public string NameSpace
    {
        get => _namespace;
        set => this.RaiseAndSetIfChanged(ref _namespace, value);
    }
    
    public string ModelClassName
    {
        get => _modelmodelclassname;
        set => this.RaiseAndSetIfChanged(ref _modelmodelclassname, value);
    }
    
    public string ViewModelClassName
    {
        get => _viewmodelmodelclassname;
        set => this.RaiseAndSetIfChanged(ref _viewmodelmodelclassname, value);
    }
    
    public string DalClassName
    {
        get => _dal_class_name;
        set => this.RaiseAndSetIfChanged(ref _dal_class_name, value);
    }
    
    public string ServiceClassName
    {
        get => _service_class_name;
        set => this.RaiseAndSetIfChanged(ref _service_class_name, value);
    }
    public string TableName
    {
        get => _table_name;
        set => this.RaiseAndSetIfChanged(ref _table_name, value);
    }
    
    public string Write_Output
    {
        get => _write_output;
        set => this.RaiseAndSetIfChanged(ref _write_output, value);
    }
    
    public bool Ischeck_Dal
    {
        get => _ischeck_dal;
        set => this.RaiseAndSetIfChanged(ref _ischeck_dal, value);
    }
    
    public bool Ischeck_Service
    {
        get => _ischeck_service;
        set => this.RaiseAndSetIfChanged(ref _ischeck_service, value);
    }

    public Brush WrotOutBrush
    {
        get => _wrote_brush;
        set=> this.RaiseAndSetIfChanged(ref _wrote_brush,value);
    }
    
    public Brush Connection_Status_Brush
    {
        get => _connection_status_brush;
        set => this.RaiseAndSetIfChanged(ref _connection_status_brush, value);
    }
    public ReactiveCommand<string, Unit> TestConnectionCommand  { get; }
    public ReactiveCommand<Unit,Unit>GenCommand  { get; }

    async Task TestConnectionString(string connection_str)
    {
        using (MySqlConnection con = new MySqlConnection(connection_str))
        {
            try
            {
                con.Open();
                Connection_Status_Brush = new SolidColorBrush(Colors.Green);
                Connection_Test_Result = "Result: Success";
            }
            catch (Exception e)
            {
                Connection_Test_Result = $"Result: Fail. {e.Message}";
                Connection_Status_Brush = new SolidColorBrush(Colors.Red);
            }
        }
    }

    public async Task GenCode()
    {
        try
        {
            Gencode.GenModel.GenModelsClass.GenClass(NameSpace, ModelClassName,Sql_Query.Trim());
            Gencode.GenModel.GenModelsClass.GenMvvmClass(NameSpace,ViewModelClassName,Sql_Query.Trim());
            if (_ischeck_dal && (string.IsNullOrEmpty(DalClassName)==false && string.IsNullOrEmpty(TableName)==false))
            {
                Gencode.GenDAL.GenClass.GenDataAccessClass(NameSpace,DalClassName,ModelClassName,TableName,Sql_Query.Trim());
            }

            if (_ischeck_service && (string.IsNullOrEmpty(ServiceClassName)==false))
            {
                Gencode.GenDAL.GenClass.GenServiceClass(NameSpace, ServiceClassName, ModelClassName, DalClassName, ViewModelClassName);
            }
            WrotOutBrush = new SolidColorBrush(Colors.Green);
            Write_Output = $"Success:Wrote output to  {AppDomain.CurrentDomain.BaseDirectory}Outputs";
           
        }
        catch (Exception e)
        {
            WrotOutBrush = new SolidColorBrush(Colors.Red);
            _write_output = "Error: Wrote output "+e.Message;
        }
    }
    
}