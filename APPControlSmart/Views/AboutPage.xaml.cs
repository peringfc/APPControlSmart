using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using APPControlSmart.Services;
using System.Threading.Tasks;
using Xamarin.Essentials;
using PermissionStatus = Plugin.Permissions.Abstractions.PermissionStatus;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using APPControlSmart.Models;

namespace APPControlSmart.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            CarregarCelularAsync();
        }

        protected async Task CarregarCelularAsync()
        {
            //Verify Permission
            var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Phone);
            if (status != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(Permission.Phone);
                //Best practice to always check that the key exists
                if (results.ContainsKey(Permission.Phone))
                    status = results[Permission.Phone];
            }

            //Get Imei
            LblImei.Text = "IMEI = " + DependencyService.Get<IServiceImei>().GetImei();
            lbl_Imei.Text = DependencyService.Get<IServiceImei>().GetImei();

            lbl_device.Text = DeviceInfo.Name.ToString() + " - " + DeviceInfo.Model.ToString();
            lbl_manufacturer.Text = DeviceInfo.Manufacturer;
            lbl_deviceName.Text = DeviceInfo.Name;
            lbl_Version.Text = DeviceInfo.Version.ToString();
            lbl_platform.Text = DeviceInfo.Platform.ToString();
            lbl_idiom.Text = DeviceInfo.Idiom.ToString();
            lbl_deviceType.Text = DeviceInfo.DeviceType.ToString();


        }

        private void GravaDados(object sender, EventArgs e)
        {
            /// Chamada de API 
            /// 

            DadosCelular oDadosCelular = new DadosCelular();

            string url = "URL da API";

            oDadosCelular.IMEI = DependencyService.Get<IServiceImei>().GetImei();

            oDadosCelular.device = DeviceInfo.Name.ToString() +" - "+ DeviceInfo.Model.ToString();
            oDadosCelular.manufacturer = DeviceInfo.Manufacturer;
            oDadosCelular.deviceName = DeviceInfo.Name;
            oDadosCelular.Version = DeviceInfo.Version.ToString();
            oDadosCelular.platform = DeviceInfo.Platform.ToString();
            oDadosCelular.idiom = DeviceInfo.Idiom.ToString();
            oDadosCelular.deviceType = DeviceInfo.DeviceType.ToString();



            string JsonSearch = ChamadaAPI(url, oDadosCelular);




        }


        public string ChamadaAPI(string url, object p_Object)
        {
            string responseBody = "";
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var json = Newtonsoft.Json.JsonConvert.SerializeObject(p_Object);
                var data = new System.Net.Http.StringContent(json, Encoding.UTF8, "application/json");

                var result = client.PostAsync(url, data);
                responseBody = result.Result.Content.ReadAsStringAsync().Result;
            }

            return responseBody;
        }

    }
}