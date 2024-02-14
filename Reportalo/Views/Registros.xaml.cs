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
    public partial class Registros : ContentPage
    {
        private object ident;
        public IList<listaregistro> listaregistros { get; set; }
        public Registros()
        {
            InitializeComponent();
            //txtidregistro.Text = id;            
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

        }
        private void data_list()
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select dias.id, dias.hora, dias.serie35,dias.serie17,dias.serie10,dias.vendidos, estimados.estimado,dias.registro_id \r\nfrom dias \r\nINNER JOIN estimados on dias.id = estimados.id", conexion);
            //select dias.registro_id, dias.hora, dias.serie35,dias.serie17,dias.serie10,dias.vendidos, estimados.estimado from dias INNER JOIN estimados on estimados.id = estimados.id;
            var rd = cmd.ExecuteReader();

            listaregistros = new List<listaregistro>();
            while (rd.Read())
            {
                listaregistros.Add(new listaregistro
                {
                    //created_at = rd.GetDateTime("created_at").ToString(),
                    hora = rd.GetTimeSpan("hora").ToString(),
                    serie35 = rd.GetInt64("serie35").ToString(),
                    serie17 = rd.GetInt64("serie17").ToString(),
                    serie10 = rd.GetInt64("serie10").ToString(),
                    vendidos = rd.GetInt64("vendidos").ToString(),
                    estimado = rd.GetInt16("estimado").ToString(),
                    registro_id = rd.GetInt16("registro_id").ToString(),
                }
                );
            }
            rd.Close();
            vistaregistro.ItemsSource = listaregistros;
        }
    }
}