// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Net.Http;

namespace demo_santiago
{
    public interface IHttpManager
    {
        HttpClient createClient(string BaseUrl);
    }
}
