using System;
using System.ComponentModel;
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
        private HomeAdmin _homeAdmin;
        bool savedActions = true;

        public OrderHandling(Order orderInfo, HomeAdmin ha)
        {
            InitializeComponent();
            _orderInfo = orderInfo;
            _homeAdmin = ha;

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

        private void AddActionToBeDone(string action, ListBox lb)
        {
            ListBoxItem lbi = new()
            {
                Content = action
            };
            ContextMenu cm = new();
            MenuItem mi = new()
            {
                Header = "Wykonano"
            };
            mi.Click += DoneOrNotDone_Click;
            cm.Items.Add(mi);
            lbi.ContextMenu = cm;
            lb.Items.Add(lbi);
        }

        private void AddActionDone(string action, ListBox lb)
        {
            ListBoxItem lbi = new()
            {
                Content = action
            };
            lb.Items.Add(lbi);
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

            UpdateActionsToDo();
            UpdateActionsDone();
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

                DisableCarEdit();
                btn.Content = "Edytuj dane samochodu";

                OrdersTableActions.UpdateOrder(_orderInfo);
            }
            else
            {

                EnableCarEdit();
                btn.Content = "Zapisz dane samochodu";
            }
        }

        private void EnableCarEdit()
        {
            txtCarBrand.IsEnabled = true;
            txtCarModel.IsEnabled = true;
            txtEngineCapacity.IsEnabled = true;
            txtNrVin.IsEnabled = true;
            txtProductionYear.IsEnabled = true;
            txtRegistrationNumber.IsEnabled = true;
        }

        private void DisableCarEdit()
        {
            txtCarBrand.IsEnabled = false;
            txtCarModel.IsEnabled = false;
            txtEngineCapacity.IsEnabled = false;
            txtNrVin.IsEnabled = false;
            txtProductionYear.IsEnabled = false;
            txtRegistrationNumber.IsEnabled = false;
        }

        private void SetNewOrderState(string newState)
        {
            _orderInfo.OrderState = (OrderState)Enum.Parse(typeof(OrderState), newState);
            OrdersTableActions.UpdateOrder(_orderInfo);
            comboBoxOrderState.SelectedIndex = (int)_orderInfo.OrderState;
        }

        private void ButtonEditOrderState_Click(object sender, RoutedEventArgs e)
        {
            Button btn = (Button)sender;

            if (comboBoxOrderState.IsEnabled)
            {
                comboBoxOrderState.IsEnabled = false;
                btn.Content = "Edytuj status zlecenia";
                string selectedVal = (string)comboBoxOrderState.SelectedValue;
                SetNewOrderState(selectedVal);
            }
            else
            {
                comboBoxOrderState.IsEnabled = true;
                btn.Content = "Zapisz status zlecenia";
            }
        }

        private void UpdateActionsDone()
        {
            listBoxDoneActions.Items.Clear();

            if (!_orderInfo.Fix)
            {
                AddActionDone("Fix", listBoxDoneActions);
            }
            if (!_orderInfo.Review)
            {
                AddActionDone("Review", listBoxDoneActions);
            }
            if (!_orderInfo.Assembly)
            {
                AddActionDone("Assembly", listBoxDoneActions);
            }
            if (!_orderInfo.TechnicalConsultation)
            {
                AddActionDone("TechnicalConsultation", listBoxDoneActions);
            }
            if (!_orderInfo.Training)
            {
                AddActionDone("Training", listBoxDoneActions);
            }
            if (!_orderInfo.OrderingParts)
            {
                AddActionDone("OrderingParts", listBoxDoneActions);
            }
        }

        private void UpdateActionsToDo()
        {
            listBoxActions.Items.Clear();

            if (_orderInfo.Fix)
            {
                AddActionToBeDone("Fix", listBoxActions);
            }
            if (_orderInfo.Review)
            {
                AddActionToBeDone("Review", listBoxActions);
            }
            if (_orderInfo.Assembly)
            {
                AddActionToBeDone("Assembly", listBoxActions);
            }
            if (_orderInfo.TechnicalConsultation)
            {
                AddActionToBeDone("TechnicalConsultation", listBoxActions);
            }
            if (_orderInfo.Training)
            {
                AddActionToBeDone("Training", listBoxActions);
            }
            if (_orderInfo.OrderingParts)
            {
                AddActionToBeDone("OrderingParts", listBoxActions);
            }
        }

        private void ButtonSaveDoneActions_Click(object sender, RoutedEventArgs e)
        {
            OrdersTableActions.UpdateOrder(_orderInfo);
            string msg = "Dane zostały zapisane.";
            MessageBox.Show(
              msg,
              "Dane zapisane",
              MessageBoxButton.OK,
              MessageBoxImage.Information);
            savedActions = true;
            UpdateActionsToDo();

            if (listBoxActions.Items.Count == 0 && _orderInfo.OrderState != OrderState.CLOSED)
            {
                SetNewOrderState(OrderState.DONE.ToString());
            }
            else if (listBoxDoneActions.Items.Count == 0 && _orderInfo.OrderState == OrderState.DONE)
            {
                SetNewOrderState(OrderState.IN_PROGRESS.ToString());
            }
        }

        private void ListBoxActions_DoubleClick(object sender, RoutedEventArgs e)
        {
            ListBox lb = ((ListBox)sender);
            if (lb.SelectedItem != null)
            {
                object item = ((ListBoxItem)lb.SelectedItem).ContextMenu.Items.GetItemAt(0);
                DoneOrNotDone_Click(item, e);
            }
        }

        private void DoneOrNotDone_Click(object sender, RoutedEventArgs e)
        {
            MenuItem mi = (MenuItem)sender;
            ListBoxItem? lbi = null;
            if ((string)mi.Header == "Wykonano" && listBoxActions.SelectedItem != null)
            {
                lbi = (ListBoxItem)listBoxActions.SelectedItem;
                listBoxActions.Items.Remove(lbi);
                mi.Header = "Nie wykonano";
                listBoxDoneActions.Items.Add(lbi);
            }
            else if (listBoxDoneActions.SelectedItem != null)
            {
                lbi = (ListBoxItem)listBoxDoneActions.SelectedItem;
                listBoxDoneActions.Items.Remove(lbi);
                mi.Header = "Wykonano";
                listBoxActions.Items.Add(lbi);
            }

            if (lbi != null)
            {
                SetDoneAction(lbi.Content.ToString());
            }
        }

        private void SetDoneAction(string? action)
        {
            if (action != null)
            {
                savedActions = false;

                switch (action)
                {
                    case "Fix":
                        _orderInfo.Fix = !_orderInfo.Fix;
                        break;
                    case "Review":
                        _orderInfo.Review = !_orderInfo.Review;
                        break;
                    case "Assembly":
                        _orderInfo.Assembly = !_orderInfo.Assembly;
                        break;
                    case "TechnicalConsultation":
                        _orderInfo.TechnicalConsultation = !_orderInfo.TechnicalConsultation;
                        break;
                    case "Training":
                        _orderInfo.Training = !_orderInfo.Training;
                        break;
                    case "OrderingParts":
                        _orderInfo.OrderingParts = !_orderInfo.OrderingParts;
                        break;
                    default:
                        MessageBox.Show(
                          "Error",
                          "Nieznana akcja.",
                          MessageBoxButton.OK,
                          MessageBoxImage.Error);
                        break;
                }
            }
            else
            {
                MessageBox.Show(
                  "Error",
                  "Nieznana akcja.",
                  MessageBoxButton.OK,
                  MessageBoxImage.Error);
            }
        }

        private bool CheckIfDataSaved()
        {
            return comboBoxOrderState.IsEnabled || txtCarBrand.IsEnabled || txtCarModel.IsEnabled ||
                txtEngineCapacity.IsEnabled || txtNrVin.IsEnabled || txtProductionYear.IsEnabled ||
                txtRegistrationNumber.IsEnabled || !savedActions;
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
                    return;
                }
            }
            _homeAdmin.RefreshOrders();
        }

        private void ButtonUserInfo_Click(object sender, RoutedEventArgs e)
        {
            User? user = LoginTableActions.GetUserById(_orderInfo.ClientId);
            if (user != null) 
            {
                AccountEdition accountEdition = new(user);
                accountEdition.Show();
            }
        }
    }
}
