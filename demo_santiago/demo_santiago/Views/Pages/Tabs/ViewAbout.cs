using System;
using static demo_santiago.LayoutTools;
using Xamarin.Forms;

namespace demo_santiago
{
    public class ViewAbout : RelativeLayout
    {
        Label principalText, descriptionText, contactanosText, cellNumber, cellEmail, message_title;
        Image propuse_icon, whapp_icon, email_icon;
        public ViewAbout()
        {
            CreateViews();
            AddViews();
            AddEvents();
        }

        void CreateViews()
        {
            message_title = new Label
            {
                Text = "Acerca de ComunicaTEA",
                TextColor = gb.mainColorBlack,
                FontSize = _h(24),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };

            principalText = new Label
            {
                Text = "Propósito",
                TextColor = gb.mainColorBlack,
                FontSize = _h(18),
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
            };

            descriptionText = new Label
            {
                Text = "Lorem is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged.",
                TextColor = gb.mainColorBlack,
                FontSize = _h(14),
                VerticalTextAlignment = TextAlignment.Center,
            };

            contactanosText = new Label
            {
                Text = "Contáctanos",
                TextColor = gb.mainColorBlack,
                FontSize = _h(18),
                VerticalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold,
            };

            cellNumber = new Label
            {
                Text = "123-456-789",
                TextColor = gb.mainColorBlack,
                FontSize = _h(16),
                VerticalTextAlignment = TextAlignment.Center,
            };

            cellEmail = new Label
            {
                Text = "testmail@gmail.com",
                TextColor = gb.mainColorBlack,
                FontSize = _h(16),
                VerticalTextAlignment = TextAlignment.Center,
            };

            propuse_icon = new Image
            {
                Source = Images.propuse_icon
            };

            whapp_icon = new Image
            {
                Source = Images.whapp_icon
            };

            email_icon = new Image
            {
                Source = Images.email_icon
            };

            IsVisible = false;
        }

        void AddViews()
        {
            Children.Add(message_title,
                                 Constraint.RelativeToParent((p) => { return 0; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.046; }),
                                 Constraint.RelativeToParent((p) => { return p.Width; })
            );

            Children.Add(principalText,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.136; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.206; }),
                                 Constraint.RelativeToParent((p) => { return p.Width; })
            );

            Children.Add(propuse_icon,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.042; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.202; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.082; })
            );

            Children.Add(descriptionText,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.034; }),
                                 Constraint.RelativeToParent((p) => {
                                     return (p.Height * 0.019) + gb.CalculateHeighViewControl(principalText);
                                 }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.874; })
            );

            Children.Add(contactanosText,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.034; }),
                                 Constraint.RelativeToParent((p) => {
                                     return (p.Height * 0.076) + gb.CalculateHeighViewControl(descriptionText);
                                 })
            );

            Children.Add(cellNumber,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.176; }),
                                 Constraint.RelativeToParent((p) => {
                                     return (p.Height * 0.029) + gb.CalculateHeighViewControl(contactanosText);
                                 })
            );

            Children.Add(whapp_icon,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.074; }),
                                 Constraint.RelativeToParent((p) => {
                                     return (p.Height * 0.026) + gb.CalculateHeighViewControl(contactanosText);
                                 }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.082; })
            );

            Children.Add(cellEmail,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.176; }),
                                 Constraint.RelativeToParent((p) => {
                                     return (p.Height * 0.013) + gb.CalculateHeighViewControl(cellNumber);
                                 })
            );

            Children.Add(email_icon,
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.074; }),
                                 Constraint.RelativeToParent((p) => {
                                     return (p.Height * 0.013) + gb.CalculateHeighViewControl(whapp_icon);
                                 }),
                                 Constraint.RelativeToParent((p) => { return p.Width * 0.082; })
            );
        }

        void AddEvents()
        {
            
        }
    }
}


