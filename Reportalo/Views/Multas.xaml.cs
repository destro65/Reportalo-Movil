using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace Reportalo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Multas : ContentPage
    {
        public IList<listamulta> listamultas { get; set; }
        private Timer timer;
        public Multas()
        {
            InitializeComponent();
            data_list();
            InitializeTimer();
        }
        public class listamulta
        {
            public string created_at { get; set; }
            public string nombre { get; set; }
            public string registro { get; set; }
            
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