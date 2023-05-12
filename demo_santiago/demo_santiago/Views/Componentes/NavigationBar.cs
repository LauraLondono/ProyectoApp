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
        Image PrincipalLogo;
        bool click = false;
        public Button buttonBluetooth;
        ImageSource path_principal_logo;
        public NavigationBar(ImageSource path_logo = null, string titulo = "", ImageSource rutaSegundoIcono = null)
        {
            path_principal_logo = path_logo;
            CrearVistas();
            AgregarVistas();
            AgregarEventosControles();
        }

        void CrearVistas()
        {
            buttonBluetooth = new Button
            {
                CornerRadius = (int) (gb.screenHeight * 0.5),
                BackgroundColor = Color.Red
            };

            PrincipalLogo = new Image
            {
                Source = (path_principal_logo == null) ? "" : path_principal_logo,
                Aspect = Aspect.AspectFit
            };

            BackgroundColor = gb.mainColor;
        }

        void AgregarEventosControles()
        {
        }

        void AgregarVistas()
        {
            double spacing = 0;
            if (Device.RuntimePlatform == Device.iOS)
                spacing = gb.deviceCarrierSpacing;

            Children.Add(PrincipalLogo,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.074; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.144; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.293; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.650; })
            );

            Children.Add(buttonBluetooth,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.816; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.265; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.101; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.101; })
            );
        }
    }
}