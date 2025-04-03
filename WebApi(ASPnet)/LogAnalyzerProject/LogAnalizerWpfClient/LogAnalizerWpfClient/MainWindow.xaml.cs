using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Win32;
using LogAnalizerShared;
using System.Net.Http.Json;

namespace LogAnalizerWpfClient
{
    public partial class MainWindow : Window
    {
        private readonly HttpClient _httpClient;
        private readonly LogApiClient _apiClient;

        public MainWindow()
        {
            InitializeComponent();

            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5000")
            };

            _apiClient = new LogApiClient(_httpClient);

            comboBoxWeekType.ItemsSource = Enum.GetValues(typeof(LogWeekType));
            comboBoxWeekType2.ItemsSource = Enum.GetValues(typeof(LogWeekType));
        }

        private void ButtonBrowseCurrent_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                textBoxCurrentWeekFile.Text = dialog.FileName;
            }
        }

        private void ButtonBrowsePrevious_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*"
            };

            if (dialog.ShowDialog() == true)
            {
                textBoxPreviousWeekFile.Text = dialog.FileName;
            }
        }

        private async void ButtonImport_Click(object sender, RoutedEventArgs e)
        {
            string filePath = textBoxCurrentWeekFile.Text;
            if (string.IsNullOrWhiteSpace(filePath))
            {
                MessageBox.Show("Choose file.");
                return;
            }

            if (comboBoxWeekType.SelectedItem is not LogWeekType weekType)
            {
                MessageBox.Show("Choose week.");
                return;
            }

            try
            {
                await _apiClient.ImportLogsAsync(filePath, weekType);
                MessageBox.Show("Logs has successfully imported.!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error could not import log: " + ex.Message);
            }
        }

        private async void ButtonCompare_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxWeekType.SelectedItem is LogWeekType week1 &&
                comboBoxWeekType2.SelectedItem is LogWeekType week2)
            {
                try
                {
                    // Шаг 1: сравнение и сохранение результатов в базу
                    await _apiClient.CompareWeeksAsync(week1, week2);

                    // Шаг 2: получение результатов из базы
                    var results = await _apiClient.GetComparisonResultsAsync(week1, week2);

                    // Шаг 3: отображение в DataGrid
                    dataGridResults.ItemsSource = results;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при сравнении: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Выбери обе недели для сравнения.");
            }
        }
    }
}