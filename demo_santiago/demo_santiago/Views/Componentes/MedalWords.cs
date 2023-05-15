using System;
using Xamarin.Forms;
using static demo_santiago.LayoutTools;

namespace demo_santiago
{
	public class MedalWords: StackLayout
	{
		Label text_medal;
		Image icon_medal;
		public MedalWords(string text, ImageSource image)
		{
            Orientation = StackOrientation.Horizontal;
            Spacing = _w(15);
            text_medal = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = text,
                TextColor = gb.mainColorBlack,
                FontSize = _h(14)
            };

            icon_medal = new Image
            {
                Source = image,
                Aspect = Aspect.AspectFit,
                WidthRequest = _w(12)
            };

            Children.Add(icon_medal);
            Children.Add(text_medal);
        }
	}
}

