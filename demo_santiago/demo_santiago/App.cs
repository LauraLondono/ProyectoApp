// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Plugin.BluetoothClassic.Abstractions;
using Xamarin.Forms;

namespace demo_santiago
{
    public class App : Application
    {
        public static StorageMessage ObjectMessage;
        public static IBluetoothManagedConnection CurrentBluetoothConnection { get; internal set; }
        public App()
        {
            Core.init();
            StorageInit();
            MainPage = new NavigationPage(new HomePage());
        }

        void StorageInit()
        {

            bool existeArchivo = gb.LocalStorage.exist(gb.JsonFileName);
            if (existeArchivo)
            {
                //gb.LocalStorage.delete(gb.nombreArchivoJSONLocal);
                string fileLocalString = gb.LocalStorage.LoadText(gb.JsonFileName);
                ObjectMessage = JsonConvert.DeserializeObject<StorageMessage>(fileLocalString);
            }
            else
            {
                ObjectMessage = new StorageMessage();
            }
        }
    }
}