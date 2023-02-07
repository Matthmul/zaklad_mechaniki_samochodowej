using System.Windows;
using ZakladMechanikiSamochodowej.Authentication;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitDatabase();
        }
        private void InitDatabase()
        {
            if (LoginTableActions.TryGetUserByName("admin") == null)
            {
                LoginTableActions.SaveUser(new User
                    ("admin", "admin")
                {
                    IsAdmin = true
                });
            }

            Hide();
            Login login = new();
            login.ShowDialog();
            Close();
        }
    }
}
