// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace demo_santiago
{
    struct FileEntry
    {
        public Stream stream;
        public string mimeType;
        public FileEntry(Stream stream, string mimeType){
            this.stream = stream;
            this.mimeType = mimeType;
        }
    }

    public struct Response
    {
        public JToken datos;
        public string mensaje;
        public string codigoRespuesta;
        public bool exitoso;
        public Response(bool success, JToken data, string message, string responseCode){
            datos = data;
            mensaje = message;
            codigoRespuesta = responseCode;
            exitoso = success;
        }

    }

    public static class APIServices
    {
        public static string BaseUrl = "";
        public static int testDelay = 0;
        enum ServerEnvironment { development, production }

        public static class MimeType
        {
            public static readonly string
                image = "image/jpeg",
                video = "video/mp4",
                audio = "audio/x-wav"
                ;
        }

        public static void initAPIServices()
        {
            switch (ServerEnvironment.development)
            {
                case ServerEnvironment.development: BaseUrl = "https://klaxen.net/klaxenasistenciaapp/api/"; break;
            }
            //Android fix 
            var h1 = createClient();
            var h2 = createClient();
        }

        public static bool checkInternetError(Exception e)
        {
            if (e.InnerException != null && e.InnerException is System.Net.WebException)
            {
                var webEx = (System.Net.WebException)e.InnerException;
                if (webEx.Status == System.Net.WebExceptionStatus.ConnectFailure)
                    return true;
            }
            return false;
        }

        public static HttpClient createClient()
        {
            HttpClient httpClient = DependencyService.Get<IHttpManager>().createClient(BaseUrl);
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Debug.WriteLine("->http client created!");
            return httpClient;
        }

        public static bool checkSuccess(JObject jsonObj)
        {
            JToken succed;
            if (jsonObj.TryGetValue("success", out succed) && succed.Value<string>().ToString() == "1")
                return true;
            return false;
        }

        /// <summary>
        /// Post multipart form with the passed data
        /// </summary>
        /// <returns>Returns JsonData, Message, ResponseCode</returns>
        /// <param name="url">URL end api</param>
        /// <param name="data">all data as an array</param>
        public static async Task<Response> postAsyncData(string url, object[,] data)
        {
            try
            {
                using (var httpClient = createClient())
                {
                    using (var formData = new MultipartFormDataContent())
                    {

                        for (int i = 0; i < data.GetLength(0); i++)
                        {
                            if (data[i, 0] == null || data[i, 1] == null || !(data[i, 1] is string) || string.IsNullOrEmpty(data[i, 1] as string)) { Debug.WriteLine("Missing data on index [" + i + "]"); continue; }
                            string fieldName = data[i, 1] as string;

                            if (data[i, 0] is string | data[i, 0] is int | data[i, 0] is double)
                            {
                                var input = new StringContent(data[i, 0].ToString());
                                formData.Add(input, fieldName);
                            }

                            if (data[i, 0] is FileEntry)
                            {
                                var fileEntry = (FileEntry)data[i, 0];

                                if (fileEntry.stream == null | string.IsNullOrEmpty(fileEntry.mimeType))
                                {
                                    Debug.WriteLine("Missing File data on index [" + i + "]");
                                    continue;
                                }

                                HttpContent fileStreamContent = new StreamContent(fileEntry.stream);
                                fileStreamContent.Headers.ContentType = new MediaTypeHeaderValue(fileEntry.mimeType);
                                formData.Add(fileStreamContent, fieldName, "session_id" + "_" + DateTime.Now.ToString("s"));

                            }
                        }

                        var response = await httpClient.PostAsync(url, formData);
                        var content = await response.Content.ReadAsStringAsync();
                        Debug.WriteLine("Raw Response: " + content);
                        var jsonObj = JsonConvert.DeserializeObject<JObject>(content);
                        bool success = false;
                        if (checkSuccess(jsonObj))
                            success = true;

                        string responseCode = "";
                        JToken responseCodeJ;
                        if (jsonObj.TryGetValue("codigoRespuesta", out responseCodeJ))
                            responseCode = responseCodeJ.Value<string>();

                        JToken messajeJ;
                        string messageString = "";
                        if (jsonObj.TryGetValue("mensaje", out messajeJ))
                            messageString = messajeJ.Value<string>();

                        JToken dataJ = null;
                        if (jsonObj.TryGetValue("datos", out dataJ))
                            ;

                        return new Response(success, dataJ, messageString, responseCode);
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("APIServices Error: " + e.Message);
                if (checkInternetError(e))
                {
                    return new Response(false, null, "There was an unexpected error connecting to the server, check your internet connection and try again.", "");
                }
            }

            return new Response(false, null, "There was an unexpected error connecting to the server", "");
        }

        public static async Task<Tuple<Session, Response>> Sincronizacion(StorageMessage objetoSincronizar)
        {
            object[,] data = {
                { JsonConvert.SerializeObject(objetoSincronizar),"capacitaciones" }
            };

            string url = "ka0-0.php";

            var result = await postAsyncData(url, data);
            try
            {
                var session = JsonConvert.DeserializeObject<Session>(result.datos.ToString());

                return new Tuple<Session, Response>(session, result);

            }
            catch (Exception e)
            {
                Debug.WriteLine("Sincronización Error: " + e.Message);
                result.mensaje = "Existe un error al conectarse al servidor.";
            }
            return new Tuple<Session, Response>(null, result);
        }

    }
}
