using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using LogAnalizerShared;
using Microsoft.Win32;

namespace LogAnalizerWpfClient
{
    public partial class WeekByWeekComparisonWindow : Window
    {
        private readonly LogApiClient _logApiClient;
        private readonly Dictionary<LogWeekType, Button> _weekButtons = new();
        private readonly HashSet<LogWeekType> _importedWeeks = new();
        private readonly List<LogWeekType> _selectedWeeksForComparison = new();
        private LogWeekType? _selectedWeekForImport;

        public WeekByWeekComparisonWindow(LogApiClient apiClient)
        {
            InitializeComponent();
            _logApiClient = apiClient;
            LoadWeeks();
        }

        private async void LoadWeeks()
        {
            WeekGrid.Children.Clear();
            _weekButtons.Clear();
            _selectedWeeksForComparison.Clear();
            _selectedWeekForImport = null;

            var available = await _logApiClient.GetAvailableWeekTypesAsync();
            foreach (var week in Enum.GetValues(typeof(LogWeekType)).Cast<LogWeekType>())
            {
                var button = new Button
                {
                    Content = ((int)week).ToString(),
                    Tag = week,
                    Style = (Style)FindResource("WeekButtonStyle"),
                    Background = available.Contains(week) ? Brushes.Red : Brushes.LightGreen
                };
                button.Click += WeekButton_Click;

                WeekGrid.Children.Add(button);
                _weekButtons[week] = button;

                if (available.Contains(week))
                    _importedWeeks.Add(week);
            }
        }

        private void WeekButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not LogWeekType week)
                return;

            
            if (!_importedWeeks.Contains(week))
            {
                _selectedWeekForImport = week;

              
                foreach (var button in _weekButtons.Values)
                {
                    button.BorderBrush = null;
                    button.BorderThickness = new Thickness(1);
                }

              
                btn.BorderBrush = Brushes.Blue;
                btn.BorderThickness = new Thickness(3);
                return;
            }

           
            if (_selectedWeeksForComparison.Contains(week))
            {
                _selectedWeeksForComparison.Remove(week);
                btn.BorderBrush = null;
                btn.BorderThickness = new Thickness(1);
            }
            else
            {
                
                if (_selectedWeeksForComparison.Count == 2)
                {
                    
                    var toUnselect = _selectedWeeksForComparison[0];
                    _selectedWeeksForComparison.RemoveAt(0);

                    var btnToUnselect = _weekButtons[toUnselect];
                    btnToUnselect.BorderBrush = null;
                    btnToUnselect.BorderThickness = new Thickness(1);
                }

                _selectedWeeksForComparison.Add(week);
                btn.BorderBrush = Brushes.Gold;
                btn.BorderThickness = new Thickness(3);
            }
        }

        private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWeekForImport == null)
            {
                MessageBox.Show("Select a week to import.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var ofd = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv;*.txt)|*.csv;*.txt|All Files (*.*)|*.*",
                Title = "Select log file"
            };

            if (ofd.ShowDialog() == true)
            {
                try
                {
                    await _logApiClient.ImportLogsAsync(ofd.FileName, _selectedWeekForImport.Value);
                    MessageBox.Show($"Import completed for the week {_selectedWeekForImport.Value}", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadWeeks();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error while importing: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private async void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedWeeksForComparison.Count != 2)
            {
                MessageBox.Show("Select exactly two imported weeks to compare.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                var results = await _logApiClient.CompareWeeksInMemoryAsync(_selectedWeeksForComparison[0], _selectedWeeksForComparison[1]);
                if (results == null || !results.Any())
                {
                    MessageBox.Show("No data for comparison.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var chartWindow = new ChartWindow(_logApiClient, results, _selectedWeeksForComparison[0], _selectedWeeksForComparison[1]);
                chartWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while comparing: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}