using System.Windows;

namespace LogAnalizerWpfClient;

public partial class SplashWindow : Window
{
    public SplashWindow()
    {
        InitializeComponent();
       
    }
    
    
   
    
    private  void FadeOut_Completed(object sender, EventArgs e)
    {
       
        var mainWindow = new MainWindow();
         mainWindow.Show();

        Close();
    }
    
    
}