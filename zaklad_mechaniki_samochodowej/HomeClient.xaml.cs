using System.Data.SqlClient;
using System.IO;
using System;
using System.Windows;

namespace zaklad_mechaniki_samochodowej
{
    /// <summary>
    /// Logika interakcji dla klasy HomeClient.xaml
    /// </summary>
    public partial class HomeClient : Window
    {
        SqlConnection cn;

        public HomeClient()
        {
            InitializeComponent();
            Home_Load();
            AddText();
        }

        private void AddText()
        {
            txtuser.Text = Properties.Settings.Default.nazwa_uzytkownika;
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

/*        private void AddText()
        {
            txtnapis.Text = "Admininie co podać?";
        }*/

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
