using MySqlConnector;
using Reportalo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Reportalo.Views.Multas;

namespace Reportalo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IngresarMulta : ContentPage
    {
        public IngresarMulta()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Close();
            conexion.Open();

            var cmd = new MySqlCommand("insert into multas (Hora,created_at,carro_id,ruta_id) values('"+ DateTime.Now.ToLongTimeString() + "','"+ DateTime.Now.ToString("yyyy-MM-dd") + "','"+idcarro.Text+"','"+idruta.Text+"')", conexion);
            var rd = cmd.ExecuteReader();
            var toast = DependencyService.Get<IToastService>();
            toast?.ShowToast("Multa Ingresada correctamente");
            

        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var ruta=new List<string>();
            var carro= new List<string>();
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select nombre from rutas ", conexion);
            var rd = cmd.ExecuteReader();
            var conexion1 = new MySqlConnection(Properties.Resources.Conexion);
            conexion1.Open();
            var cmd1 = new MySqlCommand("select registro from carros ", conexion1);
            var rd1 = cmd1.ExecuteReader();


            while (rd.Read())
            {
                ruta.Add(rd.GetString("nombre"));
            }
            rd.Close();
            ComboRuta.ItemsSource = ruta;

            while (rd1.Read())
            {
                carro.Add(rd1.GetString("registro"));
            }
            rd.Close();
            ComboCarro.ItemsSource = carro;

            conexion.Close();
        }
        private void ComboRuta_SelectedIndexChanged(object sender, EventArgs e)
        {
            string listaruta=ComboRuta.SelectedItem.ToString();
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Close();

            conexion.Open();
            var cmd = new MySqlCommand("select * from rutas where nombre='" +listaruta+ "'", conexion);
            var rd = cmd.ExecuteReader();

            if(rd.Read())
            {
                idruta.Text=rd.GetInt32(0).ToString();
            }
                       
        }

        private void ComboCarro_SelectedIndexChanged(object sender, EventArgs e)
        {
            string listacarro = ComboCarro.SelectedItem.ToString();
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Close();

            conexion.Open();
            var cmd = new MySqlCommand("select * from carros where registro='" + listacarro + "'", conexion);
            var rd = cmd.ExecuteReader();

            if (rd.Read())
            {
                idcarro.Text = rd.GetInt32(0).ToString();
            }
            rd.Close();
        }
    }
}