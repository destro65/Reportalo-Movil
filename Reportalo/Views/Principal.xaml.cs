using MySqlConnector;
using Reportalo.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reportalo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        public string id { get; set; }

        public IList<listadia> listadias { get; set; }
        public Principal()
        {
            InitializeComponent();
            data_list();
            var toast = DependencyService.Get<IToastService>();
            toast?.ShowToast("Seleccione un Registro para ver el detalle");

        }

        public class listadia
        {
            public string carro_id { get; set; }
            public string ruta_id { get; set; }
            public string created_at { get; set; }

            public string nombre { get; set; }

            public string registro { get; set; }
            public string id { get; set; }
        }

        public void data_list()
        {

            var fecha = DateTime.Now.ToString("yyyy-MM-dd");
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select registros.id, rutas.nombre, carros.registro from registros INNER JOIN rutas on registros.ruta_id=rutas.id INNER JOIN carros on registros.carro_id=carros.id WHERE registros.fecha='" + fecha + "';", conexion);

            var rd = cmd.ExecuteReader();

            listadias = new List<listadia>();

            while (rd.Read())
            {
                listadias.Add(new listadia
                {
                    //created_at = rd.GetDateTime("created_at").ToString(),
                    nombre = rd.GetString("nombre").ToString(),
                    registro = rd.GetString("registro").ToString(),
                    id = rd.GetInt16("id").ToString()

                }
                );
            }

            rd.Close();
            vistadia.ItemsSource = listadias;

        }

        public void vistadia_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select * from registros ;", conexion);
            var rd = cmd.ExecuteReader();
            rd.Read();
            id = rd.GetInt16("id").ToString();
            data_list();
            Navigation.PushAsync(new Registros(id));


        }
    }
}