// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Android.App;
using Android.Views;
using Android.Views.InputMethods;
using Android.Widget;
using demo_santiago;
using demo_santiago.Droid.Personalizados;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

#pragma warning disable CS0612 // El tipo o el miembro están obsoletos
[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
#pragma warning restore CS0612 // El tipo o el miembro están obsoletos
namespace demo_santiago.Droid.Personalizados
{
    [Obsolete]
    public class MyEntryRenderer : EntryRenderer
    {
        //private Android.Widget.EditText native;
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            updateBorder();

            if (e.OldElement != null)
            {
                Control.EditorAction -= HandleEditorAction;
            }
            if (e.NewElement != null)
            {
                updateBorder();
                SetReturnType(Element as MyEntry);
                Control.EditorAction += HandleEditorAction;
            }
        }

        void HandleEditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            e.Handled = false;
            var returnAction = ((MyEntry)Element).returnAction;
            if (returnAction != null) returnAction();
            if (e.ActionId == ImeAction.Done | e.ActionId == ImeAction.Search)
            {
                if (((MyEntry)Element).returnKeyDefault)
                    ((MyEntry)Element).Unfocus();
            } else
                ((IEntryController)Element).SendCompleted();

            e.Handled = true;
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            //System.Console.WriteLine("Prop: " + e.PropertyName);
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == MyEntry.HasBorderProperty.PropertyName
                | e.PropertyName == MyEntry.HorizontalTextAlignmentProperty.PropertyName)
                updateBorder();

            if (e.PropertyName == MyEntry.ReturnTypeProperty.PropertyName)
            {
                SetReturnType(Element as MyEntry);
            }
        }

        void updateBorder()
        {
            if (Element == null) return;
            EditText textField = (EditText)Control;

            if (((MyEntry)Element).HorizontalTextAlignment == Xamarin.Forms.TextAlignment.Center)
                textField.Gravity = Android.Views.GravityFlags.Center;
            else
                textField.Gravity = Android.Views.GravityFlags.CenterVertical;
            textField.SetPadding(textField.PaddingLeft, 0, textField.PaddingRight, 0);
            if (((MyEntry)Element).HasBorder)
                textField.Background.Alpha = 255;
            else
            {
                textField.Background.Alpha = 0;
            }

            if (((MyEntry)Element).ellipsis)
            {
                //textField.SetMaxLines(1);
                //textField.InputType = Android.Text.InputTypes.TextFlagNoSuggestions;
                //textField.Ellipsize = Android.Text.TextUtils.TruncateAt.Marquee;
                //textField.SetSingleLine(true);
            }

        }

        void SetReturnType(MyEntry entry)
        {
            ReturnType type = entry.ReturnType;

            switch (type)
            {
                case ReturnType.Go:
                    Control.ImeOptions = ImeAction.Go;
                    Control.SetImeActionLabel("Go", ImeAction.Go);
                    break;
                case ReturnType.Next:
                    Control.ImeOptions = ImeAction.Next;
                    Control.SetImeActionLabel("Next", ImeAction.Next);
                    break;
                case ReturnType.Send:
                    Control.ImeOptions = ImeAction.Send;
                    Control.SetImeActionLabel("Send", ImeAction.Send);
                    break;
                case ReturnType.Search:
                    Control.ImeOptions = ImeAction.Search;
                    Control.SetImeActionLabel("Search", ImeAction.Search);
                    break;
                default:
                    Control.ImeOptions = ImeAction.Done;
                    Control.SetImeActionLabel("Done", ImeAction.Done);
                    break;
            }
        }

    }
}
