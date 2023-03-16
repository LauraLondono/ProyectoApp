// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public class RBoxView : BoxView
    {
        public static readonly BindableProperty BorderRadiusProperty =
             BindableProperty.Create("BorderRadius", typeof(double), typeof(RBoxView), 0.0, BindingMode.Default,
                                     propertyChanged: (bindable, oldValue, newValue) => { ((RBoxView)bindable).BorderRadius = (double)newValue; }
                                    );

        public double BorderRadius
        {
            get { return (double)this.GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static readonly BindableProperty BotBorderRadiusProperty =
            BindableProperty.Create("BotBorderRadius", typeof(double), typeof(RBoxView), -1.0, BindingMode.Default,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((RBoxView)bindable).BotBorderRadius = (double)newValue; }
                           );

        public double BotBorderRadius
        {
            get { return (double)this.GetValue(BotBorderRadiusProperty); }
            set { SetValue(BotBorderRadiusProperty, value); }
        }

        public static readonly BindableProperty StrokeColorProperty =
            BindableProperty.Create("StrokeColor", typeof(Color), typeof(RBoxView), Color.Black, BindingMode.Default,
                                     propertyChanged: (bindable, oldValue, newValue) => {
                                         ((RBoxView)bindable).StrokeColor = (Color)newValue;
                                     }
                                   );
        public Color StrokeColor
        {
            get { return (Color)this.GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create("StrokeThickness", typeof(double), typeof(RBoxView), 0.0, BindingMode.Default,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((RBoxView)bindable).StrokeThickness = (double)newValue; }
                                   );
        public double StrokeThickness
        {
            get { return (double)this.GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        public static readonly BindableProperty DashedProperty =
            BindableProperty.Create("Dashed", typeof(bool), typeof(RBoxView), false, BindingMode.Default,
                            propertyChanged: (bindable, oldValue, newValue) => { ((RBoxView)bindable).Dashed = (bool)newValue; }
                           );
        public bool Dashed
        {
            get { return (bool)this.GetValue(DashedProperty); }
            set { SetValue(DashedProperty, value); }
        }

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create("HasShadow", typeof(bool), typeof(RBoxView), false, BindingMode.Default,
                            propertyChanged: (bindable, oldValue, newValue) => { ((RBoxView)bindable).HasShadow = (bool)newValue; }
                           );
        public bool HasShadow
        {
            get { return (bool)this.GetValue(HasShadowProperty); }
            set { SetValue(HasShadowProperty, value); }
        }

        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create("StartColor", typeof(Color), typeof(RBoxView), Color.Transparent, BindingMode.Default,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((RBoxView)bindable).StartColor = (Color)newValue; }
                   );
        public Color StartColor
        {
            get { return (Color)this.GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create("EndColor", typeof(Color), typeof(RBoxView), Color.Transparent, BindingMode.Default,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((RBoxView)bindable).EndColor = (Color)newValue; }
           );
        public Color EndColor
        {
            get { return (Color)this.GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }
    }
}
