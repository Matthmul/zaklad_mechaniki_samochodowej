using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
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
            LoadFindedUsers();
        }

        private void LoadNewUsers()
        {
            listViewNewAccounts.ItemsSource = LoginTableActions.GetAllNewUsers();
            CollectionView viewNew = (CollectionView)CollectionViewSource.GetDefaultView(listViewNewAccounts.ItemsSource);
            viewNew.SortDescriptions.Clear();
            viewNew.SortDescriptions.Add(new SortDescription("Username", ListSortDirection.Ascending));
        }

        private void LoadFindedUsers(List<User>? users = null)
        {
            listViewFoundAccounts.ItemsSource = (users != null) ? users : LoginTableActions.GetAllAcceptedUsers();
            
            CollectionView viewFound = (CollectionView)CollectionViewSource.GetDefaultView(listViewFoundAccounts.ItemsSource);
            viewFound.SortDescriptions.Clear();
            viewFound.SortDescriptions.Add(new SortDescription("Username", ListSortDirection.Ascending));
        }

        private void ListViewNewAccountsAscending_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewNewAccounts.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Username", ListSortDirection.Ascending));
        }

        private void ListViewNewAccountsDescending_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewNewAccounts.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Username", ListSortDirection.Descending));
        }

        private void ListViewFoundAccountsAscending_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewFoundAccounts.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Username", ListSortDirection.Ascending));
        }

        private void ListViewFoundAccountsDescending_Click(object sender, RoutedEventArgs e)
        {
            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(listViewFoundAccounts.ItemsSource);
            view.SortDescriptions.Clear();
            view.SortDescriptions.Add(new SortDescription("Username", ListSortDirection.Descending));
        }

        private void ButtonRefreshNewAccounts_Click(object sender, RoutedEventArgs e)
        {
            LoadNewUsers();
        }

        private void ButtonSearchAccount_Click(object sender, RoutedEventArgs e)
        {
            List<User> listUsers = LoginTableActions.FindMatchingAcceptedUsers(searchPhrase.Text);
            LoadFindedUsers(listUsers);
        }

        private void ButtonEditUser_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)((Button)sender).DataContext;
            AccountEdition accountEdition = new(user);
            accountEdition.Show();
        }

        private void ButtonDeleteUser_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)((Button)sender).DataContext;
            if (user.IsAdmin)
            {
                MessageBox.Show(
                    "Nie można usunąć konta administratora.",
                    "Zakazana akcja",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return;
            }
            LoginTableActions.RemoveUser(user);

            LoadNewUsers();
            LoadFindedUsers();
        }

        private void ButtonAcceptUser_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)((Button)sender).DataContext;
            LoginTableActions.AcceptUser(user);

            LoadNewUsers();
            LoadFindedUsers();
        }
    }
}
