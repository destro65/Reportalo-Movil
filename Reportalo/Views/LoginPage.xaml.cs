using MySqlConnector;
using Reportalo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;


namespace Reportalo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginPage : ContentPage
	{
        public string id { get; set; }

        public LoginPage ()
		{
			InitializeComponent ();
            NavigationPage.SetHasBackButton(this, false);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

			Navigation.PushAsync (new RegisterPage ());
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();


            var cmd = new MySqlCommand("select * from users where email='" + txtuser.Text + "'and password='" + txtpass.Text + "'", conexion);
            var rd = cmd.ExecuteReader();



            if (rd.Read())
            {
                var toast = DependencyService.Get<IToastService>();
                toast?.ShowToast("Usuario Logeado");
                //DisplayAlert("Aviso", "Informacion Correcta", "Ok");
                id = rd.GetInt16("id").ToString();

                Navigation.PushAsync(new Menu(id));
            }
            else
            {
                var toast = DependencyService.Get<IToastService>();
                toast?.ShowToast("Usuario No Registrado");
                //DisplayAlert("Aviso", "Usuario no Registrado", "Ok");
            }
                        
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RegisterPage());
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new RememberPage());
        }
    }
}