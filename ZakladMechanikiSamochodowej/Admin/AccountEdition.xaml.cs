using System.Windows;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy AccountEdition.xaml
    /// </summary>
    public partial class AccountEdition : Window
    {
        User _user;
        public AccountEdition(User user)
        {
            InitializeComponent();
            _user = user;
        }
    }
}
