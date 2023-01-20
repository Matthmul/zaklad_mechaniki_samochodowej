using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZakladMechanikiSamochodowej.Database.DatabaseModels;

namespace ZakladMechanikiSamochodowej.Admin
{
    /// <summary>
    /// Logika interakcji dla klasy OrderHandling.xaml
    /// </summary>
    public partial class OrderHandling : Window
    {
        private Order _orderInfo;

        public OrderHandling(Order orderInfo)
        {
            InitializeComponent();
            _orderInfo = orderInfo;

            LoadOrder();
        }

        private void LoadOrder()
        {
            txtCarBrand.Text = _orderInfo.Brand ?? "---------";
            txtCarModel.Text = _orderInfo.Model ?? "---------";
            txtEngineCapacity.Text = _orderInfo.EngineCapacity.ToString();
            txtNrVin.Text = _orderInfo.NrVIN ?? "---------";
            txtProductionYear.Text = _orderInfo.ProductionYear.ToString();
            txtRegistrationNumber.Text = _orderInfo.RegistrationNumber ?? "---------";
        }
    }
}
