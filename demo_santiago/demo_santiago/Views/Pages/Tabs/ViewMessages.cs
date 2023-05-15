using System;
using System.Collections.ObjectModel;
using static demo_santiago.LayoutTools;
using Xamarin.Forms;

namespace demo_santiago
{
    public class ViewMessages : RelativeLayout
    {
        public ListView listViewMessages;
        public ObservableCollection<Messages_DB> messages;
        Label message_title;
        public ViewMessages()
        {
            CreateViews();
            AddViews();
            AddEvents();
        }

        void CreateViews()
        {
            messages = new ObservableCollection<Messages_DB>();
            message_title = new Label
            {
                Text = "Mensajes",
                TextColor = gb.mainColorBlack,
                FontSize = _h(24),
                VerticalTextAlignment = TextAlignment.Center,
                HorizontalTextAlignment = TextAlignment.Center,
                FontAttributes = FontAttributes.Bold
            };

            listViewMessages = new ListView
            {
                ItemsSource = messages,
                ItemTemplate = new DataTemplate(typeof(MessageStyle)),
                SeparatorColor = gb.mainColor,
                HasUnevenRows = true,
                IsPullToRefreshEnabled = true,
                RowHeight = (int)_h(0.089)
            };

            IsVisible = true;
        }

        void AddViews()
        {
            double spacing = 0;
            if (Device.RuntimePlatform == Device.iOS)
                spacing = gb.deviceCarrierSpacing;
            
            Children.Add(message_title,
                                 Constraint.RelativeToParent((p) => { return 0; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.046; }),
                                 Constraint.RelativeToParent((p) => { return p.Width; })
            );

            Children.Add(listViewMessages,
                                 Constraint.RelativeToParent((p) => { return 0; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * 0.191; }),
                                 Constraint.RelativeToParent((p) => { return p.Width; }),
                                 Constraint.RelativeToParent((p) => { return p.Height; })
            );
        }

        void AddEvents()
        {
            listViewMessages.ItemSelected += ListViewMessages_ItemSelected;
        }

        private void ListViewMessages_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }
    }
}


