using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static Reportalo.Views.Registros;

namespace Reportalo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Registros : ContentPage
    {

        public IList<listaregistro> listaregistros { get; set; }
        public Registros(String id)
        {

            InitializeComponent();
            txtidregistro.Text = id;
            data_list();


        }

        public class listaregistro
        {
            public string hora { get; set; }
            public string serie35 { get; set; }
            public string serie17 { get; set; }
            public string serie10 { get; set; }
            public string vendidos { get; set; }
            public string estimado { get; set; }
            public string registro_id { get; set; }
            public Color Color { get; internal set; }
        }
        
        public void data_list()
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select * from vista_registro WHERE registro_id='" + txtidregistro.Text + "'", conexion);
            var rd = cmd.ExecuteReader();

            listaregistros = new List<listaregistro>();
            List<int> datosEstimados = new List<int> { 10, 20, 30, 40, 50 }; // Ejemplo de lista de datos estimados

            int estimadoIndex = 0; // Índice para recorrer la lista de datos estimados

            while (rd.Read())
            {
                var registro = new listaregistro
                {
                    hora = rd["hora"] is DBNull ? string.Empty : rd.GetTimeSpan("hora").ToString(),
                    serie35 = rd["serie35"] is DBNull ? string.Empty : rd.GetInt64("serie35").ToString(),
                    serie17 = rd["serie17"] is DBNull ? string.Empty : rd.GetInt64("serie17").ToString(),
                    serie10 = rd["serie10"] is DBNull ? string.Empty : rd.GetInt64("serie10").ToString(),
                    vendidos = rd["vendidos"] is DBNull ? string.Empty : rd.GetInt64("vendidos").ToString(),

                    // Asignar datos estimados desde la lista
                    estimado = estimadoIndex < datosEstimados.Count ? datosEstimados[estimadoIndex].ToString() : string.Empty,

                    registro_id = rd["registro_id"] is DBNull ? string.Empty : rd.GetInt16("registro_id").ToString(),
                };

                // Realizar la comparación entre vendidos y estimado
                if (!string.IsNullOrEmpty(registro.vendidos) && !string.IsNullOrEmpty(registro.estimado))
                {
                    int vendidos = Convert.ToInt32(registro.vendidos);
                    int estimado = Convert.ToInt32(registro.estimado);

                    if (vendidos < estimado)
                    {
                        registro.Color = Color.Red; // Asignar color rojo
                    }
                    else
                    {
                        registro.Color = Color.Green; // Asignar color verde
                    }
                }

                listaregistros.Add(registro);

                estimadoIndex++; // Mover al siguiente dato estimado en la lista
            }



            rd.Close();
            vistaregistro.ItemsSource = listaregistros;


        }
        public class ComparisonToColorConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                if (value is Color color)
                {
                    return color;
                }

                return Color.Default;
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

    }
}
