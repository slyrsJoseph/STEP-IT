using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LogAnalizerShared;

using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WPF;
using LiveChartsCore.Kernel.Sketches;
using LiveChartsCore.Drawing;
using LiveChartsCore.Measure;
namespace LogAnalizerWpfClient
{
    public partial class ChartWindow : Window
    {
        private readonly List<ComparisonResult> _results;
        private readonly LogWeekType _week1;
        private readonly LogWeekType _week2;
        
        
        
       
        
        

        public ChartWindow(LogApiClient logApiClient, List<ComparisonResult> results, LogWeekType? week1, LogWeekType? week2)
        {
            InitializeComponent();
            
          

            this.Title = $"Comparison: {week1} vs {week2}";

            _results = results;

            if (week1.HasValue && week2.HasValue)
            {
                _week1 = week1.Value;
                _week2 = week2.Value;
                this.Title = $"Comparison: {week1.Value} vs {week2.Value}";
            }
            else
            {
                this.Title = "Comparison by Date Range";
            }
            

            comboReportType.ItemsSource = new List<string> { "Equipment Alarms" };
            comboReportType.SelectedIndex = 0;

            comboCategory.ItemsSource = new List<string>
               
               { "VPH", "BRC", "LGA", "HRN", "DDM", "DW","DW VFD", "DW ZPS", "DW ECS" ,"TFM", "ELT", "PDPH" };
            comboCategory.SelectedIndex = -1;
            
            
            
            
        }
        
        
     
        
        
        
        private void comboReportType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            comboCategory.IsEnabled = comboReportType.SelectedItem?.ToString() == "Equipment Alarms";
        }

        private void comboCategory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshChart();
        }
        
        
        private void comboMinCount_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshChart();
        }

        private void RefreshChart()
        {
            try
            {
                if (comboCategory.SelectedItem == null || _results == null)
                    return;

                string selectedCategory = comboCategory.SelectedItem.ToString();

                if (!int.TryParse(((ComboBoxItem)comboMinCount.SelectedItem)?.Content.ToString(), out int minCount))
                    minCount = 0;

                var filteredResults = _results
                    .Where(r => !string.IsNullOrEmpty(r.AlarmMessage) &&
                                r.AlarmMessage.Contains(selectedCategory, StringComparison.OrdinalIgnoreCase) &&
                                (r.CountWeek1 > minCount || r.CountWeek2 > minCount))
                    .OrderByDescending(r => r.CountWeek1 + r.CountWeek2)
                    .ToList();

                if (!filteredResults.Any())
                {
                    MessageBox.Show("No data for selected category.", "Information", MessageBoxButton.OK,
                        MessageBoxImage.Information);
                    chart.Series = Array.Empty<ISeries>();
                    chart.XAxes = Array.Empty<Axis>();
                    chart.YAxes = Array.Empty<Axis>();
                    return;
                }

                BuildChart(filteredResults);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating chart: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        

      private void BuildChart(List<ComparisonResult> filteredResults)
        {
            
            
            var labels = new List<string>();
            var valuesWeek1 = new List<double>();
            var valuesWeek2 = new List<double>();

            foreach (var result in filteredResults)
            {
               
                labels.Add(result.AlarmMessage);
                valuesWeek1.Add(result.CountWeek1);
                valuesWeek2.Add(result.CountWeek2);

              
                labels.Add(""); 
                valuesWeek1.Add(double.NaN); 
                valuesWeek2.Add(double.NaN);
                
                labels.Add(""); 
                valuesWeek1.Add(double.NaN); 
                valuesWeek2.Add(double.NaN);
                
                
               
                
                
            }

            var seriesWeek1 = new ColumnSeries<double>
            {
                Values = valuesWeek1,
                Name = _week1.ToString(),
                Fill = new SolidColorPaint(SKColors.SteelBlue),
                DataLabelsPaint = new SolidColorPaint(SKColors.Cyan),
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                DataLabelsFormatter = point => point.Coordinate.PrimaryValue.ToString(),
                
            };

            var seriesWeek2 = new ColumnSeries<double>
            {
                Values = valuesWeek2,
                Name = _week2.ToString(),
                Fill = new SolidColorPaint(SKColors.OrangeRed),
                DataLabelsPaint = new SolidColorPaint(SKColors.Cyan),
                DataLabelsPosition = LiveChartsCore.Measure.DataLabelsPosition.Top,
                DataLabelsFormatter = point => point.Coordinate.PrimaryValue.ToString(),
                
            };

            chart.Series = new ISeries[] { seriesWeek1, seriesWeek2 };

            chart.XAxes = new Axis[]
            {
                new Axis
                {
                    Labels = labels,
                    LabelsRotation = 25,
                    TextSize = 12,
                    LabelsPaint = new SolidColorPaint(SKColors.Cyan),
                    UnitWidth = 1,
                    ForceStepToMin = true,
                    Padding = new Padding(20, 0, 0, 50),
                    MinStep = 4,
                   
                }
            };
            
            chart.YAxes = new Axis[]
            {
                new Axis
                {
                    Name = "Count",
                    TextSize = 12,
                    LabelsPaint = new SolidColorPaint(SKColors.Cyan),
                    NamePaint = new SolidColorPaint(SKColors.Cyan)
                }
            };

            chart.LegendTextPaint = new SolidColorPaint(SKColors.Cyan);
            
            
          
        }
      

       
        

        private async void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.SaveFileDialog
            {
                FileName = "Chart",
                DefaultExt = ".png",
                Filter = "PNG Image (.png)|*.png"
            };

            if (dialog.ShowDialog() == true)
            {
                var filePath = dialog.FileName;
                using var stream = new FileStream(filePath, FileMode.Create);
              
                MessageBox.Show($"Chart saved to {filePath}", "Saved", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        private void MaximizeRestore_Click(object sender, RoutedEventArgs e)
        {
            if (WindowState == WindowState.Normal)
                WindowState = WindowState.Maximized;
            else
                WindowState = WindowState.Normal;
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


