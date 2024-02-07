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
        public Registros(String id)
        {
            InitializeComponent();
            txtidregistro.Text = id;
            ident = "22";
            data_list();
        }

        public class listaregistro
        {
            public string id { get; set; }
            public string hora { get; set; }
            public string serie35 { get; set; }
            public string serie17 { get; set; }
            public string serie10 { get; set; }

        }
        private void data_list()
        {
            var conexion = new MySqlConnection(Properties.Resources.Conexion);
            conexion.Open();
            var cmd = new MySqlCommand("select * from dias where registro_id= '" + ident+ "'", conexion);


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
                    serie10 = rd.GetInt64("serie10").ToString()
                }
                );
            }
            rd.Close();
            vistaregistro.ItemsSource = listaregistros;
        }
    }
}