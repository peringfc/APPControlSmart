using Android.Telephony;
using APPControlSmart.Services;
using DemoImei.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(ServiceImei))]
namespace DemoImei.Droid
{
    public class ServiceImei : IServiceImei
    {
        public string GetImei()
        {
            try
            {
                TelephonyManager manager = (TelephonyManager)Forms.Context.GetSystemService(Android.Content.Context.TelephonyService);

                return manager.Imei;
            }
            catch (System.Exception ex)
            {
                string message = ex.Message.ToString();
                return null;
            }


        }
    }
}