// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static demo_santiago.LayoutTools;

using Xamarin.Forms;

namespace demo_santiago
{
    public class DevicesViewCell : ViewCell
    {
        StackLayout viewEstiloCapacitacion;
        Label nombreCapacitacion;
        public DevicesViewCell()
        {
            CreateViews();
            AddViews();
        }

        void CreateViews()
        {
            viewEstiloCapacitacion = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                VerticalOptions = LayoutOptions.Center,
                Padding = new Thickness(10,0,0,0)
            };

            nombreCapacitacion = new Label
            {
                TextColor = gb.textsColor,
                FontSize = _h(9),
                HorizontalOptions = LayoutOptions.StartAndExpand,
                VerticalTextAlignment = TextAlignment.Center
            };

            FormattedString formatted = new FormattedString();

            Span span_name = new Span();
            Span span_id = new Span();

            formatted.Spans.Add(span_name);
            formatted.Spans.Add(span_id);

            span_name.SetBinding(Span.TextProperty, "Name");
            span_id.SetBinding(Span.TextProperty, "Address");

            nombreCapacitacion.FormattedText = formatted;
        }

        void AddViews()
        {
            viewEstiloCapacitacion.Children.Add(nombreCapacitacion);

            View = viewEstiloCapacitacion;
        }
    }
}