using System;
using System.Collections.Generic;
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
using MahApps.Metro.Controls;

namespace _76_FileToDbConverter
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnEinfügen(object sender, RoutedEventArgs e)
        {
            FileSelection fileSelection = new FileSelection();
            fileSelection.Show();
        }

        private void BtnDatenbank(object sender, RoutedEventArgs e)
        {
            DatenbankAnzeige datenbankAnzeige = new DatenbankAnzeige();
            datenbankAnzeige.Show();
        }
    }
}