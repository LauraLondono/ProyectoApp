// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public static class Styles
    {
        public static Style defultLabel;
        public static Style simpleTitle;

        public static Style smallLabel;

        public static Style labelButton;

        public static Style bgEntry;

        public static Style defaulEntry;

        public static Style simpleButton;

        public static Style settingsLabel;

        public static Style SettingsEntry;

        public static Style SettingsLabelItem;

        public static void initStyles()
        {
            defultLabel = new Style(typeof(Label))
            {
                Setters = {
                    //new Setter { Property = Label.TextColorProperty, Value = Color.White  },
                    //new Setter { Property = Label.FontSizeProperty, Value = gb.normalFontSize  },
                    //new Setter { Property = Label.FontFamilyProperty, Value = gb.regularFont  },
                    //new Setter { Property = Label.BackgroundColorProperty, Value = gb.mainColor  },
                    //new Setter { Property = Label.VerticalTextAlignmentProperty, Value = TextAlignment.Center },
                    //new Setter { Property = Label.HorizontalTextAlignmentProperty, Value = TextAlignment.Center },
                }
            };

            //defaulEntry = new Style(typeof(MyEntry))
            //{
            //    Setters = {
            //      new Setter { Property = MyEntry.PlaceholderColorProperty, Value =  gb.entryPlaceHolderColor, },
            //      new Setter { Property = MyEntry.TextColorProperty, Value =  gb.entryTextColor},
            //      new Setter { Property = MyEntry.FontSizeProperty, Value =  gb.entryFontSize},
            //      new Setter { Property = MyEntry.FontFamilyProperty, Value =  gb.regularFont },
            //      //new Setter { Property = MyEntry.BackgroundColorProperty, Value = Color.FromRgba(255,255,255,0.50), },
            //      new Setter { Property = MyEntry.HasBorderProperty, Value = false },
            //      new Setter { Property = MyEntry.HorizontalTextAlignmentProperty, Value = TextAlignment.Start },
            //  }
            //};

            App.Current.Resources = new ResourceDictionary();
            App.Current.Resources.Add(defultLabel);
            //App.Current.Resources.Add(defaulEntry);

            //redBgEntry();

            //redBgLabel();
        }

        public static void redBgEntry()
        {
            //defaulEntry.Setters.Add(new Setter { Property = MyEntry.BackgroundColorProperty, Value = Color.Red.MultiplyAlpha(0.3) });
        }
        public static void redBgLabel()
        {
            defultLabel.Setters.Add(new Setter { Property = Label.BackgroundColorProperty, Value = Color.Red.MultiplyAlpha(0.3) });
        }

    }
}