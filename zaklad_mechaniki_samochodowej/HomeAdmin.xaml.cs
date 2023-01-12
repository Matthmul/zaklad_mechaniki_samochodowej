using System;
using System.Data.SqlClient;
using System.IO;
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
            string dirStr = AppDomain.CurrentDomain.BaseDirectory;
            var dir = Directory.GetParent(dirStr);
            while (dir.Parent.Exists)
            {
                if (dir.GetFiles("Database.mdf").Length != 0)
                {
                    dirStr = dir.ToString() + "\\Database.mdf";
                    break;
                }
                dir = dir.Parent;
            }
            if (!dir.Parent.Exists)
            {
                return;
            }
            cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dirStr + ";Integrated Security=True");
            cn.Open();
        }

        private void AddText()
        {
            txtnapis.Text = "Admininie co podać?";
        }
    }
}
