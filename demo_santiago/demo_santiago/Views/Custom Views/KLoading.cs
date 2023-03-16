// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static demo_santiago.LayoutTools;

using Xamarin.Forms;

namespace demo_santiago
{
    public class BLoading : RelativeLayout
    {
        ActivityIndicator kLoading;
        public BLoading()
        {
            CrearVistas();
            AgregarVistas();
        }

        void CrearVistas()
        {
            kLoading = new ActivityIndicator
            {
                IsRunning = true,
                IsVisible = true,
                Color = gb.titleColor
            };

            IsVisible = false;
            BackgroundColor = gb.mainColor.MultiplyAlpha(0.4);
        }

        void AgregarVistas()
        {
            addChild(this, kLoading, _w(162.5), _h(307.5), _w(50), _w(50));
        }
    }
}