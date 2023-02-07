using Microsoft.IdentityModel.Tokens;
using System.Text.RegularExpressions;
using System.Windows;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;

namespace ZakladMechanikiSamochodowej.Client
{
    /// <summary>
    /// Interaction logic for AddData.xaml
    /// </summary>
    public  partial class AddData : Window
    {
        public AddData()
        {
            InitializeComponent();
        }

        private void addClientAdditionalData(object sender, RoutedEventArgs e)
        {
            var email = txtEmail.Text;
            var phone = txtTelephone.Text;

            if(!IsValidEmailAddress(email) || email.IsNullOrEmpty())
            {
                MessageBox.Show("Nieprawidłowy adres e-mail");
                return;
            }

            if (!IsValidPhoneNumber(phone) || phone.IsNullOrEmpty())
            {
                MessageBox.Show("Nieprawidłowy numer telefonu");
                return;
            }

            var user=LoginTableActions.TryGetUserByName(Properties.Settings.Default.UserName);
            if (user == null)
            {
                MessageBox.Show("Nie ma pobrać użytkownika o takim loginie", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            user.PhoneNumber = phone;
            user.EmialAddress = email;


           LoginTableActions.UpdateUserInformation(user);
           MessageBox.Show("Poprawnie zmodyfikowano dane", "Done", MessageBoxButton.OK, MessageBoxImage.Information);

           Hide();
           Close();
        }

        public bool IsValidEmailAddress(string s)
        {
            Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
            return regex.IsMatch(s);
        }

        public bool IsValidPhoneNumber(string s)
        {
            Regex phoneNumpattern = new Regex(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$");
            return phoneNumpattern.IsMatch(s);
        }

    }
}
