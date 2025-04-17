using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using LogAnalizerShared;
using Microsoft.Win32;
using System.Windows.Input;
namespace LogAnalizerWpfClient
{
    public partial class MainWindow : Window
    {
        
       
        private readonly LogApiClient _logApiClient;
        private readonly List<(string FilePath, LogWeekType Week)> importedLogs = new();

       // private int importCounter = 0;
        public MainWindow()
        {
            InitializeComponent();
            _logApiClient = new LogApiClient(new HttpClient { BaseAddress = new Uri("http://localhost:5001") });
            btnCompare.IsEnabled = false;
            comboImportWeek.ItemsSource = Enum.GetValues(typeof(LogWeekType)).Cast<LogWeekType>();
            comboImportWeek.SelectedIndex = 0; 
            
        }
       private async void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (comboImportWeek.SelectedItem == null)
            {
                MessageBox.Show("Выберите неделю перед импортом файла.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            LogWeekType selectedWeek = (LogWeekType)comboImportWeek.SelectedItem;

            OpenFileDialog ofd = new OpenFileDialog
            {
                Filter = "CSV Files (*.csv;*.txt)|*.csv;*.txt|All Files (*.*)|*.*",
                Title = "Select Alarm Log File"
            };

            if (ofd.ShowDialog() == true)
            {
                string filePath = ofd.FileName;

                try
                {
                    await _logApiClient.ImportLogsAsync(filePath, selectedWeek);

                    // Добавляем в список импортированных
                    importedLogs.Add((filePath, selectedWeek));
                    //importCounter++;

                    // Обновляем список в UI
                    listBoxLogs.Items.Add($"{selectedWeek}: {System.IO.Path.GetFileName(filePath)} (импортировано на сервер)");

                    if (importedLogs.Count == 2)
                    {
                        btnCompare.IsEnabled = true;
                        MessageBox.Show("Загружено два файла. Можно выполнить сравнение!", "Информация",
                            MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else if (importedLogs.Count > 2)
                    {
                        // Очищаем и оставляем только последние два
                        importedLogs.RemoveAt(0);
                        RefreshLogList();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при импорте файла: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void RefreshLogList()
        {
            listBoxLogs.Items.Clear();
            foreach (var (filePath, week) in importedLogs)
            {
                listBoxLogs.Items.Add($"{week}: {System.IO.Path.GetFileName(filePath)} (импортировано на сервер)");
            }
        }
         
        private void btnResetLogs_Click(object sender, RoutedEventArgs e)
        {
            importedLogs.Clear(); 
            listBoxLogs.Items.Clear();

       

            btnCompare.IsEnabled = false;

            MessageBox.Show("Логи сброшены. Выберите новые файлы для анализа.", "Информация",
                MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        private async void btnViewInTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (importedLogs.Count < 2)
                {
                    MessageBox.Show("Необходимо импортировать два файла перед просмотром таблицы.", "Предупреждение",
                        MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                // Берём две недели из импортированных логов
                var week1 = importedLogs[0].Week;
                var week2 = importedLogs[1].Week;

                var logsWeek1 = await _logApiClient.GetLogsByWeekAsync(week1);
                var logsWeek2 = await _logApiClient.GetLogsByWeekAsync(week2);

                var allLogs = logsWeek1.Concat(logsWeek2).ToList();

                // Фильтрация как для графика
                var allowedAlarmClasses = new[]
                {
                    "CRI_B", "CRI_C", "CRI_A", "FAULT",
                    "SYS_A", "SYS_B", "SYS_C",
                    "WRN", "WRN_A", "WRN_B", "WRN_C"
                };

                var filtered = allLogs
                    .Where(log => log.FinalState == "G" && allowedAlarmClasses.Contains(log.AlarmClass))
                    .ToList();

                var window = new TableViewWindow(filtered);
                window.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при отображении таблицы: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        
        private async void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (importedLogs.Count < 2)
            {
                MessageBox.Show("Необходимо загрузить два файла для сравнения.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var week1 = importedLogs[0].Week;
            var week2 = importedLogs[1].Week;

            try
            {
                var results = await _logApiClient.CompareWeeksInMemoryAsync(week1, week2);

                if (results == null || !results.Any())
                {
                    MessageBox.Show("Нет данных для выбранных логов.", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                ChartWindow chartWindow = new ChartWindow(_logApiClient, results, week1, week2);
                chartWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сравнении: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
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