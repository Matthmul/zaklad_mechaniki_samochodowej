﻿using System;
using System.Collections.Generic;
using System.Linq;
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
        private List<Order>[] _orders = new List<Order>[Enum.GetNames(typeof(OrderState)).Length];

        public HomeAdmin()
        {
            InitializeComponent();
            LoadOrders(listBoxOrdes);
        }

        private void ButtonClientModification_Click(object sender, RoutedEventArgs e)
        {

        }
/*
        private void AddFakeOrders()
        {
            OrdersTableActions.SaveOrder(new Order
                (1, "Opel", "Astra", "565456564", 1, "AS231356", 1, OrderState.NEW)
            {
                Fix = true
            });
        }*/

        private void LoadOrders(ListBox lb)
        {
            //AddFakeOrders(); // TODO Usunąć to potem

            _orders[(int)OrderState.NEW] = ProvideOrder(lb, OrderState.NEW, txtNewOrderNumber);
            _orders[(int)OrderState.IN_PROGRESS] = ProvideOrder(lb, OrderState.IN_PROGRESS, txtInProgressOrderNumber);
            _orders[(int)OrderState.CLOSED] = ProvideOrder(lb, OrderState.CLOSED, txtClosedOrderNumber);
        }

        private List<Order> ProvideOrder(ListBox lb, OrderState orderState, TextBlock txtNumber)
        {
            List<Order> orderList = OrdersTableActions.GetOrdersByOrderState(orderState);

            txtNumber.Text = orderList.Count.ToString();

            foreach (var order in orderList)
            {
                ListBoxItem lbi = new()
                {
                    Tag = new Tuple<int, OrderState>(order.Id, orderState),
                    Content = "Zlecenie na " + order.Brand + " " + order.Model
                };
                lb.Items.Add(lbi);
            }

            return orderList;
        }

        private void ListBoxOrders_DoubleClick(object sender, RoutedEventArgs e)
        {
            ListBox lb = ((ListBox)sender);
            if (lb.SelectedItem != null)
            {
                Tuple<int, OrderState> tagTup = (Tuple<int, OrderState>)((ListBoxItem)lb.SelectedItem).Tag;
                try
                {
                    var ordersTab = _orders[(int)tagTup.Item2];
                    var order = ordersTab.SingleOrDefault(o => o.Id == tagTup.Item1, new Order
                        (0, "-----", "-----", "------", 0, "-------", 0, OrderState.CLOSED));
                    OrderHandling home = new(order);
                    home.Show();
                }
                catch
                {
                    MessageBox.Show("Błąd podczas ładowania zlecenia.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ButtonRefreshOrder_Click(object sender, RoutedEventArgs e)
        {
            listBoxOrdes.Items.Clear();

            LoadOrders(listBoxOrdes);
        }
    }
}
