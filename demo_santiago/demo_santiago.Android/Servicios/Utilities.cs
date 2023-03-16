// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Android.Content;
using Android.Content.Res;
using Android.Views;
using demo_santiago;
using demo_santiago.Droid;
using SQLite.Net.Interop;
using Xamarin.Forms;

[assembly: Dependency(typeof(Utilities))]
namespace demo_santiago.Droid
{
    public class Utilities : IUtilities
    {
        string directoryBD;
        ISQLitePlatform platform;

        public string DirectoryBD
        {
            get
            {
                if (string.IsNullOrEmpty(directoryBD))
                {
                    directoryBD = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                }

                return directoryBD;
            }
        }

        public ISQLitePlatform Platfom
        {
            get
            {
                platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid();
                return platform;
            }
        }

        public void WriteLine(string str)
        {
            System.Console.WriteLine(str);
        }

        public string bundleVersion()
        {
            var context = Android.App.Application.Context;
            var version = context.PackageManager.GetPackageInfo(context.PackageName, 0).VersionName;
            return version;
        }

        public double screenWidth()
        {
            var metrics = Android.App.Application.Context.Resources.DisplayMetrics;
            var widthInDp = ConvertPixelsToDp(metrics.WidthPixels);
            return widthInDp;
        }

        public static double ConvertPixelsToDp(float pixelValue)
        {
            var v = ((pixelValue) / Android.App.Application.Context.Resources.DisplayMetrics.Density);
            return v;
        }

        public static float ConvertDpToPixels(float dpValue)
        {
            return (Android.App.Application.Context.Resources.DisplayMetrics.Density * dpValue);
        }

        public double screenHeight()
        {
            var metrics = Android.App.Application.Context.Resources.DisplayMetrics;
            var heightInDp = ConvertPixelsToDp(metrics.HeightPixels);
            return heightInDp;
        }

        public void hideStatusBar(bool hide)
        {
            var mainActivity = Android.App.Application.Context as MainActivity;
            if (hide)
            {
                mainActivity.Window.AddFlags(WindowManagerFlags.Fullscreen);
            }
            else
            {
                mainActivity.Window.ClearFlags(WindowManagerFlags.Fullscreen);
            }
        }

        public double getStatusBarHeight()
        {
            var mainActivity = Xamarin.Forms.Forms.Context as MainActivity;
            return ConvertPixelsToDp(mainActivity.getStatusBarHeight());
        }

        public string getHardwareVersion()
        {
            return DeviceHardware.Version;
        }

        public void CopyToClipboard(string text)
        {
            // Get the Clipboard Manager
            var clipboardManager = (ClipboardManager)Forms.Context.GetSystemService(Context.ClipboardService);

            // Create a new Clip
            ClipData clip = ClipData.NewPlainText("footprint_cliptext", text);

            // Copy the text
            clipboardManager.PrimaryClip = clip;
        }

        public double nonStaticConvertDpToPixels(double dp)
        {
            return ConvertDpToPixels((float)dp);
        }

        public double nonStaticConvertPixelsToDp(double pixels)
        {
            return ConvertPixelsToDp((float)pixels);
        }

        public string baseAppUrl()
        {
            return "file:///android_asset/";
        }

       

    }
}
