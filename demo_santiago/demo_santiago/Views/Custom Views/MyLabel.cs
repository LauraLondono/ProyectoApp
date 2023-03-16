// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public class MyLabel : Label
    {
        public static readonly BindableProperty LineHeightProperty =
            BindableProperty.Create("LineHeight", typeof(double), typeof(MyLabel), 1.0, BindingMode.Default,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((MyLabel)bindable).LineHeight = (double)newValue; }
                                   );
        public double LineHeight
        {
            get { return (double)this.GetValue(LineHeightProperty); }
            set { SetValue(LineHeightProperty, value); }
        }
    }
}
