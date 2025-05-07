using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LogAnalizerShared;

namespace LogAnalizerWpfClient
{
    public partial class TableDateTimeComparisonWindow : Window
    {
        private readonly LogApiClient _apiClient;
        private List<AlarmlogClient> _allLogs = new();

        private readonly List<string> _equipmentCategories = new()
        {
            "Все", "VPH", "BRC", "LGA", "HRN", "DDM", "DW",
            "DW VFD", "DW ZPS", "DW ECS", "TFM", "ELT", "PDPH"
        };

        private readonly string[] _allowedAlarmClasses =
        {
            "CRI_B", "CRI_C", "CRI_A", "FAULT",
            "SYS_A", "SYS_B", "SYS_C",
            "WRN", "WRN_A", "WRN_B", "WRN_C"
        };

        private Dictionary<LogWeekType, (DateTime Start, DateTime End)> _weekDateRanges = new();

        public TableDateTimeComparisonWindow(LogApiClient apiClient)
        {
            InitializeComponent();
            _apiClient = apiClient;
            comboEquipment.ItemsSource = _equipmentCategories;
            comboEquipment.SelectedIndex = 0;
            LoadAvailableWeeks();
        }

        private async void LoadAvailableWeeks()
        {
            var weeks = await _apiClient.GetAvailableWeekTypesAsync();
            _weekDateRanges = new Dictionary<LogWeekType, (DateTime, DateTime)>();

            foreach (var week in weeks)
            {
                var logs = await _apiClient.GetLogsByWeekAsync(week);
                if (!logs.Any()) continue;

                var min = logs.Min(x => x.TimeWhenLogged);
                var max = logs.Max(x => x.TimeWhenLogged);

                _weekDateRanges[week] = (min, max);
            }

            comboWeek1.ItemsSource = _weekDateRanges.Keys;
            comboWeek2.ItemsSource = _weekDateRanges.Keys;

            comboWeek1.SelectedItem = _weekDateRanges.Keys.FirstOrDefault();
            comboWeek2.SelectedItem = _weekDateRanges.Keys.Skip(1).FirstOrDefault();

            comboWeek1_SelectionChanged(null, null);
            comboWeek2_SelectionChanged(null, null);
        }

     
        
        
        private async void btnCompareDateRange_Click(object sender, RoutedEventArgs e)
        {
            if (btnCompare.IsEnabled == false) return;

            try
            {
                btnCompare.IsEnabled = false;

                if (comboWeek1.SelectedItem == null || comboWeek2.SelectedItem == null)
                {
                    MessageBox.Show("Select both weeks to compare.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var week1 = (LogWeekType)Enum.Parse(typeof(LogWeekType), comboWeek1.SelectedItem.ToString().Replace("Week", ""));
                var week2 = (LogWeekType)Enum.Parse(typeof(LogWeekType), comboWeek2.SelectedItem.ToString().Replace("Week", ""));

                var allLogs = await _apiClient.GetLogsByWeekAsync(week1);
                allLogs.AddRange(await _apiClient.GetLogsByWeekAsync(week2));
                _allLogs = allLogs;

                LoadFilteredLogs();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while comparing logs: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                btnCompare.IsEnabled = true;
            }
        }
        
        
        

        private void LoadFilteredLogs()
        {
            if (_allLogs == null || !_allLogs.Any()) return;

            string selectedCategory = comboEquipment.SelectedItem?.ToString() ?? "All";

            var filtered = _allLogs
                .Where(log => log.FinalState == "G" && _allowedAlarmClasses.Contains(log.AlarmClass))
                .Where(log => selectedCategory == "All" || log.AlarmMessage.Contains(selectedCategory, StringComparison.OrdinalIgnoreCase))
                .ToList();

            dataGridLogs.ItemsSource = filtered;
        }

        private void comboWeek1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboWeek1.SelectedItem is LogWeekType week && _weekDateRanges.TryGetValue(week, out var range))
            {
                textRangeStart1.Text = range.Start.ToString("g");
                textRangeEnd1.Text = range.End.ToString("g");
            }
        }

        private void comboWeek2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboWeek2.SelectedItem is LogWeekType week && _weekDateRanges.TryGetValue(week, out var range))
            {
                textRangeStart2.Text = range.Start.ToString("g");
                textRangeEnd2.Text = range.End.ToString("g");
            }
        }

        private void comboEquipment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadFilteredLogs();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
    }
}