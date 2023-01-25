using System.Windows;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy AccountEdition.xaml
    /// </summary>
    public partial class AccountEdition : Window
    {
        private User _user;

        public AccountEdition(User user)
        {
            InitializeComponent();
            _user = user;
            UpdateUserInfo();
        }

        private void UpdateUserInfo()
        {
            mainLabel.Text = "Obsługa użytkownika " + _user.Username;
            txtUsername.Text = _user.Username;
            txtPassword.Password = _user.Password;
            txtPhoneNumber.Text = _user.PhoneNumber;
            txtEmail.Text = _user.EmialAddress;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            if (LoginTableActions.TryGetUserByName(txtUsername.Text) != null)
            {
                string msg = "Dane nie zostały zapisane. Istnieje już użytkownik o takiej nazwie.";
                MessageBox.Show(
                  msg,
                  "Dane niezapisane",
                  MessageBoxButton.OK,
                  MessageBoxImage.Error);
                return;
            }
            _user.Username = txtUsername.Text;
            _user.Password = txtPassword.Password;
            _user.PhoneNumber = txtPhoneNumber.Text;
            _user.EmialAddress = txtEmail.Text;
            LoginTableActions.UpdateUserInformation(_user);
            UpdateUserInfo();
        }
    }
}
