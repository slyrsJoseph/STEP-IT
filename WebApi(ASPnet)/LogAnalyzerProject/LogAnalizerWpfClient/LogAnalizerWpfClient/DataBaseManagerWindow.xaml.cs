using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace LogAnalizerWpfClient
{
    public partial class DatabaseManagerWindow : Window
    {
        private readonly LogApiClient _logApiClient;

        public DatabaseManagerWindow(LogApiClient logApiClient)
        {
            InitializeComponent();
            _logApiClient = logApiClient;
            LoadDatabases();
        }

        private async void LoadDatabases()
        {
            try
            {
                var databases = await _logApiClient.GetDatabasesAsync();
                comboDatabases.ItemsSource = databases;
                if (databases.Any())
                {
                    comboDatabases.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading database list: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var dbName = txtNewDatabase.Text.Trim();

            if (string.IsNullOrWhiteSpace(dbName))
            {
                MessageBox.Show("Enter a name for the new database.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                await _logApiClient.CreateDatabaseAsync(dbName);
                MessageBox.Show("The database has been successfully created..", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LoadDatabases();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnUse_Click(object sender, RoutedEventArgs e)
        {
            if (comboDatabases.SelectedItem == null)
            {
                MessageBox.Show("Select a database to use.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dbName = comboDatabases.SelectedItem.ToString();

            try
            {
                await _logApiClient.SelectDatabaseAsync(dbName);
                MessageBox.Show("Data base selected.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while selecting database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private async void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (comboDatabases.SelectedItem == null)
            {
                MessageBox.Show("Select a database to delete.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var dbName = comboDatabases.SelectedItem.ToString();

            var result = MessageBox.Show($"Are you sure you want to delete the database '{dbName}'?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    await _logApiClient.DeleteDatabaseAsync(dbName);
                    MessageBox.Show("The database has been deleted.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    LoadDatabases();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
