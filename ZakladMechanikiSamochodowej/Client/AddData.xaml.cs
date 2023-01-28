using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

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
