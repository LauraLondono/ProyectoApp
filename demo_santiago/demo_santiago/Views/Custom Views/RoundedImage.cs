// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public class RoundedImage : Image
    {
        public static readonly BindableProperty BorderRadiusProperty =BindableProperty.Create("BorderRadius", typeof(double), typeof(RoundedImage), 0.0, BindingMode.Default,
                        propertyChanged: (bindable, oldValue, newValue) => { ((RoundedImage)bindable).BorderRadius = (double)newValue; }
                       );

        public double BorderRadius
        {
            get { return (double)this.GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

        public static readonly BindableProperty StrokeColorProperty =
            BindableProperty.Create("StrokeColor", typeof(Color), typeof(RoundedImage), Color.Transparent, BindingMode.Default,
                                             propertyChanged: (bindable, oldValue, newValue) =>
                                             {
                                                 ((RoundedImage)bindable).StrokeColor = (Color)newValue;
                                             }
                                           );
        public Color StrokeColor
        {
            get { return (Color)this.GetValue(StrokeColorProperty); }
            set { SetValue(StrokeColorProperty, value); }
        }

        public static readonly BindableProperty StrokeThicknessProperty =
            BindableProperty.Create("StrokeThickness", typeof(double), typeof(RoundedImage), 0.0, BindingMode.Default,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((RoundedImage)bindable).StrokeThickness = (double)newValue; }
                                   );
        public double StrokeThickness
        {
            get { return (double)this.GetValue(StrokeThicknessProperty); }
            set { SetValue(StrokeThicknessProperty, value); }
        }

        //public static readonly BindableProperty OuterStrokeColorProperty =
        //          BindableProperty.Create("OuterStrokeColor", typeof(Color), typeof(RoundedImage), Color.Black, BindingMode.Default,
        //                                           propertyChanged: (bindable, oldValue, newValue) =>
        //                                           {
        //                                               ((RoundedImage)bindable).OuterStrokeColor = (Color)newValue;
        //                                           }
        //                                         );
        //public Color OuterStrokeColor
        //{
        //  get { return (Color)this.GetValue(OuterStrokeColorProperty); }
        //  set { SetValue(OuterStrokeColorProperty, value); }
        //}

        //public static readonly BindableProperty OuterStrokeThicknessProperty =
        //  BindableProperty.Create("OuterStrokeThickness", typeof(double), typeof(RoundedImage), 0.0, BindingMode.Default,
        //                          propertyChanged: (bindable, oldValue, newValue) => { ((RoundedImage)bindable).OuterStrokeThickness = (double)newValue; }
        //                         );
        //public double OuterStrokeThickness
        //{
        //  get { return (double)this.GetValue(OuterStrokeThicknessProperty); }
        //  set { SetValue(OuterStrokeThicknessProperty, value); }
        //}
    }
}
