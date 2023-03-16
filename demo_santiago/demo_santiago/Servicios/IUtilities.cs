// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;

namespace demo_santiago
{
    public interface IUtilities
    {
        void WriteLine(string str);
        string bundleVersion();
        double screenWidth();
        double screenHeight();
        void hideStatusBar(bool hide);
        double getStatusBarHeight();
        double nonStaticConvertDpToPixels(double dp);
        double nonStaticConvertPixelsToDp(double pixels);
        string getHardwareVersion();
        string baseAppUrl();
        void CopyToClipboard(string text);
        string DirectoryBD { get; }
    }
}
