using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;

namespace ZakladMechanikiSamochodowej.Authentication
{
    /// <summary>
    /// Logika interakcji dla klasy RegistrationWindow.xaml
    /// </summary>
    public partial class Registration : Window
    {
        private SqlCommand _cmd;
        private SqlConnection _cn;
        private SqlDataReader _dr;

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
            _cn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=" + dirStr + ";Integrated Security=True");
            _cn.Open();
        }

        private void ButtonLogin_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Login login = new();
            login.ShowDialog();
            this.Close();
        }

        private void ButtonRegister_Click(object sender, RoutedEventArgs e)
        {
            if (txtConfirmPassword.Password.ToString() != string.Empty || txtPassword.Password.ToString() != string.Empty || txtUserName.Text != string.Empty)
            {
                if (txtPassword.Password.ToString() == txtConfirmPassword.Password.ToString())
                {
                    _cmd = new SqlCommand("select * from LoginTable where Username='" + txtUserName.Text + "'", _cn);
                    _dr = _cmd.ExecuteReader();
                    if (_dr.Read())
                    {
                        _dr.Close();
                        MessageBox.Show("Użytkownik o takiej nazwie już istnieje.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        _dr.Close();
                        _cmd = new SqlCommand("INSERT INTO LoginTable (Username, Password) VALUES (@Username,@Password)", _cn);
                        _cmd.Parameters.AddWithValue("Username", txtUserName.Text);
                        _cmd.Parameters.AddWithValue("Password", txtPassword.Password.ToString());
                        _cmd.ExecuteNonQuery();
                        MessageBox.Show("Twoje konto zostało utworzone. Zaloguj się teraz.", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Hasła muszą się zgadzać!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Wszystkie pola muszą być wypełnione.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
