// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public class ShadowLabel : AbsoluteLayout
    {
        public Label label;
        public RBoxView bg;
        public double shadowThickness;
        public string Text
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        public new Color BackgroundColor
        {
            get { return bg.BackgroundColor; }
            set { bg.BackgroundColor = value; }
        }

        public Color TextColor
        {
            get { return label.TextColor; }
            set { label.TextColor = value; }
        }

        public ShadowLabel(double w, double h, double shadow, bool noLabel = false)
        {
            this.WidthRequest = w + shadow * 2;
            this.HeightRequest = h + shadow * 2;

            bg = new RBoxView()
            {
                StrokeThickness = shadow,
                HasShadow = true,
                BorderRadius = 0,
                BackgroundColor = Color.Transparent,
                StrokeColor = Color.Transparent,
                InputTransparent = true,
            };
            Children.Add(bg);

            AbsoluteLayout.SetLayoutBounds(bg, new Rectangle(0, 0, WidthRequest, HeightRequest));
            AbsoluteLayout.SetLayoutFlags(bg, AbsoluteLayoutFlags.None);

            if (!noLabel)
            {
                label = new Label()
                {
                    InputTransparent = true,
                };
                AbsoluteLayout.SetLayoutBounds(label, new Rectangle(shadow, shadow, w, h));
                AbsoluteLayout.SetLayoutFlags(label, AbsoluteLayoutFlags.None);
                Children.Add(label);

                label.TranslationX = -shadow;
                label.TranslationY = -shadow;
            }

            bg.TranslationX = -shadow;
            bg.TranslationY = -shadow;
        }
    }
}
