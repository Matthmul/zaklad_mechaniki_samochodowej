using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace zaklad_mechaniki_samochodowej
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

                    this.Hide();
                    if (txtUserName.Text == "admin")
                    {
                        HomeAdmin home = new HomeAdmin();
                        home.ShowDialog();
                    }
                    else
                    {
                        HomeClient home = new HomeClient();
                        home.ShowDialog();
                    }
                    this.Close();
                }
                else
                {
                    dr.Close();
                    MessageBox.Show("Nie istnieje użytkownik o takim loginie lub haśle.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
