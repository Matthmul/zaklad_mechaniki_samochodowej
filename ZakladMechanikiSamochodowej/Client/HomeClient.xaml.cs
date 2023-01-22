using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;
using System;
using System.Linq;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;
using System.Diagnostics.Metrics;

namespace ZakladMechanikiSamochodowej.Client
{
    /// <summary>
    /// Logika interakcji dla klasy HomeClient.xaml
    /// </summary>
    public partial class HomeClient : Window
    {
        public HomeClient()
        {
            InitializeComponent();
            AddText();
            AddComboBoxBrands();
        }

        private void AddText()
        {
            txtUserName.Text = Properties.Settings.Default.UserName;
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

            string checkedCHeckbox = RetrieveCheckboxesAndGetOne();
            switch (checkedCHeckbox)
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
                    Console.WriteLine($"Przetworzono nieprawiłdową wartosc checkboxa");
                    break;
            }

            if (txtCarModel.Text.ToString() != string.Empty || txtNrVin.Text.ToString() != string.Empty || txtProductionYear.Text.ToString() != string.Empty ||
               txtRegistrationNumber.Text.ToString() != string.Empty || txtEngineCapacity.Text.ToString() != string.Empty || comboBoxCarBrand.SelectedItem.ToString() != string.Empty)
            {
                string brand = comboBoxCarBrand.SelectedItem.ToString();


                var user = LoginTableActions.TryGetUserByName(Properties.Settings.Default.UserName);


                if (user != null && brand != string.Empty)
                {
                    int clientID = user.Id;

                    OrdersTableActions.SaveOrder(new Order
                    (clientID,
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
                    });
                    MessageBox.Show("Twoje zlecenie zostało wysłane do mechanika. Sprawdź status twojego zlecenia!", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Nie istnieje użytkownik  lub marka jest nieprawidłowa.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public string RetrieveCheckboxesAndGetOne()
        {
            var list = gridCheckBoxes.Children.OfType<CheckBox>().Where(x => x.IsChecked == true);
            return list.ElementAt(0).Name;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            /*txtCarModel.Text = string.Empty;
			txtCarModel.GotFocus -= RemoveText;*/
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

        }
    }
}
