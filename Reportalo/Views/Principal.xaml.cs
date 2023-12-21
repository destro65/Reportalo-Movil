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
	public partial class Principal : ContentPage
	{
		public IList<listadia> listadias { get; set; }
		public Principal ()
		{
			InitializeComponent ();
			data_list();
		}

		public class listadia
		{
			public string carro_id { get; set; }
			public string ruta_id { get; set; }
			public string created_at { get; set; }
		}

		private void data_list()
		{
			var conexion = new MySqlConnection(Properties.Resources.Conexion);
			conexion.Open ();
			var cmd = new MySqlCommand("select * from registros", conexion);
			var rd = cmd.ExecuteReader ();

			listadias = new List<listadia> ();

			while (rd.Read ())
			{
				listadias.Add( new listadia
				{
                    //created_at = rd.GetDateTime("created_at").ToString(),
					carro_id = rd.GetInt16("carro_id").ToString(),
					ruta_id = rd.GetInt16("ruta_id").ToString()

				}
				);
			}
			rd.Close ();
            vistadia.ItemsSource = listadias;
		}

        private void vistadia_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Navigation.PushAsync(new Registros());
        }
    }
}