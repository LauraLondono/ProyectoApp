// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Android.Content;
using Android.OS;
using Android.Views;
using demo_santiago.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Page), typeof(PageRenderers))]
namespace demo_santiago.Droid.Renderers
{
    public class PageRenderers : PageRenderer
    {
        AndroidBug5497WorkaroundForXamarinAndroid fix;
        public PageRenderers(Context context) : base(context)
        {
            //Forms.Context as Activity;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Page> args)
        {
            base.OnElementChanged(args);

                if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr2)
                {
                    // Bug in Android 5+, this is an adequate workaround
#pragma warning disable CS0618 // Type or member is obsolete
                    fix = AndroidBug5497WorkaroundForXamarinAndroid.assistActivity((Forms.Context as Android.App.Activity), ((Android.App.Activity)Forms.Context).WindowManager);
#pragma warning restore CS0618 // Type or member is obsolete
                }

        }

    }
}

