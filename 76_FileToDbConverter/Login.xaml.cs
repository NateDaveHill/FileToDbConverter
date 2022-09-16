using System.Linq;
using System.Security.Cryptography;
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors.Core;


namespace _76_FileToDbConverter
{
    public partial class Login
    {
        public Login()
        {
            InitializeComponent();
        }

        private void BtnAnmelden(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            Hash256 hash256 = new Hash256();
            AppContext context = new AppContext();

            var existingUsername = context.Users.FirstOrDefault(x => x.Username == TxtUsername.Text);

            using SHA256 sha256 = SHA256.Create();

            string newPassword = hash256.GetHash(sha256, TxtPasswort.Password);

            if (existingUsername == null || newPassword != existingUsername.Password)
            {
                MessageBox.Show("Ihre Eingabe war ungültige. Bitte versuchen Sie es noch einmal.");
                TxtUsername.Clear();
                TxtPasswort.Clear();
            }
            else
            {
                if (newPassword == existingUsername.Password)
                {
                    mainWindow.Show();
                    TxtUsername.Clear();
                    TxtPasswort.Clear();
                }
            }
        }

        private void BtnAbbrechen(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void BtnRegistrierung(object sender, RoutedEventArgs e)
        {
            Registrierung registrierung = new Registrierung();
            registrierung.Show();
        }
    }
}