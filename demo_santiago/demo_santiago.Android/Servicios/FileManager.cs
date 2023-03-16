// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Diagnostics;
using System.IO;
using demo_santiago;
using demo_santiago.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileManagerDroid))]
namespace demo_santiago.Droid
{
    public class FileManagerDroid : IFileManager
    {

        public bool SaveText(string filename, string text, bool overWrite = true)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filePath = Path.Combine(documentsPath, filename);
            if (!overWrite) if (System.IO.File.Exists(filePath)) return false;
            System.IO.File.WriteAllText(filePath, text);
            return true;
        }
        public string LoadText(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filePath = Path.Combine(documentsPath, filename);
            return System.IO.File.ReadAllText(filePath);
        }

        public Stream LoadFile(string filename)
        {
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            var filePath = Path.Combine(documentsPath, filename);
            try { return System.IO.File.Open(filePath, FileMode.Open, FileAccess.Read); }
            catch (Exception e)
            {
                Debug.WriteLine("FileIO Error: " + e.Message);
            }
            return null;
        }

        public bool exist(string filename, string path = "")
        {
            var filePath = "";
            if (path == "")
            {
                var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                filePath = Path.Combine(documentsPath, filename);
            }
            else
            {
                filePath = Path.Combine(path, filename);
            }
            return System.IO.File.Exists(filePath);
        }

        public bool delete(string filename, string documentsPath = "")
        {
            string filePath;
            if (documentsPath == "")
            {
                documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                filePath = Path.Combine(documentsPath, filename);
            }
            else
            {
                filePath = Path.Combine(documentsPath, filename);
            }
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }

        public string getFullPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        Stream IFileManager.LoadFile(string filename)
        {
            throw new NotImplementedException();
        }
    }
}
