// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace demo_santiago
{
    public static class Storage
    {
        public static Session demoSession = new Session { Id = "123456", Correo = "user@mail.com", Usuario = "UserName" };

        public static Session getSession()
        {
            bool exist = DependencyService.Get<IFileManager>().exist(gb.sessionFileName);
            if (exist)
            {
                var sessionJson = DependencyService.Get<IFileManager>().LoadText(gb.sessionFileName);
                //Debug.WriteLine("Session Read from disk");
                try
                {
                    return JsonConvert.DeserializeObject<Session>(sessionJson);
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Error parsing Session from disk: " + e.Message);
                    deleteSession();
                    return null;
                };
            }
            else
            {
                Debug.WriteLine("No Sesion Stored");
                return null;
            }
        }
        public static void saveSession(Session session)
        {
            var sessionJson = JsonConvert.SerializeObject(session);
            DependencyService.Get<IFileManager>().SaveText(gb.sessionFileName, sessionJson);
            Debug.WriteLine("Session Saved: id " + session.Id);
        }

        public static void deleteSession()
        {
            var deleted = DependencyService.Get<IFileManager>().delete(gb.sessionFileName);
            var deletedPlayerid = DependencyService.Get<IFileManager>().delete(gb.storedPlayeridFileName);
            var deleteSocialAccountData = DependencyService.Get<IFileManager>().delete(gb.accountsDataFileName);
            var deleteReceiptData = DependencyService.Get<IFileManager>().delete(gb.storedReceiptsFileName);
            Debug.WriteLine("Session, stored Deleted: " + deleted);
        }


    }

    public interface IFileManager
    {
        bool SaveText(string filename, string text, bool overWrite = true);
        string LoadText(string filename);
        Stream LoadFile(string filename);
        bool exist(string filename, string path = "");
        bool delete(string filename, string documentsPath = "");
        string getFullPath();
    }
}
