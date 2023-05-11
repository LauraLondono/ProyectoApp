// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static demo_santiago.LayoutTools;
using Xamarin.Forms;

namespace demo_santiago
{
    public class NavigationBar : RelativeLayout
    {
        //Label textoTitulo;
        Image imagenInicial, imagenFinal, imagenLogo;
        bool click = false;
        public Label textoTitulo;
        public Button buttonBluetooth;
        ImageSource RutaPrimeraIcono, RutaSegundoIcono;
        public NavigationBar(ImageSource rutaPrimerIcono = null, string titulo = "", ImageSource rutaSegundoIcono = null)
        {
            RutaPrimeraIcono = rutaPrimerIcono;
            //textoTitulo = titulo;
            RutaSegundoIcono = rutaSegundoIcono;
            CrearVistas();
            AgregarVistas();
            AgregarEventosControles();
        }

        void CrearVistas()
        {
            textoTitulo = new Label();
            buttonBluetooth = new Button();
            //textoTitulo = new Label()
            //{
            //    Text = Titulo,
            //    TextColor = colorTextoTitulo,
            //    FontSize = _h(24),
            //    VerticalTextAlignment = TextAlignment.Center,
            //    HorizontalTextAlignment = TextAlignment.Center
            //};

            //imagenLogo = new Image
            //{
            //    Source = Images.Logo_Klaxen
            //};

            imagenInicial = new Image
            {
                Source = (RutaPrimeraIcono == null) ? "" : RutaPrimeraIcono,
            };

            imagenFinal = new Image
            {
                Source = (RutaSegundoIcono == null) ? "" : RutaSegundoIcono,
            };

            BackgroundColor = gb.mainColor;
        }

        void AgregarEventosControles()
        {
            Core.addClick(imagenInicial, ClickAtras_Tapped);
        }

        private async void ClickAtras_Tapped(object sender, EventArgs e)
        {
            Core.scaleOnTap(imagenInicial);
            if (click) return;
            click = true;

            if (RutaPrimeraIcono == null)
                return;

            Page paginaActual = ((NavigationPage)Application.Current.MainPage).Navigation.NavigationStack[((NavigationPage)Application.Current.MainPage).Navigation.NavigationStack.Count - 1];

            await paginaActual.Navigation.PopAsync();

            click = false;
        }

        void AgregarVistas()
        {
            double spacing = 0;
            if (Device.RuntimePlatform == Device.iOS)
                spacing = gb.deviceCarrierSpacing;

            //addChild(this, textoTitulo, _w(56), _h(spacing), _w(267), _h(56));
            //addChild(this, imagenLogo, _w(234), _h(spacing), _w(124), _h(44));
            addChild(this, imagenInicial, _w(0), _h(spacing), _w(56), _h(56));
            addChild(this, imagenFinal, _w(321), _h(spacing), _w(56), _h(56));
        }
    }
}