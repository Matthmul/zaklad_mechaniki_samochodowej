using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Forms;
using MessageBox = System.Windows.Forms.MessageBox;

namespace zaklad_mechaniki_samochodowej
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationWindow.xaml
    /// </summary>
    public partial class Registration : Window
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

        public Registration()
        {
            InitializeComponent();
            Registration_Load();
        }

        private void Registration_Load()
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
            this.Hide();
            Login login = new Login();
            login.ShowDialog();
            this.Close();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtConfirmPassword.Password.ToString() != string.Empty || txtPassword.Password.ToString() != string.Empty || txtUserName.Text != string.Empty)
            {
                if (txtPassword.Password.ToString() == txtConfirmPassword.Password.ToString())
                {
                    cmd = new SqlCommand("select * from LoginTable where Username='" + txtUserName.Text + "'", cn);
                    dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        dr.Close();
                        MessageBox.Show("Użytkownik o takiej nazwie już istnieje.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        dr.Close();
                        cmd = new SqlCommand("INSERT INTO LoginTable (Username, Password) VALUES (@Username,@Password)", cn);
                        cmd.Parameters.AddWithValue("Username", txtUserName.Text);
                        cmd.Parameters.AddWithValue("Password", txtPassword.Password.ToString());
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Twoje konto zostało utworzone. Zaloguj się teraz.", "Done", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Hasła muszą się zgadzać!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
