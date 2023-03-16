// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Graphics;
using Android.Graphics.Drawables;
using demo_santiago;
using demo_santiago.Droid.Personalizados;
using Rect = Android.Graphics.Rect;

[assembly: ExportRendererAttribute(typeof(RBoxView), typeof(RBoxViewRenderer))]
namespace demo_santiago.Droid.Personalizados
{
    public class RBoxViewRenderer : BoxRenderer
    {
        Rect rc;
        Rect interior;
        Paint pfill;
        Paint pstroke;
        Paint shadowPaint;
        MaskFilter blur;
        GradientDrawable gradient;
        public RBoxViewRenderer()
        {
            //this.SetWillNotDraw(false);
            rc = new Rect();
            interior = new Rect();
            pfill = new Paint();
            pfill.SetStyle(Paint.Style.Fill);
            pfill.AntiAlias = true;
            pstroke = new Paint();
            pstroke.SetStyle(Paint.Style.Stroke);
            pstroke.AntiAlias = true;
            shadowPaint = new Paint();
            var color = Android.Graphics.Color.Black;
            color.A = (byte)(255 * 0.25);
            shadowPaint.Color = color;
            shadowPaint.AntiAlias = true;
            //shadowCanvas = new Canvas(shadowBitmap);
            gradient = new GradientDrawable();
            gradient.SetOrientation(GradientDrawable.Orientation.TlBr);
        }

        protected override void OnElementChanged(ElementChangedEventArgs<BoxView> e)
        {
            base.OnElementChanged(e);
            RBoxView rbv = (RBoxView)this.Element;
            if (rbv != null)
            {
                ViewGroup.Background.Dispose();
            }
        }

        public override void Draw(Canvas canvas)
        {
            RBoxView rbv = (RBoxView)this.Element;
            GetDrawingRect(rc);
            interior.Left = rc.Left;
            interior.Top = rc.Top;
            interior.Bottom = rc.Bottom;
            interior.Right = rc.Right;
            interior.Inset((int)(Utilities.ConvertDpToPixels((int)rbv.StrokeThickness) / 2.0), (int)(Utilities.ConvertDpToPixels((int)rbv.StrokeThickness) / 2.0));
            var interiorRectFStroke = new RectF(interior);
            interior.Inset((int)(Utilities.ConvertDpToPixels((int)rbv.StrokeThickness) / 2.0), (int)(Utilities.ConvertDpToPixels((int)rbv.StrokeThickness) / 2.0));
            var interiorRectF = new RectF(interior);
            float ra = (Math.Max(0, Math.Min(Utilities.ConvertDpToPixels((int)rbv.BorderRadius), Math.Min(rc.Height() / 2, rc.Width() / 2))));
            float rab = (Math.Max(0, Math.Min(Utilities.ConvertDpToPixels((int)rbv.BotBorderRadius), Math.Min(rc.Height() / 2, rc.Width() / 2))));

            var rcRectF = new RectF(rc);
            if (rbv.HasShadow & rbv.StrokeThickness > 0)
                generateShadowBitmap2(interiorRectF, ra, canvas);

            pfill.Color = rbv.BackgroundColor.ToAndroid();
            ////interiorRectF.OffsetTo(interiorRectF.Left, interiorRectF.Top - Utilities.ConvertDpToPixels(2));
            //canvas.DrawRoundRect(interiorRectF, ra, ra, pfill);
            if (rbv.StartColor != Xamarin.Forms.Color.Transparent & rbv.EndColor != Xamarin.Forms.Color.Transparent)
            {
                gradient.SetColors(new int[] { rbv.StartColor.ToAndroid(), rbv.EndColor.ToAndroid() });
            }
            else
            {
                gradient.SetColor(rbv.BackgroundColor.ToAndroid());
            }
            gradient.Bounds.Set(interior);
            if (rbv.BotBorderRadius < 0)
                gradient.SetCornerRadius(ra);
            else
                gradient.SetCornerRadii(new float[] { ra, ra, ra, ra, rab, rab, rab, rab });

            gradient.Draw(canvas);

            if (rbv.StrokeThickness > 0)
            {
                pstroke.Color = rbv.StrokeColor.ToAndroid();
                pstroke.StrokeWidth = Utilities.ConvertDpToPixels((float)rbv.StrokeThickness);

                if (rbv.Dashed)
                    pstroke.SetPathEffect(new DashPathEffect(new float[] { 60, 20 }, 0));
                canvas.DrawRoundRect(interiorRectFStroke, ra, ra, pstroke);
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //System.Console.WriteLine("Prop: " + e.PropertyName);
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == RBoxView.StrokeColorProperty.PropertyName
                | e.PropertyName == RBoxView.BorderRadiusProperty.PropertyName
                | e.PropertyName == RBoxView.StrokeThicknessProperty.PropertyName
                  | e.PropertyName == RBoxView.HasShadowProperty.PropertyName
                  | e.PropertyName == RBoxView.DashedProperty.PropertyName
                | e.PropertyName == RBoxView.HeightProperty.PropertyName
                | e.PropertyName == RBoxView.WidthProperty.PropertyName
                | e.PropertyName == RBoxView.BackgroundColorProperty.PropertyName
                | e.PropertyName == RBoxView.StartColorProperty.PropertyName
                | e.PropertyName == RBoxView.EndColorProperty.PropertyName
                | e.PropertyName == RBoxView.BotBorderRadiusProperty.PropertyName
               )
                this.Invalidate();

        }

        private void generateShadowBitmap2(RectF interior, float ra, Canvas canvas)
        {
            RBoxView rbv = (RBoxView)this.Element;
            var divider = 4;
            if (Utilities.ConvertDpToPixels(1) <= 2)
                divider = 3;
            if (Utilities.ConvertDpToPixels(1) <= 1)
                divider = 2;

            var shadowBitmap = Bitmap.CreateBitmap((rc.Width() / divider) + 1, rc.Height() / divider, Bitmap.Config.Argb4444);
            var shadowCanvas = new Canvas(shadowBitmap);
            shadowBitmap.EraseColor(Android.Graphics.Color.Transparent);
            if (blur == null)
            {
                var blurRadius = Utilities.ConvertDpToPixels((float)(rbv.StrokeThickness * 0.5)) / ((float)divider);
                blur = new BlurMaskFilter(blurRadius, BlurMaskFilter.Blur.Normal);
                if (blurRadius > 0f)
                    shadowPaint.SetMaskFilter(blur);
            }
            var interiorHelf = new RectF(interior.Left / (int)divider, interior.Top / (int)divider, interior.Right / (int)divider, interior.Bottom / (int)divider);
            shadowCanvas.DrawRoundRect(interiorHelf, ra / (float)divider, ra / (float)divider, shadowPaint);
            canvas.DrawBitmap(shadowBitmap, null, rc, null);
            shadowCanvas.Dispose();
            shadowBitmap.Dispose();
            //Console.WriteLine("---->RBox Renderer: Shadow created: ");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            rc.Dispose();
            interior.Dispose();
            pfill.Dispose();
            pstroke.Dispose();
            shadowPaint.Dispose();
            if (blur != null) blur.Dispose();
        }
    }
}
