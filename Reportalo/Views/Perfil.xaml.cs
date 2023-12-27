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
    public partial class Perfil : ContentPage
    {
        private object usuario;

        public Perfil(string usuario)
        {
            
            InitializeComponent();
            lbluser.Text = usuario;

            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select * from users where id = '" + lbluser.Text + "'", conexion);
            var rd = cmd.ExecuteReader();
            rd.Read();
            txtuser.Text = rd.GetString("name").ToString();
            txtmail.Text = rd.GetString("email").ToString();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("update users set password='"+txtclave.Text+"' where id = '" + lbluser.Text + "'", conexion);
            var rd = cmd.ExecuteReader();

            DisplayAlert("Aviso", "Contraseña Cambiada", "Ok");
            Navigation.PushAsync(new LoginPage());
        }
    }
}