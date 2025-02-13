using System;
using System.IO;
using System.Windows;
using System.Globalization;

namespace MaxTemp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnAuswerten_Click(object sender, RoutedEventArgs e)
        {
            string csvFilePath = @".\temps.csv";

            if (!File.Exists(csvFilePath))
            {
                MessageBox.Show("File not found!");
                return;
            }

            if (startDatePicker.SelectedDate == null || endDatePicker.SelectedDate == null)
            {
                MessageBox.Show("Please select a valid date range.");
                return;
            }

            DateTime startDate = startDatePicker.SelectedDate.Value;
            DateTime endDate = endDatePicker.SelectedDate.Value;

            if (startDate > endDate)
            {
                MessageBox.Show("Start date cannot be after end date.");
                return;
            }

            double maxTemp = double.MinValue;
            string maxTempDetails = string.Empty;

            try
            {
                using (var reader = new StreamReader(csvFilePath))
                {
                    reader.ReadLine();

                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        var columns = line.Split(',');

                        if (columns.Length == 3)
                        {
                            var device = columns[0].Trim();
                            var timeStr = columns[1].Trim();
                            var tempStr = columns[2].Trim();

                            if (DateTime.TryParseExact(timeStr, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime timestamp))
                            {
                                if (timestamp >= startDate && timestamp <= endDate)
                                {
                                    if (double.TryParse(tempStr, out double temp))
                                    {
                                        temp = temp / 10.0;

                                        if (temp > maxTemp)
                                        {
                                            maxTemp = temp;
                                            maxTempDetails = $"Device: {device}, Time: {timestamp}, Temperature: {temp:F1}";
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine($"Skipping invalid temperature: {tempStr}");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Skipping invalid date format: {timeStr}");
                            }
                        }
                    }
                }

                if (maxTemp != double.MinValue)
                {
                    lblAusgabe.Content = $"Höchste Temperatur im Zeitraum: {maxTempDetails}";
                }
                else
                {
                    lblAusgabe.Content = "Keine gültigen Daten im gewählten Zeitraum gefunden.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}
