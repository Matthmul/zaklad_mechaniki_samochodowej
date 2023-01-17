using System.Data.SqlClient;
using System.IO;
using System;
using System.Windows;
using System.Windows.Documents;
using System.Collections.Generic;
using System.Linq;
/*using System.Windows.Forms;*/
using MessageBox = System.Windows.Forms.MessageBox;

namespace zaklad_mechaniki_samochodowej
{
    /// <summary>
    /// Logika interakcji dla klasy HomeClient.xaml
    /// </summary>
    public partial class HomeClient : Window
    {
        SqlConnection cn;
        SqlCommand cmd;
        SqlDataReader dr;

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
            List<string> brands = new List<string> { "Opel", "Mazda", "Mercedes", "Toyota", "Suzuki" , "Ford" }; // TODO Dodać zczytywanie rodzajów z bazy danych

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
            //pobieramuy id uzytkownika 
            string sql = "select id from LoginTable where Username='" + Properties.Settings.Default.UserName+ "'";
            cmd = new SqlCommand(sql, cn);
            dr = cmd.ExecuteReader();
            int clientID = 0;
            if (dr.Read())
            {
                 clientID = Convert.ToInt32(dr["Id"]);
            }
            //mozna dodać czy ktos był juz klientem
            //tutaj przetworzyć któe checkboxy są kliknięte

            //model=comboBoxCarBrand.selectedItem.ToString();

            if (txtNrVin.Text.ToString() != string.Empty || txtCarModel.Text.ToString() != string.Empty || txtProductionYear.Text.ToString() != string.Empty
                || txtEngineCapacity.Text.ToString() != string.Empty || txtRegistrationNumber.Text.ToString() != string.Empty)
            {
                string sql2 = string.Format("INSERT INTO OrderTable(CLientId,Brand,Model, Fix, Review, Assembly, TechnicalConsultation, Training, OrderingParts, OrderState, NrVIN, ProductionYear, RegistrationNumber,EngineCapacity) VALUES({0}, {1}, {2}, {3}, {4}, {5}, {6}, {7})",
                  clientID, comboBoxCarBrand.SelectedItem.ToString(), txtCarModel.Text.ToString(), false,false,false,false, false,false,0, txtNrVin.Text.ToString(), txtProductionYear.Text.ToString(), txtRegistrationNumber.Text.ToString(), txtEngineCapacity.Text.ToString() );

                cmd = new SqlCommand(sql2, cn);

                try
                {
                    cmd.ExecuteNonQuery();
                    /*MessageBox.Show("Wszystkie pola muszą być wypełnioness.", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                }
                catch (SqlException ex)
                {
                   /* MessageBox.Show(ex.Message.ToString(), "Blad podczas wykonywania zapytania");*//**/
                  /*  MessageBox.Show("Wszystkie pola muszą być wypełnione.","Error", MessageBoxButtons.OK, MessageBoxIcon.Error);*/
                }



            }
            /* else
             {
                 MessageBox.Show("Wszystkie pola muszą być wypełnione.", MessageBoxButtons.OK, MessageBoxIcon.Error);
             }*/
        }


        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveText(object sender, RoutedEventArgs e)
        {
            txtCarModel.Text = string.Empty;
            txtCarModel.GotFocus -= RemoveText;
        }

        private void txtEngineCapacity_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtRegistrationNumber_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtPRoductionYear_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void txtNrVIN_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
