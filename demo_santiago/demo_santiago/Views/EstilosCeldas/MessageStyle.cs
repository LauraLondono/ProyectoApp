// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static demo_santiago.LayoutTools;

using Xamarin.Forms;

namespace demo_santiago
{
    public class MessageStyle : ViewCell
    {
        StackLayout viewPrincipal,viewLabel;
        Label message,dateRow;
        public Image icon_trash;

        public MessageStyle()
        {
            CrearVistas();
            AgregarVistas();
        }

        void CrearVistas()
        {
            viewPrincipal = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(_w(32), 0, _w(32), 0),
                Spacing = 2,
                HorizontalOptions = LayoutOptions.FillAndExpand,
            };

            icon_trash = new Image
            {
                Aspect = Aspect.AspectFit,
                Source = Images.logo_icon_trash,
                HorizontalOptions = LayoutOptions.EndAndExpand
            };

            viewLabel = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            message = new Label
            {
                TextColor = gb.mainColorBlack,
                FontSize = _h(14),
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };

            dateRow = new Label
            {
                TextColor = gb.textsColor,
                FontSize = _h(12),
                VerticalTextAlignment = TextAlignment.Center
            };

            //iconoSiguiente = new Image
            //{
            //    Source = Imagenes.Icono_Soguiente_Naranja
            //};

            message.SetBinding(Label.TextProperty, "Message");
            dateRow.SetBinding(Label.TextProperty, "Date_Created");
        }

        void AgregarVistas()
        {
            viewLabel.Children.Add(message);
            viewLabel.Children.Add(dateRow);

            viewPrincipal.Children.Add(viewLabel);
            viewPrincipal.Children.Add(icon_trash);

            View = viewPrincipal;
        }
    }
}