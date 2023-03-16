// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using demo_santiago;
using demo_santiago.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(HttpManager))]
namespace demo_santiago.Droid
{
    public class HttpManager : IHttpManager
    {
        public HttpClient createClient(string BaseUrl)
        {
            HttpClient httpClient = new HttpClient(new Xamarin.Android.Net.AndroidClientHandler())
            {
                BaseAddress = new Uri(BaseUrl),
                Timeout = new TimeSpan(0, 0, 0, gb.timeOutSec),
            };
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Debug.WriteLine("->Android httpClient");
            return httpClient;
        }
    }
}
