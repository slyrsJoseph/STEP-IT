using System.Windows;

namespace LogAnalizerWpfClient;

public partial class SplashWindow : Window
{
    public SplashWindow()
    {
        InitializeComponent();
        //Loaded += FadeOut_Completed;
    }
    
    
    /*private async void SplashWindow_Loaded(object sender, RoutedEventArgs e)
    {
       // await Task.Delay(7000); // ⏱️ Задержка 3 секунды (можно настроить)

        // После задержки открываем MainWindow
        /*var mainWindow = new MainWindow();
        mainWindow.Show();#1#

        // Закрываем splash
       // Close();
    }*/
    
    private  void FadeOut_Completed(object sender, EventArgs e)
    {
       
        var mainWindow = new MainWindow();
         mainWindow.Show();

        Close();
    }
    
    
}