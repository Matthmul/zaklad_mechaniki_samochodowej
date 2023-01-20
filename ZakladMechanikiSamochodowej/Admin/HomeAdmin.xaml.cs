using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        public HomeAdmin()
        {
            InitializeComponent();
            LoadOrders(listBoxOrdes);
        }

        private void ButtonClientModification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFakeOrders()
        {
            OrdersTableActions.SaveOrder(new Order
            {
                ClientId = 1,
                Brand = "Opel",
                Model = "Astra",
                Fix = true,
                NrVIN = "565456564",
                ProductionYear = 1,
                RegistrationNumber = "AS231356",
                EngineCapacity = 1,
                OrderState = OrderState.NEW
            });
        }

        private void LoadOrders(ListBox lb)
        {
            //AddFakeOrders(); // TODO Usunąć to potem

            List<Order> newOrders = OrdersTableActions.GetOrdersByOrderState(OrderState.NEW);

            txtNewOrderNumber.Text = newOrders.Count.ToString();
            txtInProgressOrderNumber.Text = OrdersTableActions.GetOrdersByOrderState(OrderState.IN_PROGRESS).Count.ToString();
            txtClosedOrderNumber.Text = OrdersTableActions.GetOrdersByOrderState(OrderState.CLOSED).Count.ToString();

            foreach (var order in newOrders)
            {
                ListBoxItem lbi = new()
                {
                    Tag = order.Id,
                    Content = "Zlecenie na " + order.Brand + " " + order.Model
                };
                lb.Items.Add(lbi);
            }
        }

        private void ListBoxOrders_DoubleClick(object sender, RoutedEventArgs e)
        {
            ListBox lb = ((ListBox)sender);
            if (lb.SelectedItem != null)
            {
                ListBoxItem lbi = ((ListBoxItem)lb.SelectedItem);
                MessageBox.Show(lbi.Tag.ToString());
            }
        }

        private void ButtonRefreshOrder_Click(object sender, RoutedEventArgs e)
        {
            listBoxOrdes.Items.Clear();

            LoadOrders(listBoxOrdes);
        }
    }
}
