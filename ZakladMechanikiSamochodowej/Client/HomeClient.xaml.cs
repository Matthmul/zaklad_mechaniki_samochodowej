using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Linq;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Media;

namespace ZakladMechanikiSamochodowej.Client
{
    /// <summary>
    /// Logika interakcji dla klasy HomeClient.xaml
    /// </summary>
    /// 

    public partial class HomeClient : Window
    {
        
        private Order order;

        public HomeClient()
        {
            InitializeComponent();
            AddText();
            AddComboBoxBrands();
        }

        private void AddText()
        {
            txtUserNameText.Text = Properties.Settings.Default.UserName;

            var isNewUser = checkUserState(Properties.Settings.Default.UserName);

            if (isNewUser)
            {
                txtUserNameText.Foreground = Brushes.Red;
                isNewUserLabel.Visibility = Visibility.Visible;
            }
            else
            {
                txtUserNameText.Foreground = Brushes.Black;
                isNewUserLabel.Visibility = Visibility.Hidden;
            }

            var user = LoginTableActions.TryGetUserByName(Properties.Settings.Default.UserName);
            if(user?.Username == null) {
                MessageBox.Show("Nie ma pobrać użytkownika o takim loginie", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (user.PhoneNumber == null || user.EmialAddress==null)
            {
                txtTelephoneNumber.Text = "-------";
                txtEmail.Text = "-------";
            }
            else
            {
                txtTelephoneNumber.Text = user.PhoneNumber;
                txtEmail.Text = user.EmialAddress;
            }
        }

        private void AddComboBoxBrands()
        {
            List<string> carsList = new List<string>() { 
                "Toyota", 
                "Skoda", 
                "Dacia", 
                "Opel", 
                "Hyundai", 
                "Kia", 
                "Volkswagen", 
                "Renault", 
                "Fiat" 
            };

            foreach (var item in carsList)
            {
                comboBoxCarBrand.Items.Add(item);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool fix = false;
            bool review = false;
            bool assembly = false;
            bool technicalConsultation = false;
            bool orderingParts = false;
            bool training = false;

            var isNotAcceptedUser = checkUserState(Properties.Settings.Default.UserName);
            if (isNotAcceptedUser)
            {
                MessageBox.Show("Administrator nie potwierdził jeszcze konta użytkownikowi, nie możesz wysłać zlecenia.", 
                    "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (txtCarModel.Text == "" || txtNrVin.Text == "" || txtProductionYear.Text == "" ||
               txtRegistrationNumber.Text == "" || txtEngineCapacity.Text == "")
            {
                MessageBox.Show("Nie wpisano wszystkich wymaganych wartosci", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!int.TryParse(txtProductionYear.Text.ToString(), out _) || !int.TryParse(txtEngineCapacity.Text.ToString(), out _))
            {
                MessageBox.Show("Nie można przekonwertować wartości, wpisz prawidłową liczbe!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (comboBoxCarBrand.SelectedItem.ToString() == "System.Windows.Controls.ComboBoxItem: -------------")
            {
                MessageBox.Show("Musisz wybrać jakąś markę pojazdu!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            //checkboxy, które są na true
            var list = gridCheckBoxes.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);

            if (list.IsNullOrEmpty())
            {
                MessageBox.Show("Musisz wybrać jakąś czynność!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (var item in list)
            {
                if (item.Name == "Fix") fix = true;
                else if (item.Name == "Review") review = true;
                else if (item.Name == "Assembly") assembly = true;
                else if (item.Name == "TechnicalConsultation") technicalConsultation = true;
                else if (item.Name == "Training") training = true;
                else if (item.Name == "OrderingParts") orderingParts = true;
                else continue;
            }

            if (txtCarModel.Text.ToString() != string.Empty || txtNrVin.Text.ToString() != string.Empty || txtProductionYear.Text.ToString() != string.Empty 
                || txtRegistrationNumber.Text.ToString() != string.Empty || txtEngineCapacity.Text.ToString() != string.Empty || 
                comboBoxCarBrand.SelectedItem.ToString() != string.Empty)
            {
                string brand = comboBoxCarBrand.SelectedItem.ToString();
                var user = LoginTableActions.TryGetUserByName(Properties.Settings.Default.UserName);

                if (user != null && brand != string.Empty)
                {
                    int clientID = user.Id;

                    order = new Order(clientID,
                    brand,
                    txtCarModel.Text.ToString(),
                    txtNrVin.Text.ToString(),
                    Int32.Parse(txtProductionYear.Text.ToString()),
                    txtRegistrationNumber.Text.ToString(),
                    Int32.Parse(txtEngineCapacity.Text.ToString()),
                    OrderState.NEW)
                    {
                        Fix = fix,
                        Review = review,
                        Assembly = assembly,
                        Training = training,
                        TechnicalConsultation = technicalConsultation,
                        OrderingParts = orderingParts
                    };

                    OrdersTableActions.SaveOrder(order);
                    MessageBox.Show("Twoje zlecenie zostało wysłane do mechanika. Sprawdź status twojego zlecenia!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nie istnieje taki użytkownik lub marka jest nieprawidłowa.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool checkUserState(string username)
        {
            var user = LoginTableActions.TryGetUserByName(username);
            
            return user?.IsNew ?? false;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            var user = LoginTableActions.TryGetUserByName(Properties.Settings.Default.UserName);
            if (user == null)
            {
                MessageBox.Show("Nie ma pobrać użytkownika o takim loginie", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            List<Order> orderState = OrdersTableActions.GetLastOrder(user.Id);
            if (orderState == null || orderState.Count == 0)
            {
                MessageBox.Show("Nie stworzyłeś jeszcze żadnego zamówienia", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            statusText.Text= orderState[0].OrderState.ToString();
        }

        private void AddMailTelephoneButton_Click(object sender, RoutedEventArgs e)
        {     
            AddData data = new();
            data.ShowDialog();
            AddText();
            //Close();
        }
    }
}
