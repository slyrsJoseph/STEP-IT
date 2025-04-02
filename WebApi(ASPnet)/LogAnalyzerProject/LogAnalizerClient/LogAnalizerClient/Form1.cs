namespace LogAnalizerClient;
using LogAnalizerShared;

using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
public partial class Form1 : Form
{
    
    private readonly HttpClient _httpClient;
    private readonly LogApiClient _apiClient;
    
    public Form1()
    {
        InitializeComponent();
        var httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5000")
        };

        _apiClient = new LogApiClient(httpClient);

        comboBoxWeekType.DataSource = Enum.GetValues(typeof(LogWeekType));
        comboBoxWeekType2.DataSource = Enum.GetValues(typeof(LogWeekType));
    }

  

    private void buttonBrowse_Click(object sender, EventArgs e)
    {
        using var dialog = new OpenFileDialog();
        dialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
            textBoxFilePath.Text = dialog.FileName;
        }
    }

    private async void buttonImport_Click(object sender, EventArgs e)
    {
        string filePath = textBoxFilePath.Text;
        if (string.IsNullOrWhiteSpace(filePath))
        {
            MessageBox.Show("Выберите файл.");
            return;
        }

        if (comboBoxWeekType.SelectedItem is not LogWeekType weekType)
        {
            MessageBox.Show("Выберите тип недели.");
            return;
        }

        try
        {
            await _apiClient.ImportLogsAsync(filePath, weekType);
            MessageBox.Show("Логи успешно импортированы!");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при импорте логов: " + ex.Message);
        }
    }


    private async void buttonCompare_Click(object sender, EventArgs e)
    {
        if (comboBoxWeekType.SelectedItem is not LogWeekType week1 ||
            comboBoxWeekType2.SelectedItem is not LogWeekType week2)
        {
            MessageBox.Show("Выберите обе недели для сравнения.");
            return;
        }

        try
        {
            await _apiClient.CompareWeeksAsync(week1, week2);
            MessageBox.Show("Сравнение завершено.");
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при сравнении: " + ex.Message);
        }
    }

    private void comboBoxWeekType2_SelectedIndexChanged(object sender, EventArgs e)
    {
        throw new System.NotImplementedException();
    }

    private async void buttonShowResults_Click(object sender, EventArgs e)
    {
        if (comboBoxWeekType.SelectedItem is not LogWeekType week1 ||
            comboBoxWeekType2.SelectedItem is not LogWeekType week2)
        {
            MessageBox.Show("Выберите обе недели для отображения результатов.");
            return;
        }

        try
        {
            var results = await _apiClient.GetComparisonResultsAsync(week1, week2);
            dataGridView1.DataSource = results;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка при получении результатов: " + ex.Message);
        }
    }
}