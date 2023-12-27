using Reportalo.Droid.Servicios;
using Reportalo.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastService))]
namespace Reportalo.Droid.Servicios
{
    public class ToastService : IToastService
    {
        public void ShowToast(string Text)
        {
            Android.Widget.Toast.MakeText(Android.App.Application.Context, Text, Android.Widget.ToastLength.Short).Show();
        }
    }
}