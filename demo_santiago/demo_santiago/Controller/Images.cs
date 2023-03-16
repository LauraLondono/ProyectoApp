// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace demo_santiago
{
    public static class Images
    {
        public static ImageSource messages_icon { get; } = ImageSource.FromFile("messages.png");
        public static ImageSource report_icon { get; } = ImageSource.FromFile("report.png");
        public static ImageSource about_icon { get; } = ImageSource.FromFile("about.png");
        public static ImageSource propuse_icon { get; } = ImageSource.FromFile("propuse.png");
        public static ImageSource whapp_icon { get; } = ImageSource.FromFile("whapp.png");
        public static ImageSource email_icon { get; } = ImageSource.FromFile("email.png");
        public static ImageSource medal_gold_icon { get; } = ImageSource.FromFile("medal_gold_icon.png");
        public static ImageSource medal_gray_icon { get; } = ImageSource.FromFile("medal_gray_icon.png");
        public static ImageSource medal_orange_icon { get; } = ImageSource.FromFile("medal_orange_icon.png");
    }
}
