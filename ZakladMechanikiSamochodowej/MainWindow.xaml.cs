using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using ZakladMechanikiSamochodowej.Authentication;

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
            SqlCommand cmd;
            SqlConnection cn;
            SqlDataReader dr;

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

            cmd = new SqlCommand("select * from LoginTable where Username='admin'", cn);
            dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                dr.Close();
            }
            else
            {
                dr.Close();
                cmd = new SqlCommand("INSERT INTO LoginTable (Username, Password, IsAdmin) VALUES (@Username,@Password,@IsAdmin)", cn);
                cmd.Parameters.AddWithValue("Username", "admin");
                cmd.Parameters.AddWithValue("Password", "admin");
                cmd.Parameters.AddWithValue("IsAdmin", 1);
                cmd.ExecuteNonQuery();
            }
            this.Hide();
            Login login = new();
            login.ShowDialog();
            this.Close();
        }
    }
}
