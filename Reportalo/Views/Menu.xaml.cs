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
    public partial class Menu : TabbedPage
    {
        private object usuario;

        public Menu(string id)
        {

            InitializeComponent();
            txtid.Text = id;
            usuario = id;

            var con = new MySqlConnection(Properties.Resources.Conexion);
            con.Open();
            var cmd = new MySqlCommand("select * from users where id = '" + txtid.Text + "'", con);
            var rd = cmd.ExecuteReader();
            rd.Read();
            txtid.Text = rd.GetString("name").ToString();

        }

        public Menu()
        {
        }

        private void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginPage());
        }


        public void ToolbarItem_Clicked_1(object sender, EventArgs e)
        {

            Navigation.PushAsync(new Perfil((string)usuario));
        }
    }
}