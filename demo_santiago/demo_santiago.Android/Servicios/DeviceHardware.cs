// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Android.OS;

namespace demo_santiago.Droid
{
    public class DeviceHardware
    {
        public static string Version
        {
            get
            {
                try
                {
                    string manufacturer = Build.Manufacturer;
                    string model = Build.Model;
                    if (model.StartsWith(manufacturer))
                    { return model; }
                    { return manufacturer + " " + model; }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("DeviceHardware.Version Ex: " + ex.Message);
                }

                return "Unknown";
            }
        }
    }
}
