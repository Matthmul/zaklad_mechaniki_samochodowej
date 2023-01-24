using System;
using System.Windows;
using System.Windows.Controls;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy AccountsService.xaml
    /// </summary>
    public partial class AccountsService : Window
    {
        public AccountsService()
        {
            InitializeComponent();
            LoadNewUsers();
        }

        private void LoadNewUsers()
        {
            var newUsersList = LoginTableActions.GetAllNewUsers();

            foreach (var user in newUsersList)
            {
                ListViewItem lvi = new()
                {
                    Tag = user.Id,
                    Content = user.Username
                };
                listViewNewAccounts.Items.Add(lvi);
            }
        }

        private void ButtonRefreshNewAccounts_Click(object sender, RoutedEventArgs e)
        {
            listViewNewAccounts.Items.Clear();
            LoadNewUsers();
        }

        private void ButtonSearchAccount_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ListViewFoundAccounts_DoubleClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void ListViewNewAccounts_DoubleClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
