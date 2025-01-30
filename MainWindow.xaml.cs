using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//dotnet add package CsvHelper
namespace MaxTemp
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public class csvData
    {
        private string geraetName, datum;
        private double temperatur;
        public CsvData getHighTemp(string filePath)
        {
         
            List<CsvData> csvList = new List<CsvData>();

   
            if (File.Exists(filePath))
            {
       
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] columns = line.Split(',');

                     
                        if (columns.Length >= 3)
                        {
                            CsvData data = new CsvData
                            {
                                Column1 = columns[0],
                                Column2 = int.TryParse(columns[1], out int col2) ? col2 : 0, 
                                Column3 = double.TryParse(columns[2], out double col3) ? col3 : 0 
                            };

                            csvList.Add(data);
                        }
                    }
                }

           
                if (csvList.Count > 0)
                {
                    CsvData highTempData = null;

                    foreach (var data in csvList)
                    {
                        if (highTempData == null || data.Column3 > highTempData.Column3)
                        {
                            highTempData = data;
                        }
                    }

                    return highTempData;
                }
            }
            else
            {
                Console.WriteLine("File does not exist.");
            }

            return null; // Return null if no data found or file is invalid
        }


    };
        
    }; 
    
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        c#.readFile(_)

        /// <summary>
        /// Diese Routine (EventHandler des Buttons Auswerten) liest die Werte
        /// zeilenweise aus der Datei temps.csv aus, merkt sich den höchsten Wert
        /// und gibt diesen auf der Oberfläche aus.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnAuswerten_Click(object sender, RoutedEventArgs e)
        {
            //Zugriff auf Datei erstellen.

            //Anfangswert setzen, um sinnvoll vergleichen zu können.


            //In einer Schleife die Werte holen und auswerten. Den größten Wert "merken".


            //Datei wieder freigeben.


            //Höchstwert auf Oberfläche ausgeben.

            MessageBox.Show("Implimenting Logic");
            //kommentieren Sie die Exception aus.
            throw new Exception("peng");
        }
    }
}
