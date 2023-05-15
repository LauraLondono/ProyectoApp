using System;
using static demo_santiago.LayoutTools;
using Microcharts;
using Xamarin.Forms;

namespace demo_santiago
{
    public class ViewReports : RelativeLayout
    {
        Microcharts.Forms.ChartView chart;
        public BarChart barChart;
        public DatePicker datePickerInit, datePickerEnd;
        public Button buttonFilter;
        Label labelTitleChart, labelTitleMoreUsed;
        public StackLayout stackWords;
        Label message_title, subtitleReport;
        public ViewReports()
        {
            CreateViews();
            AddViews();
            AddEvents();
        }

        void CreateViews()
        {
            message_title = new Label
            {
                Text = "Reportes",
                TextColor = gb.mainColorBlack,
                FontSize = _h(24),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };

            subtitleReport = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Elige un rango de fecha dentro de los últimos 3 meses",
                TextColor = gb.mainColorBlack,
                FontSize = _h(14)
            };

            chart = new Microcharts.Forms.ChartView();
            barChart = new BarChart ();
            stackWords = new StackLayout();

            datePickerInit = new DatePicker
            {
                Date = DateTime.Today.AddMonths(-3),
                TextColor = Color.Black
            };

            datePickerEnd = new DatePicker
            {
                Date = DateTime.Today,
                TextColor = Color.Black
            };

            buttonFilter = new Button
            {
                Text = "Filtrar",
                FontSize = gb.normalFontSize,
                TextColor = gb.titleColor,
                BackgroundColor = gb.mainColor,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = (int)_h(4)
            };

            labelTitleChart = new Label
            {
                Text = "Número de frases",
                TextColor = gb.textsColor,
                FontSize = _h(14),
                FontAttributes = FontAttributes.Bold
            };

            labelTitleMoreUsed = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Palabras más\nusadas",
                TextColor = gb.textsColor,
                FontSize = _h(14),
                FontAttributes = FontAttributes.Bold
            };

            IsVisible = false;
        }

        void AddViews()
        {
            chart.Chart = barChart;

            Children.Add(message_title,
                                 Constraint.RelativeToParent((p) => { return 0; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.046; }),
                                 Constraint.RelativeToParent((p) => { return p.Width; })
            );

            Children.Add(chart,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.050; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.648; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.453; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.289; })
            );

            Children.Add(labelTitleChart,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.109; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.533; })
            );

            Children.Add(subtitleReport,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.194; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.141; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.610; })
            );

            Children.Add(labelTitleMoreUsed,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.533; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.533; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.373; })
            );

            Children.Add(datePickerInit,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.082; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.264; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.392; })
            );

            Children.Add(datePickerEnd,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.528; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.264; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.392; })
            );

            Children.Add(buttonFilter,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.301; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.395; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.392; })
            );

            Children.Add(stackWords,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.650; }),
                                 Constraint.RelativeToParent((p) => {
                                     double calculated = gb.CalculateHeighViewControl(labelTitleMoreUsed);
                                     return (p.Height * 0.044) + calculated;
                                 }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.314; })
            );

        }

        void AddEvents()
        {
            
        }
    }
}


