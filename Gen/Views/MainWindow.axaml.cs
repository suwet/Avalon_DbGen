using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Gen.Views;

public partial class MainWindow : Window
{
    private Button btn_clos_window;
    public MainWindow()
    {
        InitializeComponent();
        btn_clos_window = this.FindControl<Button>("btn_close");
        
        btn_clos_window.Click+=Btn_clos_windowOnClick;
        
    }

    private void Btn_clos_windowOnClick(object? sender, RoutedEventArgs e)
    {
       this.Close();
    }
}