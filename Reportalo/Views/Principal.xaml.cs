using MySqlConnector;
using Reportalo.Services;
using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reportalo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Principal : ContentPage
    {
        public object id;

        public IList<listadia> listadias { get; set; }
        private Timer timer;
        public Principal()
        {
            InitializeComponent();
            InitializeTimer();
            data_list();
            var toast = DependencyService.Get<IToastService>();
            toast?.ShowToast("Seleccione un Registro para ver el detalle");

        }
        private void InitializeTimer()
        {
            // Configura un temporizador que llamará a la función UpdateDataList cada 5 segundos
            timer = new Timer(UpdateDataList, null, 0, 5000);
        }

        public void UpdateDataList(object state)
        {
            // Esta función se ejecutará cada vez que el temporizador alcance su intervalo.
            Device.BeginInvokeOnMainThread(() =>
            {
                data_list();
            });
        }

        public class listadia
        {
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
                    id = rd.GetInt32("id").ToString(),
                }
                );
            }
            vistadia.ItemsSource = listadias;
            conexion.Close();

        }

        public void vistadia_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            if (e.Item is listadia selectedDia)
            {
                string selectedId = selectedDia.id;
                Navigation.PushAsync(new Registros(selectedId));
            }

        }
    }
}