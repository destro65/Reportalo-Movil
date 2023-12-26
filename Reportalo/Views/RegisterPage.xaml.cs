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
	public partial class RegisterPage : ContentPage
	{
		public RegisterPage ()
		{
			InitializeComponent ();
		}

        private void Button_Clicked(object sender, EventArgs e)
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();


            var cmd = new MySqlCommand("insert into users(name,email,password) values('"+txtname.Text+"','"+txtmail.Text+"','"+txtpass.Text+"')", conexion);
            var rd = cmd.ExecuteReader();
            conexion.Close();
            DisplayAlert("Alerta", "Usuario Registrado con exito", "ok");
            Navigation.PushAsync(new LoginPage());


        }
    }
}