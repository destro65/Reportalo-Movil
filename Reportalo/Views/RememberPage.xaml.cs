using MySqlConnector;
using Reportalo.Services;
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
	public partial class RememberPage : ContentPage
	{
        public string id { get; set; }
        public RememberPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();


            var cmd = new MySqlCommand("select * from users where email='" + txtcorreo.Text +"'", conexion);
            var rd = cmd.ExecuteReader();



            if (rd.Read())
            {
                var toast = DependencyService.Get<IToastService>();
                toast?.ShowToast("Usuario Correcto");
                //DisplayAlert("Aviso", "Informacion Correcta", "Ok");
                id = rd.GetInt16("id").ToString();

                Navigation.PushAsync(new CambiarContraseña(id));
            }
            else
            {
                var toast = DependencyService.Get<IToastService>();
                toast?.ShowToast("Usuario No Registrado");
                //DisplayAlert("Aviso", "Usuario no Registrado", "Ok");
            }
        }
    }
}