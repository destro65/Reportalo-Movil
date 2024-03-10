using MySqlConnector;
using Plugin.LocalNotifications;
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
            var fecha = DateTime.Now.ToString("yyyy-MM-dd");
            var tiempo = DateTime.Now.ToLongTimeString();
            var creacion = fecha + tiempo;

            var cmd = new MySqlCommand("insert into multas (Hora,created_at,carro_id,ruta_id) values('"+ DateTime.Now.ToLongTimeString() + "','"+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")+ "','"+idcarro.Text+"','"+idruta.Text+"')", conexion);
            var rd = cmd.ExecuteReader();

            
            var ruta = idruta;
            var carro = idcarro;
            var conexion1 = new MySqlConnection(Properties.Resources.Conexion);
            conexion1.Open();
            var cmd1 = new MySqlCommand("select * from rutas where id='" + idruta.Text + "'", conexion1);
            var rd1 = cmd1.ExecuteReader();
            var conexion2 = new MySqlConnection(Properties.Resources.Conexion);
            conexion2.Open();
            var cmd2 = new MySqlCommand("select * from carros where id='" + idcarro.Text + "'", conexion2);
            var rd2 = cmd2.ExecuteReader();

            rd1.Read();
            String ruta1= rd1.GetString("nombre").ToString();
            rd2.Read();
            String carro1 = rd2.GetString("registro").ToString();
            

            conexion2.Close();

            CrossLocalNotifications.Current.Show("Multa Aplicada", "La unidad: "+carro1+", En la Ruta: "+ruta1+", Ha sido multada", 1, DateTime.Now.AddSeconds(5));
            //Navigation.PushAsync(new Multas());

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