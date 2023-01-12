using System.Data.SqlClient;
using System.Windows;

namespace zaklad_mechaniki_samochodowej
{
    /// <summary>
    /// Logika interakcji dla klasy HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        SqlConnection cn;

        public HomeAdmin()
        {
            InitializeComponent();
            Home_Load();
            AddText();
        }

        private void Home_Load()
        {
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=H:\zaklad_mechaniki_samochodowej\zaklad_mechaniki_samochodowej\Database.mdf;Integrated Security=True");
            cn.Open();
        }

        private void AddText()
        {
            txtnapis.Text = "Admininie co podać?";
        }
    }
}
