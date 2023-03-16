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
        public ViewReports()
        {
            CreateViews();
            AddViews();
            AddEvents();
        }

        void CreateViews()
        {
            chart = new Microcharts.Forms.ChartView();
            barChart = new BarChart();
            stackWords = new StackLayout();

            datePickerInit = new DatePicker
            {
                Date = DateTime.Today.AddMonths(-3)
            };

            datePickerEnd = new DatePicker
            {
                Date = DateTime.Today
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
                FontSize = _h(14)
            };

            labelTitleMoreUsed = new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                Text = "Palabras más\nusadas",
                TextColor = gb.textsColor,
                FontSize = _h(14),
            };

            IsVisible = false;
        }

        void AddViews()
        {
            chart.Chart = barChart;
            Children.Add(chart,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.050; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.470; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.453; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.289; })
            );

            Children.Add(labelTitleChart,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.109; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.360; })
            );
            
            Children.Add(labelTitleMoreUsed,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.533; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.360; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.373; })
            );

            Children.Add(datePickerInit,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.082; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.080; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.392; })
            );

            Children.Add(datePickerEnd,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.528; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.080; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.392; })
            );

            Children.Add(buttonFilter,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.301; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.205; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.392; })
            );

            Children.Add(stackWords,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.650; }),
                                 Constraint.RelativeToParent((p) => {
                                     double calculated = gb.CalculateHeighViewControl(labelTitleMoreUsed);
                                     return (p.Height * 0.034) + calculated;
                                 }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.314; })
            );

        }

        void AddEvents()
        {
            
        }
    }
}


