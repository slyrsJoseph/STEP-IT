using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using LogAnalizerShared;
using Microsoft.Win32;

namespace LogAnalizerWpfClient
{
    public partial class MainWindow : Window
    {
        private readonly LogApiClient _logApiClient;

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            _logApiClient = new LogApiClient(new HttpClient { BaseAddress = new Uri("http://localhost:5001") });
        }

        private void btnManageDatabases_Click(object sender, RoutedEventArgs e)
        {
            var window = new DatabaseManagerWindow(_logApiClient);
            window.ShowDialog();
        }

        private void btnCompareByDateRange_Click(object sender, RoutedEventArgs e)
        {
            var window = new DateTimeComparisonWindow(_logApiClient);
            window.ShowDialog();
        }

        private void btnWeekComparison_Click(object sender, RoutedEventArgs e)
        {
            var window = new WeekByWeekComparisonWindow(_logApiClient);
            window.ShowDialog();
        }
        
        private void btnOpenTableDateRange_Click(object sender, RoutedEventArgs e)
        {
            var window = new TableDateTimeComparisonWindow(_logApiClient);
            window.Show();
        }
        
        

        private void Minimize_Click(object sender, RoutedEventArgs e) => WindowState = WindowState.Minimized;
        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
            => WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) DragMove();
        }
    }
    
    
    
    
} 
