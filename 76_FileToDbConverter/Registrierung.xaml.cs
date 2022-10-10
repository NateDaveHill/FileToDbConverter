using System.Linq;
using System.Security.Cryptography;
using System.Windows;

namespace _76_FileToDbConverter;

public partial class Registrierung
{
    private readonly AppContext context = new();



    public Registrierung()
    {
        InitializeComponent();
        context.Database.EnsureCreated();
    }

    private void BtnSpeichern(object sender, RoutedEventArgs e)
    {
        var hash256 = new Hash256();

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
                var userPassword = TxtPasswordConfirm.Password;
                using (var sha256 = SHA256.Create())
                {
                    var newPassword = hash256.GetHash(sha256, userPassword);

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