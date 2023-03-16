using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace demo_santiago
{
    public class ViewMessages : RelativeLayout
    {
        public ListView listViewMessages;
        public ObservableCollection<Messages_DB> messages;
        public ViewMessages()
        {
            CreateViews();
            AddViews();
            AddEvents();
        }

        void CreateViews()
        {
            messages = new ObservableCollection<Messages_DB>();
            listViewMessages = new ListView
            {
                ItemsSource = messages,
                ItemTemplate = new DataTemplate(typeof(MessageStyle)),
                SeparatorColor = gb.mainColor,
                HasUnevenRows = true,
                IsPullToRefreshEnabled = true
            };

            IsVisible = true;
        }

        void AddViews()
        {
            double spacing = 0;
            if (Device.RuntimePlatform == Device.iOS)
                spacing = gb.deviceCarrierSpacing;

            Children.Add(listViewMessages,
                                 Constraint.RelativeToParent((p) => { return 0; }),
                                 Constraint.RelativeToParent((p) => { return 0; }),
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


