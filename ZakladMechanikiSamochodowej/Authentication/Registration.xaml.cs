using System.Windows;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Authentication
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationWindow.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            Login login = new();
            login.ShowDialog();
            Close();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtConfirmPassword.Password.ToString() != string.Empty || txtPassword.Password.ToString() != string.Empty || txtUserName.Text != string.Empty)
            {
                if (txtPassword.Password.ToString() == txtConfirmPassword.Password.ToString())
                {
                    var userName = txtUserName.Text;
                    if (LoginTableActions.TryGetUserByName(userName) != null)
                    {
                        MessageBox.Show("Użytkownik o takiej nazwie już istnieje.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        LoginTableActions.SaveUser(new User
                        {
                            Username = userName,
                            Password = txtPassword.Password.ToString()
                        });
                        MessageBox.Show("Twoje konto zostało utworzone. Zaloguj się teraz.", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Hasła muszą się zgadzać!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
