using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Linq;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;
using System.Diagnostics.Metrics;
using Microsoft.IdentityModel.Tokens;
using System.Windows.Documents;

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
        }

        private void AddComboBoxBrands()
        {
            List<Cars> carsList = CarsTableActions.GetAllCars();

            foreach (var item in carsList)
            {
                comboBoxCarBrand.Items.Add(item.CarModel);
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

            if (txtCarModel.Text == "BRAK" || txtNrVin.Text == "BRAK" || txtProductionYear.Text == "BRAK" ||
               txtRegistrationNumber.Text == "BRAK" || txtEngineCapacity.Text == "BRAK")
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
            var str = "";

            if (list.IsNullOrEmpty())
            {
                MessageBox.Show("Musisz wybrać jakąś czynność!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                str = list.ElementAt(0).Name;
            }

            switch (str)
            {
                case "Fix":
                    fix = true;
                    break;
                case "Review":
                    review = true;
                    break;
                case "Assembly":
                    assembly = true;
                    break;
                case "TechnicalConsultation":
                    technicalConsultation = true;
                    break;
                case "OrderingParts":
                    orderingParts = true;
                    break;
                case "Training":
                    training = true;
                    break;
                default:
                    MessageBox.Show($"Przetworzono nieprawiłdową wartosc checkboxa");
                    return;
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

        //po kliknięciu jednego checkboxa inne się odznaczają
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            Assembly.IsChecked = false;
            Training.IsChecked = false;
            OrderingParts.IsChecked = false;
            Review.IsChecked = false;
            TechnicalConsultation.IsChecked = false;

        }

        private void CheckBox_Checked2(object sender, RoutedEventArgs e)
        {
            Assembly.IsChecked = false;
            Training.IsChecked = false;
            OrderingParts.IsChecked = false;
            Fix.IsChecked = false;
            TechnicalConsultation.IsChecked = false;
        }
        private void CheckBox_Checked3(object sender, RoutedEventArgs e)
        {
            Review.IsChecked = false;
            Training.IsChecked = false;
            OrderingParts.IsChecked = false;
            Fix.IsChecked = false;
            TechnicalConsultation.IsChecked = false;
        }

        private void CheckBox_Checked4(object sender, RoutedEventArgs e)
        {
            Review.IsChecked = false;
            Training.IsChecked = false;
            OrderingParts.IsChecked = false;
            Fix.IsChecked = false;
            Assembly.IsChecked = false;
        }
        private void CheckBox_Checked5(object sender, RoutedEventArgs e)
        {
            Review.IsChecked = false;
            TechnicalConsultation.IsChecked = false;
            OrderingParts.IsChecked = false;
            Fix.IsChecked = false;
            Assembly.IsChecked = false;
        }
        private void CheckBox_Checked6(object sender, RoutedEventArgs e)
        {
            Review.IsChecked = false;
            TechnicalConsultation.IsChecked = false;
            Training.IsChecked = false;
            Fix.IsChecked = false;
            Assembly.IsChecked = false;
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= TextBox_GotFocus;
        }

        private void txtEngineCapacity_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtRegistrationNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtPRoductionYear_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtNrVIN_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtCarModel_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void StatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (order== null)
            {
                MessageBox.Show("Nie stworzyłeś jeszcze żadnego zamówienia", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            statusText.Text= order.OrderState.ToString();
        }

       
    }
}
