using System.Windows;
using ZakladMechanikiSamochodowej.Database.DatabaseActions;

namespace ZakladMechanikiSamochodowej.Authentication
{
	/// <summary>
	/// Logika interakcji dla klasy Login.xaml
	/// </summary>
	public partial class Login : Window
	{
		public Login()
		{
			InitializeComponent();
		}

		private void ButtonLogin_Click(object sender, RoutedEventArgs e)
		{
			
			if (txtPassword.Password.ToString() != string.Empty || txtUserName.Text != string.Empty)
			{
				var user = LoginTableActions.TryGetUserByName(txtUserName.Text);
				if (user != null && user.Password == txtPassword.Password.ToString())
				{
					Properties.Settings.Default.UserName = txtUserName.Text;

					Hide();
					if (txtUserName.Text == "admin")
					{
						Admin.HomeAdmin home = new();
						home.ShowDialog();
					}
					else
					{
						Client.HomeClient home = new();
						home.ShowDialog();
					}
					this.Close();
				}
				else
				{
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
