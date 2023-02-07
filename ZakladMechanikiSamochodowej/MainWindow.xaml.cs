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
            Hide();
            Login login = new();
            login.ShowDialog();
            Close();
        }
    }
}
