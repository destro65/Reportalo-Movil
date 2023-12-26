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
    public partial class Perfil : ContentPage
    {
        private object usuario;

        public Perfil(string usuario)
        {
            
            InitializeComponent();
            lbluser.Text = usuario;
            
        }

        
    }
}