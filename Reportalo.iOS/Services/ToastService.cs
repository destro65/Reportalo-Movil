using Foundation;
using Reportalo.iOS.Services;
using Reportalo.Services;
using System.Security.Cryptography.X509Certificates;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ToastService))]

namespace Reportalo.iOS.Services
{
    public class ToastService : IToastService
    {
        public UIAlertController toast {  get; set; }
        public NSTimer delay { get; set; }
        public void ShowToast(string Text)
        {
            delay = NSTimer.CreateRepeatingScheduledTimer(2, (e) =>
            {
                toast?.DismissViewController(true, null);
                delay?.Dispose();
            });
            toast=UIAlertController.Create(null,Text,UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(toast,true,null);
        }
    }
}