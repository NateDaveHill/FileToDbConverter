using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace _76_FileToDbConverter;

public partial class DatenbankAnzeige
{
    private readonly ObservableCollection<Share> Data = new();

    public DatenbankAnzeige()
    {
        using var context = new AppContext();
        context.Database.EnsureCreated();

        InitializeComponent();

        var retrievedDataFromDb = context.Shares.ToDictionary(x => x.Date, x => x);
        foreach (var share in retrievedDataFromDb)
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

        DbDataGrid.DataContext = Data;
    }

    private void BtnSließen(object sender, RoutedEventArgs e)
    {
        Close();
    }
}