using MySqlConnector;
using Reportalo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Reportalo.Views.Principal;

namespace Reportalo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Multas : ContentPage
    {
        public IList<listamulta> listamultas { get; set; }
        public Multas()
        {
            InitializeComponent();
            data_list();
            var toast = DependencyService.Get<IToastService>();
            toast?.ShowToast("Seleccione un Multa para ver el detalle");
        }
        public class listamulta
        {
            public string created_at { get; set; }
            public string nombre { get; set; }
            public string registro { get; set; }
            
        }
        private void data_list()
        {
            
            
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select multas.id, rutas.nombre, carros.registro from multas INNER JOIN rutas on multas.ruta_id=rutas.id INNER JOIN carros on multas.carro_id=carros.id ;", conexion);

            var rd = cmd.ExecuteReader();

            listamultas = new List<listamulta>();

            while (rd.Read())
            {
                listamultas.Add(new listamulta
                {
                    //created_at = rd.GetString("created_at").ToString(),
                    nombre = rd.GetString("nombre").ToString(),
                    registro = rd.GetString("registro").ToString(),                   

                }
                );
            }
            rd.Close();
            vistamulta.ItemsSource = listamultas;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new IngresarMulta());
        }
    }
}