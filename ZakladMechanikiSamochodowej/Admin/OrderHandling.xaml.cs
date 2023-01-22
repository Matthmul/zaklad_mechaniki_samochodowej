using System;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy OrderHandling.xaml
    /// </summary>
    public partial class OrderHandling : Window
    {
        private Order _orderInfo;

        public OrderHandling(Order orderInfo)
        {
            InitializeComponent();
            _orderInfo = orderInfo;

            PrepareComboBox();
            LoadOrder();
        }

        private void PrepareComboBox()
        {
            foreach (var state in Enum.GetNames(typeof(OrderState)).ToList())
            {
                comboBoxOrderState.Items.Add(state);
            }
        }

        private void LoadOrder()
        {
            txtCarBrand.Text = _orderInfo.Brand;
            txtCarModel.Text = _orderInfo.Model;
            txtEngineCapacity.Text = _orderInfo.EngineCapacity.ToString();
            txtNrVin.Text = _orderInfo.NrVIN;
            txtProductionYear.Text = _orderInfo.ProductionYear.ToString();
            txtRegistrationNumber.Text = _orderInfo.RegistrationNumber;
            comboBoxOrderState.SelectedIndex = (int)_orderInfo.OrderState;
        }

        private bool TryParseText(TextBox tb, out int oiToSave)
        {
            if (int.TryParse(tb.Text, out int num))
            {
                oiToSave = num;
                tb.Background = Brushes.White;
                return true;
            }
            else
            {
                tb.Background = Brushes.Red;
                string msg = "Dane nie zostały zapisane. Sprawdź czy dobrze wypełniono.";
                MessageBox.Show(
                  msg,
                  "Dane niezapisane",
                  MessageBoxButton.OK,
                  MessageBoxImage.Warning);
                oiToSave = -1;
                return false;
            }
        }

        private void ButtonEditCar_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (txtCarBrand.IsEnabled)
            {
                _orderInfo.Brand = txtCarBrand.Text;
                _orderInfo.Model = txtCarModel.Text;
                _orderInfo.NrVIN = txtNrVin.Text;
                _orderInfo.RegistrationNumber = txtRegistrationNumber.Text;

                if (TryParseText(txtEngineCapacity, out int engineCapacity)
                    && TryParseText(txtProductionYear, out int productionYear))
                {
                    _orderInfo.EngineCapacity = engineCapacity;
                    _orderInfo.ProductionYear = productionYear;
                }
                else
                {
                    return;
                }

                txtCarBrand.IsEnabled = false;
                txtCarModel.IsEnabled = false;
                txtEngineCapacity.IsEnabled = false;
                txtNrVin.IsEnabled = false;
                txtProductionYear.IsEnabled = false;
                txtRegistrationNumber.IsEnabled = false;

                btn.Content = "Edytuj dane samochodu";

                OrdersTableActions.UpdateOrder(_orderInfo);
            }
            else
            {
                txtCarBrand.IsEnabled = true;
                txtCarModel.IsEnabled = true;
                txtEngineCapacity.IsEnabled = true;
                txtNrVin.IsEnabled = true;
                txtProductionYear.IsEnabled = true;
                txtRegistrationNumber.IsEnabled = true;

                btn.Content = "Zapisz dane samochodu";
            }
        }

        private void ButtonEditOrderState_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (comboBoxOrderState.IsEnabled)
            {
                comboBoxOrderState.IsEnabled = false;
                btn.Content = "Edytuj status zlecenia";
                string selectedVal = (string)comboBoxOrderState.SelectedValue;
                _orderInfo.OrderState = (OrderState)Enum.Parse(typeof(OrderState), selectedVal);
                OrdersTableActions.UpdateOrder(_orderInfo);
            }
            else
            {
                comboBoxOrderState.IsEnabled = true;
                btn.Content = "Zapisz status zlecenia";
            }
        }

        private bool CheckIfDataSaved()
        {
            return comboBoxOrderState.IsEnabled || txtCarBrand.IsEnabled || txtCarModel.IsEnabled ||
                txtEngineCapacity.IsEnabled || txtNrVin.IsEnabled || txtProductionYear.IsEnabled ||
                txtRegistrationNumber.IsEnabled;
        }

        private void OrderWindow_Closing(object sender, CancelEventArgs e)
        {
            if (CheckIfDataSaved())
            {
                string msg = "Dane nie zostały zapisane. Czy chcesz na pewno zamknąć okno?";
                MessageBoxResult result =
                  MessageBox.Show(
                    msg,
                    "Dane niezapisane",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Warning);
                if (result == MessageBoxResult.No)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
