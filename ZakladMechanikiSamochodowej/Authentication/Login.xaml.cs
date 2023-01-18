using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace ZakladMechanikiSamochodowej.Authentication
{
    /// <summary>
    /// Logika interakcji dla klasy Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

        public Login()
        {
            InitializeComponent();
            Login_Load();
        }

        private void Login_Load()
        {
            string dirStr = AppDomain.CurrentDomain.BaseDirectory;
            var dir = Directory.GetParent(dirStr);
            if (dir != null) { 
                while (dir.Parent != null && dir.Parent.Exists)
                {
                    if (dir.GetFiles("Database.mdf").Length != 0)
                    {
                        dirStr = dir.ToString() + "\\Database.mdf";
                        break;
                    }
                    dir = dir.Parent;
                }
                cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dirStr + ";Integrated Security=True");
                cn.Open();
            }
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            
            if (txtPassword.Password.ToString() != string.Empty || txtUserName.Text != string.Empty)
            {

                cmd = new SqlCommand("select * from LoginTable where Username='" + txtUserName.Text + "' and Password='" + txtPassword.Password.ToString() + "'", cn);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    Properties.Settings.Default.UserName = txtUserName.Text;

                    Hide();
                    if (txtUserName.Text == "admin")
                    {
                        Admin.HomeAdmin home = new Admin.HomeAdmin();
                        home.ShowDialog();
                    }
                    else
                    {
                        Client.HomeClient home = new Client.HomeClient();
                        home.ShowDialog();
                    }
                    this.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Nie istnieje użytkownik o takim loginie lub haśle.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Registration registration = new Registration();
            registration.ShowDialog();
            this.Close();
        }
    }
}
