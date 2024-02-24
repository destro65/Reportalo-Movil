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

        }
        public void data_list()
        {

            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select * from vista_registro WHERE registro_id='" + txtidregistro.Text + "'", conexion);
            //select dias.registro_id, dias.hora, dias.serie35,dias.serie17,dias.serie10,dias.vendidos, estimados.estimado from dias INNER JOIN estimados on estimados.id = estimados.id;
            //select dias.id, dias.hora, dias.serie35,dias.serie17,dias.serie10,dias.vendidos, estimados.estimado,dias.registro_id from dias LEFT JOIN estimados on dias.id = estimados.id;
            var rd = cmd.ExecuteReader();

            listaregistros = new List<listaregistro>();
            while (rd.Read())
            {
                listaregistros.Add(new listaregistro
                {
                    //created_at = rd.GetDateTime("created_at").ToString(),
                    hora = rd["hora"] is DBNull ? string.Empty : rd.GetTimeSpan("hora").ToString(),
                    serie35 = rd["serie35"] is DBNull ? string.Empty : rd.GetInt64("serie35").ToString(),
                    serie17 = rd["serie17"] is DBNull ? string.Empty : rd.GetInt64("serie17").ToString(),
                    serie10 = rd["serie10"] is DBNull ? string.Empty : rd.GetInt64("serie10").ToString(),
                    vendidos = rd["vendidos"] is DBNull ? string.Empty : rd.GetInt64("vendidos").ToString(),
                    estimado = rd["estimado"] is DBNull ? string.Empty : rd.GetInt16("estimado").ToString(),
                    registro_id = rd["registro_id"] is DBNull ? string.Empty : rd.GetInt16("registro_id").ToString(),
                }
                );
            }
            rd.Close();
            vistaregistro.ItemsSource = listaregistros;
        }
    }
}