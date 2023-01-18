using System;
using System.Data.SqlClient;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace zaklad_mechaniki_samochodowej
{
    /// <summary>
    /// Logika interakcji dla klasy HomeAdmin.xaml
    /// </summary>
    public partial class HomeAdmin : Window
    {
        SqlCommand cmd;
        SqlConnection cn;
        SqlDataReader dr;

        public HomeAdmin()
        {
            InitializeComponent();
            Home_Load();
            LoadOrders(listBoxOrdes);
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

        private void ButtonClientModification_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AddFakeOrders()
        {
            cmd = new SqlCommand("INSERT INTO OrderTable (ClientId, Brand, Model, Fix, NrVIN, ProductionYear, RegistrationNumber, EngineCapacity, OrderState) VALUES (@ClientId, @Brand, @Model, @Fix, @NrVIN, @ProductionYear, @RegistrationNumber, @EngineCapacity, @OrderState)", cn);
            cmd.Parameters.AddWithValue("ClientId", 1);
            cmd.Parameters.AddWithValue("Brand", "Opel");
            cmd.Parameters.AddWithValue("Model", "Astra");
            cmd.Parameters.AddWithValue("Fix", 1);
            cmd.Parameters.AddWithValue("NrVIN", "565456564");
            cmd.Parameters.AddWithValue("ProductionYear", 1);
            cmd.Parameters.AddWithValue("RegistrationNumber", "AS231356");
            cmd.Parameters.AddWithValue("EngineCapacity", 1);
            cmd.Parameters.AddWithValue("OrderState", OrderState.NEW.ToString());
            cmd.ExecuteNonQuery();
            cmd.ExecuteNonQuery();
        }

        private void LoadOrders(ListBox lb)
        {
            AddFakeOrders(); // TODO Usunąć to potem

            cmd = new SqlCommand("select * from OrderTable where OrderState='" + OrderState.NEW.ToString() + "'", cn);
            using (dr = cmd.ExecuteReader())
            {
                if (dr != null)
                {
                    while (dr.Read())
                    {
                        ListBoxItem lbi = new ListBoxItem
                        {
                            Tag = dr["Id"],
                            Content = "Zlecenie na " + dr["Brand"] + " " + dr["Model"]
                        };
                        lb.Items.Add(lbi);
                    }
                }
            }

            cmd = new SqlCommand("select COUNT(*) as OrdersNumber from OrderTable where OrderState='" + OrderState.NEW.ToString() + "'", cn);
            using (dr = cmd.ExecuteReader())
            {
                if (dr != null && dr.Read())
                {
                    var numNewOrders = dr["OrdersNumber"];
                    txtNewOrderNumber.Text = numNewOrders.ToString();
                }
            }

            cmd = new SqlCommand("select COUNT(*) as OrdersNumber from OrderTable where OrderState='" + OrderState.IN_PROGRESS.ToString() + "'", cn);
            using (dr = cmd.ExecuteReader())
            {
                if (dr != null && dr.Read())
                {
                    var numNewOrders = dr["OrdersNumber"];
                    txtInProgressOrderNumber.Text = numNewOrders.ToString();
                }
            }

            cmd = new SqlCommand("select COUNT(*) as OrdersNumber from OrderTable where OrderState='" + OrderState.CLOSED.ToString() + "'", cn);
            using (dr = cmd.ExecuteReader())
            {
                if (dr != null && dr.Read())
                {
                    var numNewOrders = dr["OrdersNumber"];
                    txtClosedOrderNumber.Text = numNewOrders.ToString();
                }
            }
        }

        private void ListBoxOrders_DoubleClick(object sender, RoutedEventArgs e)
        {
            ListBox lb = ((ListBox)sender);
            if (lb.SelectedItem != null)
            {
                ListBoxItem lbi = ((ListBoxItem)lb.SelectedItem);
                MessageBox.Show(lbi.Tag.ToString());
            }
        }

        private void ButtonRefreshOrder_Click(object sender, RoutedEventArgs e)
        {
            listBoxOrdes.Items.Clear();

            LoadOrders(listBoxOrdes);
        }
    }
}
