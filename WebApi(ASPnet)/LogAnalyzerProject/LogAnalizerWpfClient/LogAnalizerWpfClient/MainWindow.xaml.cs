using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using LogAnalizerShared;
using Microsoft.Win32;

namespace LogAnalizerWpfClient
{
    public partial class MainWindow : Window
    {
        private readonly LogApiClient _logApiClient;
        // Хранилище загруженных логов: для каждого WeekType сохраняем путь файла и словарь подсчётов AlarmMessage
        private Dictionary<LogWeekType, (string FilePath, Dictionary<string, int> Counts)> logData
            = new Dictionary<LogWeekType, (string, Dictionary<string, int>)>();

        public MainWindow()
        {
            _logApiClient = new LogApiClient(new HttpClient { BaseAddress = new Uri("http://localhost:5000") });
            InitializeComponent();
            // Заполняем выпадающий список доступных недель (WeekType) для импорта
            comboImportWeek.ItemsSource = Enum.GetValues(typeof(LogWeekType));
            comboImportWeek.SelectedIndex = 0;
            // Кнопку сравнения отключаем до загрузки как минимум двух логов
            btnCompare.IsEnabled = false;
        }

        // Обработчик кнопки "Import Log" - импортирует лог-файл и привязывает к выбранной неделе
        private void btnImport_Click(object sender, RoutedEventArgs e)
        {
            if (comboImportWeek.SelectedItem == null)
            {
                MessageBox.Show("Выберите неделю (Week) перед импортом файла.", "Внимание",
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
                    string[] lines = File.ReadAllLines(filePath);
                    Dictionary<string, int> counts = new Dictionary<string, int>();

                    bool isFirstLine = true;
                    foreach (string line in lines)
                    {
                        if (isFirstLine)
                        {
                            isFirstLine = false; // Пропускаем заголовок
                            continue;
                        }

                        string[] fields = line.Split(',');

                        if (fields.Length > 11)
                        {
                            string message = fields[11].Trim();

                            if (!string.IsNullOrEmpty(message))
                            {
                                if (counts.ContainsKey(message))
                                    counts[message]++;
                                else
                                    counts[message] = 1;
                            }
                        }
                    }

                    // Сохраняем или обновляем данные для выбранной недели
                    logData[selectedWeek] = (filePath, counts);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при чтении файла: {ex.Message}", "Ошибка",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                // Обновление списка загруженных логов в интерфейсе
                listBoxLogs.Items.Clear();
                foreach (var entry in logData)
                {
                    LogWeekType week = entry.Key;
                    string fname = System.IO.Path.GetFileName(entry.Value.FilePath);
                    int totalCount = entry.Value.Counts.Values.Sum();
                    listBoxLogs.Items.Add($"{week}: {fname} ({totalCount} records)");
                }

                // Обновление списков недель для сравнения
                var loadedWeeks = logData.Keys.ToList();
                comboWeek1.ItemsSource = loadedWeeks;
                comboWeek2.ItemsSource = loadedWeeks;

                comboWeek1.SelectedItem = null;
                comboWeek2.SelectedItem = null;

                btnCompare.IsEnabled = logData.Count >= 2;
            }
        }

        // Обработчик кнопки "Compare Logs" - сравнение двух выбранных недель и отображение графика
        /*private void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (comboWeek1.SelectedItem == null || comboWeek2.SelectedItem == null)
            {
                MessageBox.Show("Не выбраны две недели для сравнения.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            LogWeekType week1 = (LogWeekType)comboWeek1.SelectedItem;
            LogWeekType week2 = (LogWeekType)comboWeek2.SelectedItem;
            if (week1 == week2)
            {
                MessageBox.Show("Выберите две **разные** недели для сравнения.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // Получаем словари AlarmMessage->Count для обеих недель
            var data1 = logData[week1].Counts;
            var data2 = logData[week2].Counts;
            // Формируем объединённый список всех AlarmMessage из двух недель
            var allMessages = new HashSet<string>(data1.Keys);
            allMessages.UnionWith(data2.Keys);
            // Создаем список результатов сравнения для каждого AlarmMessage
            List<ComparisonResult> results = new List<ComparisonResult>();
            foreach (string msg in allMessages)
            {
                int count1 = data1.ContainsKey(msg) ? data1[msg] : 0;
                int count2 = data2.ContainsKey(msg) ? data2[msg] : 0;
                // Опционально: сокращаем длинные названия сообщений для подписи на графике
                string displayMsg = msg;
                if (displayMsg.Length > 20)
                    displayMsg = displayMsg.Substring(0, 17) + "...";
                results.Add(
                    new ComparisonResult { AlarmMessage = displayMsg, CountWeek1 = count1, CountWeek2 = count2 });
            }

            // Сортируем результаты по названию AlarmMessage для устойчивого порядка (можно изменить на нужный порядок)
            results.Sort((a, b) => string.Compare(a.AlarmMessage, b.AlarmMessage, StringComparison.OrdinalIgnoreCase));
            // Открываем окно с графиком сравнения
            ChartWindow chartWindow = new ChartWindow(_logApiClient, results, week1, week2);
            chartWindow.Show();
        }*/
        private async void btnCompare_Click(object sender, RoutedEventArgs e)
        {
            if (comboWeek1.SelectedItem == null || comboWeek2.SelectedItem == null)
            {
                MessageBox.Show("Не выбраны две недели для сравнения.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }

            LogWeekType week1 = (LogWeekType)comboWeek1.SelectedItem;
            LogWeekType week2 = (LogWeekType)comboWeek2.SelectedItem;

            if (week1 == week2)
            {
                MessageBox.Show("Выберите две **разные** недели для сравнения.", "Внимание",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                // Вызов сервера для выполнения сравнения
                await _logApiClient.CompareWeeksAsync(week1, week2);

                // Получаем свежие результаты сравнения с сервера
                var results = await _logApiClient.GetComparisonResultsAsync(week1, week2);

                if (results == null || !results.Any())
                {
                    MessageBox.Show("Нет данных для выбранных недель.", "Информация",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                // Открываем окно с графиком сравнения
                ChartWindow chartWindow = new ChartWindow(_logApiClient, results, week1, week2);
                chartWindow.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сравнении недель: {ex.Message}", "Ошибка",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private async void btnVerifyCounts_Click(object sender, RoutedEventArgs e)
        {
            if (comboWeek1.SelectedItem == null || comboWeek2.SelectedItem == null)
            {
                MessageBox.Show("Выберите недели для проверки.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            LogWeekType week1 = (LogWeekType)comboWeek1.SelectedItem;
            LogWeekType week2 = (LogWeekType)comboWeek2.SelectedItem;

            string category = Microsoft.VisualBasic.Interaction.InputBox("Enter category (e.g., VPH, BRC, etc.)", "Category", "VPH");
            if (string.IsNullOrWhiteSpace(category))
            {
                MessageBox.Show("Категория не указана.", "Внимание", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var logsWeek1 = await _logApiClient.GetLogsByWeekAsync(week1);
            var logsWeek2 = await _logApiClient.GetLogsByWeekAsync(week2);

            int countWeek1 = logsWeek1.Count(log => log.FinalState == "G" && log.AlarmMessage.Contains(category, StringComparison.OrdinalIgnoreCase));
            int countWeek2 = logsWeek2.Count(log => log.FinalState == "G" && log.AlarmMessage.Contains(category, StringComparison.OrdinalIgnoreCase));

            MessageBox.Show($"Verification:\nWeek1 ({week1}): {countWeek1} alarms with FinalState='G'\nWeek2 ({week2}): {countWeek2} alarms with FinalState='G'", "Verification Result", MessageBoxButton.OK, MessageBoxImage.Information);
        }
        
        
    }
}