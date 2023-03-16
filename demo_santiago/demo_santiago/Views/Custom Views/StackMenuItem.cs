// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static demo_santiago.LayoutTools;

using Xamarin.Forms;

namespace demo_santiago
{
   public class ViewMenuItem : RelativeLayout
    {
        Image imagenItem;
        Label textoItem;
        RBoxView boxViewFondo;
        bool click = false;
        public ViewMenuItem(ImageSource rutaImagen, string textoMenu,Color colorFondo)
        {
            imagenItem = new Image
            {
                WidthRequest = _w(70),
                HeightRequest = _w(70),
                BackgroundColor = colorFondo,
                Source = (rutaImagen == null) ? "" : rutaImagen,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            textoItem = new Label
            {
                Text = textoMenu,
                WidthRequest = _w(110),
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
                FontSize = _h(14)
            };

            boxViewFondo = new RBoxView
            {
                BackgroundColor = Color.White,
                BorderRadius = _w(8),
                HasShadow = false,
                StrokeColor = gb.navBarColor,
                StrokeThickness = (Device.RuntimePlatform == Device.iOS) ? 1.3 : .2
            };

            Children.Add(boxViewFondo,
                               Constraint.RelativeToParent((p) => { return 0; }),
                               Constraint.RelativeToParent((p) => { return 0; }),
                               Constraint.RelativeToParent((p) => { return p.Width; }),
                               Constraint.RelativeToParent((p) => { return p.Height; }));


            Children.Add(imagenItem,
                               Constraint.RelativeToParent((p) => { return p.Width * 0.25; }),
                               Constraint.RelativeToParent((p) => { return p.Height * 0.2; }),
                               Constraint.RelativeToParent((p) => { return p.Width * 0.5; }),
                               Constraint.RelativeToParent((p) => { return p.Height * 0.5; }));

            Children.Add(textoItem,
                               Constraint.RelativeToParent((p) => { return 0; }),
                               Constraint.RelativeToParent((p) => { return p.Height * 0.7; }),
                               Constraint.RelativeToParent((p) => { return p.Width; }),
                               Constraint.RelativeToParent((p) => { return p.Height; }));

            Core.addClick(this, GestoTap_Tapped);

        }

        private async void GestoTap_Tapped(object sender, EventArgs e)
        {
            Label labelClick = ((Label)((RelativeLayout)sender).Children[2]);

            Core.scaleOnTap(this);

            if (click) return;

            click = true;

            switch (labelClick.Text)
            {
                default:
                    break;
            }
            Console.WriteLine(labelClick.Text);

            click = false;

        }
    }
}