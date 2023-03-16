// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public class BoxViewKeyboardHeight : RBoxView
    {
        public Action<double> onKeyboardChange;

        public BoxViewKeyboardHeight()
        {
            IsVisible = false;
            BackgroundColor = Color.Transparent;
            HeightRequest = 0;
            WidthRequest = 0;
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            //Debug.WriteLine("Keyboar Box Prop " + propertyName);
        }
        protected override void OnSizeAllocated(double width, double height)
        {
            //Debug.WriteLine("OnSizeAllocated changed: " + width + ", " + height);
            //if (onKeyboardChange != null) onKeyboardChange(height);
            base.OnSizeAllocated(width, height);
        }

    }
}
