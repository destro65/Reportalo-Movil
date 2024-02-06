using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reportalo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CambiarContraseña : ContentPage
	{
		public CambiarContraseña (string id)
		{
			InitializeComponent ();
            lbluid.Text = id;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("update users set password='" + txtpass.Text + "' where id = '" + lbluid.Text + "'", conexion);
            var rd = cmd.ExecuteReader();

            DisplayAlert("Aviso", "Contraseña Cambiada", "Ok");
            Navigation.PushAsync(new LoginPage());
        }
    }
}