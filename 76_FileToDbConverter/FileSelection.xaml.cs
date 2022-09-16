using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Microsoft.Win32;
using MahApps.Metro.Controls;
using System.Globalization;

namespace _76_FileToDbConverter
{
    public partial class FileSelection
    {
        private static double? tempValue;
        private static string? folderPath;
        private static List<Share> listShares = new List<Share>();


        public FileSelection()
        {
            InitializeComponent();
        }

        private void BtnÖffnen(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            openFileDialog.Filter = "Comma-separated values (*.csv)|*.csv";

            if (openFileDialog.ShowDialog() == true)
            {
                txtBox.Text = File.ReadAllText(openFileDialog.FileName);
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                folderPath = fileInfo.FullName;
            }
        }

        private void BtnImport(object sender, RoutedEventArgs e)
        {
            using AppContext context = new AppContext();


            if (String.IsNullOrEmpty(txtBox.Text))
            {
                MessageBox.Show("Bitte wählen Sie eine .CSV Datei aus.");
            }
            else
            {
                var text = File.ReadAllLines(folderPath);

                foreach (var line in text.Skip(1))
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        continue;
                    }

                    var validData = line.Replace(".", string.Empty).Split(";")
                        .ToList().Select(x => x.Replace(" ", null)).ToList();

                    listShares.Add(new Share
                    {
                        Date = DateTime.ParseExact(validData[0], "ddMMyyyy", CultureInfo.CurrentCulture),
                        First = StringToDouble(validData[1]),
                        High = StringToDouble(validData[2]),
                        Low = StringToDouble(validData[3]),
                        Final = StringToDouble(validData[4]),
                        Volume = StringToDouble(validData[5]),
                        Added = DateTime.Now,
                        Updated = DateTime.Now
                    });
                }

                var retrievedDataFromDb = context.Shares.ToDictionary(x => x.Date, x => x);
                var retrievedDataFromFile = listShares;

                foreach (var shareFromFile in retrievedDataFromFile)
                {
                    var updateData = new Share();

                    var toBeUpdatedData = retrievedDataFromDb.TryGetValue(shareFromFile.Date, out updateData);

                    if (toBeUpdatedData)
                    {
                        if (updateData.Id != 0 && updateData.First != shareFromFile.First ||
                            updateData.Low != shareFromFile.Low || updateData.High != shareFromFile.High
                            || updateData.Final != shareFromFile.Final || updateData.Volume != shareFromFile.Volume)
                        {
                            updateData.Date = shareFromFile.Date;
                            updateData.First = shareFromFile.First;
                            updateData.Low = shareFromFile.Low;
                            updateData.High = shareFromFile.High;
                            updateData.Final = shareFromFile.Final;
                            updateData.Volume = shareFromFile.Volume;
                            updateData.Updated = shareFromFile.Updated;
                            context.Shares.Update(updateData);
                        }
                    }
                    else
                    {
                        context.Shares.Add(new Share
                        {
                            Date = shareFromFile.Date,
                            First = shareFromFile.First,
                            High = shareFromFile.High,
                            Low = shareFromFile.Low,
                            Final = shareFromFile.Final,
                            Volume = shareFromFile.Volume,
                            Added = shareFromFile.Added
                        });
                        retrievedDataFromDb.Add(shareFromFile.Date, shareFromFile);
                    }
                }

                context.SaveChanges();
                MessageBox.Show("Importieren war erfolgreich.");
                Close();
            }
        }

        public static double? StringToDouble(string item)
        {
            double value;

            var success = double.TryParse(item, out value);

            if (success)
            {
                tempValue = value;
            }
            else
            {
                tempValue = null;
            }

            return tempValue;
        }
    }
}