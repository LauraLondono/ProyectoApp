using System;
using Xamarin.Forms;
using static demo_santiago.LayoutTools;

namespace demo_santiago
{
	public class BoxMenu: StackLayout
	{
		string text_menu;
		ImageSource icon_menu;
		Color bg_menu;
		Label control_text;
		Image icon;
		public BoxMenu(string title, ImageSource image, Color bg)
		{
			text_menu = title;
			icon_menu = image;
			bg_menu = bg;
			CreateViews();
			AddViews();
		}

		void CreateViews()
		{
			BackgroundColor = bg_menu;
            icon = new Image
			{
				Source = icon_menu,
				Aspect = Aspect.AspectFit,
                Margin = new Thickness(0, _h(12), 0, 0)
            };

            control_text = new Label
			{
				Text = text_menu,
                TextColor = Color.White,
                FontSize = _h(16),
                VerticalTextAlignment = TextAlignment.Center,
				HorizontalTextAlignment = TextAlignment.Center,
            };

        }

        void AddViews()
        {
			Children.Add(icon);
            Children.Add(control_text);
        }
	}
}
