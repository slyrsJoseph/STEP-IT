namespace LogAnalizerClient;
using System;
using System.Windows.Forms;

static class Program
{
   
    [STAThread]
    static void Main()
    {
        
        ApplicationConfiguration.Initialize();
        Application.Run(new Form1());
    }
}