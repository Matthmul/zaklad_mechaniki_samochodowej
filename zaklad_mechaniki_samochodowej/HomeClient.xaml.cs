using System.Windows;

namespace zaklad_mechaniki_samochodowej
{
    /// <summary>
    /// Logika interakcji dla klasy HomeClient.xaml
    /// </summary>
    public partial class HomeClient : Window
    {
        public HomeClient()
        {
            InitializeComponent();
            AddText();
        }

        private void AddText()
        {
            txtnapis.Text = "Tu coś kiedyś będzie Kliencie :)";
        }
    }
}
