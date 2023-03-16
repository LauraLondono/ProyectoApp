// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Android.Graphics;
using demo_santiago;
using demo_santiago.Droid.Personalizados;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Rect = Android.Graphics.Rect;

[assembly: ExportRenderer(typeof(RoundedImage), typeof(RoundedImageRenderer))]
namespace demo_santiago.Droid.Personalizados
{
    [Obsolete]
    public class RoundedImageRenderer : ImageRenderer
    {
        float rai = 0;
        protected override bool DrawChild(Android.Graphics.Canvas canvas, Android.Views.View child, long drawingTime)
        {
            try
            {
                RoundedImage rbv = (RoundedImage)this.Element;
                var s = rbv.Scale;
                float ra = Utilities.ConvertDpToPixels((int)rbv.BorderRadius);
                if (rai == 0) rai = ra;
                ra = (float)(Math.Max(0, Math.Min(ra, Math.Min(Utilities.ConvertDpToPixels((float)(this.Element.Width / 2.0)), Utilities.ConvertDpToPixels((float)(this.Element.Height / 2.0))))));
                var rect = new Rect();
                GetDrawingRect(rect);
                //Create path to clip
                var path = new Path();
                rect = new Rect((int)(rect.Left * s), (int)(rect.Top * s), (int)(rect.Right * s), (int)(rect.Bottom * s));
                path.AddRoundRect(new RectF(rect), ra, ra, Path.Direction.Ccw);
                canvas.Save();
                canvas.ClipPath(path);
                var result = base.DrawChild(canvas, child, drawingTime);

                canvas.Restore();
                var insetValue = (int)(Utilities.ConvertDpToPixels((int)(rbv.StrokeThickness * s)) / 2.0);
                rect.Inset(insetValue, insetValue);
                //Create outer border
                //var offset = (int)Utilities.ConvertDpToPixels((int)rbv.OuterStrokeThickness)/2;
                //path = new Path();
                //path.AddRoundRect(new RectF(rect), ra, ra, Path.Direction.Ccw);
                var paint = new Paint();
                //paint.AntiAlias = true;
                //paint.StrokeWidth =  Utilities.ConvertDpToPixels((int)rbv.OuterStrokeThickness);
                //paint.SetStyle(Paint.Style.Stroke);
                //paint.Color = rbv.OuterStrokeColor.ToAndroid();
                //canvas.DrawPath(path, paint);

                //rect.Inset(offset, offset);
                // Create path for circle border
                path = new Path();
                path.AddRoundRect(new RectF(rect), ra, ra, Path.Direction.Ccw);
                paint = new Paint();
                paint.AntiAlias = true;
                paint.StrokeWidth = Utilities.ConvertDpToPixels((int)rbv.StrokeThickness);
                paint.SetStyle(Paint.Style.Stroke);
                paint.Color = rbv.StrokeColor.ToAndroid();
                canvas.DrawPath(path, paint);

                //Properly dispose
                path.Dispose();
                paint.Dispose();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Unable to create circle image: " + ex);
            }
            return base.DrawChild(canvas, child, drawingTime);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Image> e)
        {
            base.OnElementChanged(e);
            var image = e.NewElement;

        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //System.Console.WriteLine("Prop: " + e.PropertyName);
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == RoundedImage.StrokeColorProperty.PropertyName
                | e.PropertyName == RoundedImage.BorderRadiusProperty.PropertyName
                | e.PropertyName == RoundedImage.StrokeThicknessProperty.PropertyName
                | e.PropertyName == RoundedImage.HeightProperty.PropertyName
                | e.PropertyName == RoundedImage.WidthProperty.PropertyName
                | e.PropertyName == RoundedImage.ScaleProperty.PropertyName)
                this.Invalidate();

        }

    }
}
