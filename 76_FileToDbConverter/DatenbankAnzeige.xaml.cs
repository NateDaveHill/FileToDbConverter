using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    public partial class DatenbankAnzeige
    {
        private ObservableCollection<Share> Data = new ObservableCollection<Share>();

        public DatenbankAnzeige()
        {
            using AppContext context = new AppContext();

            InitializeComponent();

            var retrievedDataFromDb = context.Shares.ToDictionary(x => x.Date, x => x);
            foreach (var share in retrievedDataFromDb)
            {
                Data.Add(new Share
                {
                    Id = share.Value.Id,
                    Date = share.Value.Date,
                    First = share.Value.First,
                    High = share.Value.High,
                    Low = share.Value.Low,
                    Final = share.Value.Final,
                    Volume = share.Value.Volume,
                    Added = share.Value.Added,
                    Updated = share.Value.Updated
                });
            }

            DbDataGrid.DataContext = Data;
        }

        private void BtnSließen(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}