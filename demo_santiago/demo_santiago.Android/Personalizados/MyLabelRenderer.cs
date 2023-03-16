// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Android.Content;
using demo_santiago;
using demo_santiago.Droid.Personalizados;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyLabel), typeof(MyLabelRenderer))]
namespace demo_santiago.Droid.Personalizados
{
    public class MyLabelRenderer : LabelRenderer
    {
        public MyLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
            }
            if (e.NewElement != null)
            {
                var myLabel = Element as MyLabel;
                Control.SetLineSpacing(0, (float)myLabel.LineHeight);
            }
        }

    }
}
