using System.Data.SqlClient;
using System.IO;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;

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
            AddComboBoxBrands();
        }

        private void AddText()
        {
            txtUserName.Text = Properties.Settings.Default.UserName;
        }

        private void AddComboBoxBrands()
        {
            List<string> brands = new List<string> { "Opel Mazda", "Mercedes", "Toyota", "Suzuki" , "Ford" }; // TODO Dodać zczytywanie rodzajów z bazy danych

            foreach (string brand in brands)
            {
                comboBoxCarBrand.Items.Add(brand);
            }
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            txtCarModel.Text = string.Empty;
            txtCarModel.GotFocus -= RemoveText;
        }
    }
}
