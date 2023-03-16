// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public class MyEntry : Entry
    {
        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create("HasBorder", typeof(bool), typeof(MyEntry), true, BindingMode.Default,
                                    propertyChanged: (bindable, oldValue, newValue) => { ((MyEntry)bindable).HasBorder = (bool)newValue; }
                                   );
        public bool HasBorder
        {
            get { return (bool)this.GetValue(HasBorderProperty); }
            set { SetValue(HasBorderProperty, value); }
        }

        public bool ellipsis { get; set; } = false;

        public bool EndEditing { get; set; } = true;
        public bool returnKeyDefault { get; set; } = Device.RuntimePlatform == Device.Android ? true : true;
        public Action returnAction { get; set; } = null;

        public static readonly BindableProperty ReturnTypeProperty = BindableProperty.Create(
        nameof(ReturnType),
        typeof(ReturnType),
        typeof(MyEntry),
        ReturnType.Done,
                    BindingMode.OneWay
        );

        public ReturnType ReturnType
        {
            get { return (ReturnType)GetValue(ReturnTypeProperty); }
            set { SetValue(ReturnTypeProperty, value); }
        }

        //protected override void OnPropertyChanging(string propertyName = null)
        //{
        //  base.OnPropertyChanging(propertyName);
        //  if (propertyName == MyEntry.IsFocusedProperty.PropertyName)
        //  {
        //      Debug.WriteLine("Changing isFocused: " + this.Placeholder + "-> " + IsFocused);
        //  }
        //}

        //protected override void OnPropertyChanged(string propertyName = null)
        //{
        //  base.OnPropertyChanging(propertyName);
        //  if (propertyName == MyEntry.IsFocusedProperty.PropertyName)
        //  {
        //      Debug.WriteLine("Changed isFocused: " + this.Placeholder + "-> " + IsFocused);
        //  }
        //}
    }

    public enum ReturnType
    {
        Go,
        Next,
        Done,
        Send,
        Search
    }
}
