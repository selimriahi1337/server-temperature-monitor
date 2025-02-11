using System;
using System.IO;
using System.Windows;
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
                            var time = columns[1].Trim();
                            var tempStr = columns[2].Trim();

                            if (double.TryParse(tempStr, out double temp))
                            {
                                temp = temp / 10.0; 

                                if (temp > maxTemp)
                                {
                                    maxTemp = temp;
                                    maxTempDetails = $"Device: {device}, Time: {time}, Temperature: {temp:F1}";
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Skipping invalid temperature: {tempStr}");
                            }
                        }
                    }
                }

                
                if (maxTemp != double.MinValue)
                {
                    lblAusgabe.Content = $"Höchste Temperatur: {maxTempDetails}";
                }
                else
                {
                    lblAusgabe.Content = "Keine gültigen Daten gefunden.";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}