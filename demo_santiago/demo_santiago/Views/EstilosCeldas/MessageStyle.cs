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
                Padding = new Thickness(10, 0, 0, 0),
                Spacing = 2
            };

            viewLabel = new StackLayout
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.StartAndExpand
            };

            message = new Label
            {
                TextColor = gb.textsColor,
                FontSize = _h(14),
                VerticalTextAlignment = TextAlignment.Center
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

            View = viewPrincipal;
        }
    }
}