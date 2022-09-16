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
using System.Security.Cryptography;
using MahApps.Metro.Controls;

namespace _76_FileToDbConverter
{
    public partial class Registrierung
    {
        private readonly AppContext context = new AppContext();


        public Registrierung()
        {
            InitializeComponent();

        }

        private void BtnSpeichern(object sender, RoutedEventArgs e)
        {
            Hash256 hash256 = new Hash256();

            var existingUser = context.Users.Any(x => x.Username == TxtUsername.Text);

            if (existingUser)
            {
                MessageBox.Show("Dieser Benutzer existiert bereits. Bitte geben Sie einen anderen Benutzernamen ein.");
                TxtUsername.Clear();
                TxtPassword.Clear();
                TxtPasswordConfirm.Clear();
            }
            else
            {
                if (TxtPassword.Password == TxtPasswordConfirm.Password)
                {
                    string userPassword = TxtPasswordConfirm.Password;
                    using (SHA256 sha256 = SHA256.Create())
                    {
                        string newPassword = hash256.GetHash(sha256, userPassword);

                        context.Users.Add(new User
                        {
                            Username = TxtUsername.Text,
                            Password = newPassword
                        });
                        context.SaveChanges();
                    }

                    MessageBox.Show("Ihr Benutzer wurde erfolgreich angelegt.");
                    Close();
                }
                else
                {
                    MessageBox.Show(
                        "Das eingegebene Passwort stimmt nich überein. Bitte geben Sie Ihre Eingabe noch einmal ein.");
                    TxtPassword.Clear();
                    TxtPasswordConfirm.Clear();
                }
            }
        }

        private void BtnAbbrechen(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}