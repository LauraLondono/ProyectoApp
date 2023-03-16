using System;
using static demo_santiago.LayoutTools;

using Xamarin.Forms;
using Plugin.BluetoothClassic.Abstractions;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;
using System.Diagnostics;
using System.Text;
using System.Collections.Generic;

namespace demo_santiago
{
    public class HomePage : ContentPage
    {
        RelativeLayout principalView;
        BoxMenu boxMessages, boxReports, boxAbout;
        NavigationBar navigationBar;
        ViewMessages viewMessages;
        ViewReports viewReports;
        ViewAbout viewAbout;
        private IBluetoothAdapter _bluetoothAdapter;
        ObservableCollection<BluetoothDeviceModel> deviceList;
        BLoading bLoading;
        DigitViewModel model;
        DataAccess data_access;

        public HomePage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            CreateViews();
            AddViews();
            AddEvents();
        }

        void CreateViews()
        {
            deviceList = new ObservableCollection<BluetoothDeviceModel>();

            principalView = new RelativeLayout();

            viewMessages = new ViewMessages();

            viewReports = new ViewReports();

            viewAbout = new ViewAbout();

            navigationBar = new NavigationBar(null,"Mensajes");

            boxMessages = new BoxMenu("Mensajes", Images.messages_icon, gb.mainColorBlack);

            boxReports = new BoxMenu("Reportes", Images.report_icon, gb.mainColor);

            boxAbout = new BoxMenu("Acerca de", Images.about_icon, gb.mainColor);

            bLoading = new BLoading();
        }

        void AddViews()
        {
            double spacing = 0;
            if (Device.RuntimePlatform == Device.iOS)
                spacing = gb.deviceCarrierSpacing;

            addChild(principalView, navigationBar, _w(0), _h(spacing), _w(375), _h(56));
            addChild(principalView, boxMessages, _w(0), _h(spacing + 597), _w(125), _h(70));
            addChild(principalView, boxReports, _w(125), _h(spacing + 597), _w(125), _h(70));
            addChild(principalView, boxAbout, _w(250), _h(spacing + 597), _w(125), _h(70));
            addChild(principalView, viewMessages, _w(0), _h(spacing + 76), _w(375), _h(521));
            addChild(principalView, viewReports, _w(0), _h(spacing + 76), _w(375), _h(521));
            addChild(principalView, viewAbout, _w(0), _h(spacing + 76), _w(375), _h(521));
            addChild(principalView, bLoading, _w(0), _h(spacing), _w(375), _h(667));

            Content = principalView;
        }

        void AddEvents()
        {
            Core.addClick(boxMessages, () => {
                if (!viewMessages.IsVisible)
                {
                    boxMessages.BackgroundColor = gb.mainColorBlack;
                    boxReports.BackgroundColor = gb.mainColor;
                    boxAbout.BackgroundColor = gb.mainColor;
                    viewMessages.IsVisible = true;
                    viewReports.IsVisible = false;
                    viewAbout.IsVisible = false;
                    navigationBar.textoTitulo.Text = "Mensajes";
                }
            });

            Core.addClick(boxReports, async () => {
                if (!viewReports.IsVisible)
                {
                    boxReports.BackgroundColor = gb.mainColorBlack;
                    boxMessages.BackgroundColor = gb.mainColor;
                    boxAbout.BackgroundColor = gb.mainColor;
                    await FillCharts();
                    viewMessages.IsVisible = false;
                    viewReports.IsVisible = true;
                    viewAbout.IsVisible = false;
                    navigationBar.textoTitulo.Text = "Reportes";
                }
            });

            Core.addClick(boxAbout, () => {
                if (!viewAbout.IsVisible)
                {
                    boxAbout.BackgroundColor = gb.mainColorBlack;
                    boxMessages.BackgroundColor = gb.mainColor;
                    boxReports.BackgroundColor = gb.mainColor;
                    viewMessages.IsVisible = false;
                    viewReports.IsVisible = false;
                    viewAbout.IsVisible = true;
                    navigationBar.textoTitulo.Text = "Acerca de";
                }
            });

            navigationBar.buttonBluetooth.Clicked += ButtonBluetooth_Clicked;
            viewMessages.listViewMessages.ItemTapped += ListViewMessages_ItemTapped;
            viewMessages.listViewMessages.Refreshing += ListViewMessages_Refreshing;
            viewReports.buttonFilter.Clicked += ButtonFilter_Clicked;
        }

        async Task FillCharts()
        {
            List<Messages_DB> groupList = await data_access.GetGroupByMessages(viewReports.datePickerInit.Date.ToString(), viewReports.datePickerEnd.Date.ToString());
            List<Microcharts.ChartEntry> micro = new List<Microcharts.ChartEntry>();
            Random rnd = new Random();
            string lastmonth = "";
            string today = "";
            lastmonth = DateTime.Parse(viewReports.datePickerInit.Date.ToString()).ToString("yyyy-MM-dd");
            today = DateTime.Parse(viewReports.datePickerEnd.Date.ToString()).ToString("yyyy-MM-dd");
            List<Messages_DB_Quantity> listTemp = new List<Messages_DB_Quantity>();

            foreach (var message in groupList)
            {
                int contador = viewMessages.messages
                    .Where(it => it.Message.ToUpper().Equals(message.Message.ToUpper()))
                    .Where(element => (DateTime.Parse(element.Date_Created.Split(' ')[0]) >= DateTime.Parse(lastmonth) && DateTime.Parse(element.Date_Created.Split(' ')[0]) <= DateTime.Parse(today)))
                    .Count();

                listTemp.Add(new Messages_DB_Quantity { Item = message, Count = contador });

                micro.Add(new Microcharts.ChartEntry(contador)
                {
                    Label = message.Message,
                    Color = SkiaSharp.SKColor.FromHsl(rnd.Next(256), rnd.Next(256), rnd.Next(256)),
                    ValueLabel = contador.ToString(),

                });
            }

            var order = (from it in listTemp
                         orderby it.Count descending
                         select it).Take(3).ToList();

            viewReports.stackWords.Children.Clear();
            int count = 0;
            foreach (var item in order)
            {
                count++;
                switch (count)
                {
                    case 1:
                        viewReports.stackWords.Children.Add(new MedalWords(item.Item.Message, Images.medal_gold_icon));
                        break;
                    case 2:
                        viewReports.stackWords.Children.Add(new MedalWords(item.Item.Message, Images.medal_gray_icon));
                        break;
                    case 3:
                        viewReports.stackWords.Children.Add(new MedalWords(item.Item.Message, Images.medal_orange_icon));
                        break;
                    default:
                        break;
                }
                
            }
            viewReports.barChart.Entries = micro;
        }

        bool clickChart = false;
        private async void ButtonFilter_Clicked(object sender, EventArgs e)
        {
            if (clickChart) return;
            clickChart = true;
            await FillCharts();
            clickChart = false;
        }

        private void ListViewMessages_Refreshing(object sender, EventArgs e)
        {
            viewMessages.listViewMessages.IsRefreshing = true;
            Refreshing();
            viewMessages.listViewMessages.IsRefreshing = false;
        }

        private async void ListViewMessages_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            Messages_DB message_bd = e.Item as Messages_DB;

            var answer = await DisplayAlert("¿Seguro que desea eliminar el mensaje?", "No lo podrá recuperar después", "Aceptar", "Cancelar");
            if (answer)
            {
                data_access.DeleteMessage(message_bd);
                viewMessages.messages.Remove(message_bd);
            }
        }

        protected override async void OnAppearing()
        {
            _bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
            data_access = new DataAccess();
            var answer = await gb.permissions.PermissionMethod("bluetooth");
            await gb.permissions.PermissionMethod("ACCESS_FINE_LOCATION");

            if (answer)
            {
                RefreshUI();
                await DisconnectIfConnectedAsync();
                navigationBar.buttonBluetooth.BackgroundColor = Color.Red;
                FillDevices();
                Refreshing();
            }
        }

        async void Refreshing()
        {
            List<Messages_DB> messages = await data_access.GetAllMessages();
            viewMessages.messages.Clear();
            foreach (Messages_DB message in messages)
            {
                viewMessages.messages.Add(message);
            }
        }

        private void RefreshUI()
        {
            //if (_bluetoothAdapter.Enabled)
            //{
            //    bluetoothButton.Text = "Inactivar bluetooth";
            //    bluetoothButton.BackgroundColor = Color.Red;
            //    connectButton.IsVisible = true;
            //    FillDevices();
            //}
            //else
            //{
            //    bluetoothButton.Text = "Activar bluetooth";
            //    bluetoothButton.BackgroundColor = Color.Green;
            //    connectButton.IsVisible = false;
            //    deviceList.Clear();
            //}
        }

        void FillDevices()
        {
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            foreach (BluetoothDeviceModel item in adapter.BondedDevices)
            {
                deviceList.Add(item);
            }
        }

        private async Task DisconnectIfConnectedAsync()
        {
            if (App.CurrentBluetoothConnection != null)
            {
                try
                {
                    App.CurrentBluetoothConnection.Dispose();
                    //span_complement.Text = string.Empty;
                    navigationBar.buttonBluetooth.BackgroundColor = Color.Red;
                }
                catch (Exception exception)
                {
                    await DisplayAlert("Error", exception.Message, "Close");
                }
            }
        }

        private async Task<bool> TryConnect(BluetoothDeviceModel bluetoothDeviceModel)
        {
            const bool Connected = true;
            const bool NotConnected = false;


            var connection = _bluetoothAdapter.CreateManagedConnection(bluetoothDeviceModel);
            try
            {
                connection.Connect();
                App.CurrentBluetoothConnection = connection;

                return Connected;
            }
            catch (BluetoothConnectionException exception)
            {
                await DisplayAlert("Connection error",
                    $"Can not connect to the device: {bluetoothDeviceModel.Name}({bluetoothDeviceModel.Address}).\n" +
                        $"Exception: \"{exception.Message}\"\n" +
                        "Please, try another one.",
                    "Close");

                return NotConnected;
            }
            catch (Exception exception)
            {
                await DisplayAlert("Generic error", exception.Message, "Close");

                return NotConnected;
            }

        }

        private async void ButtonBluetooth_Clicked(object sender, EventArgs e)
        {
            if (navigationBar.buttonBluetooth.BackgroundColor == Color.Red)
            {
                if (_bluetoothAdapter.Enabled)
                {
                    var exist = deviceList.Where(element => element.Address.Equals("30:C6:F7:2F:01:BE")).Count();
                    if (exist == 0)
                    {
                        await DisplayAlert("Notificación", "Asegurate que tengas el dispositivo vinculado con el celular e intentalo de nuevo", "Aceptar");
                        return;
                    }

                    BluetoothDeviceModel bluetoothDeviceModel = deviceList.First(el => { return el.Address.Equals("30:C6:F7:2F:01:BE"); });
                    if (bluetoothDeviceModel != null)
                    {
                        bLoading.IsVisible = true;
                        var connected = await TryConnect(bluetoothDeviceModel);
                        bLoading.IsVisible = false;
                        if (connected)
                        {
                            if (App.CurrentBluetoothConnection != null)
                            {
                                App.CurrentBluetoothConnection.OnStateChanged += CurrentBluetoothConnection_OnStateChanged;
                                App.CurrentBluetoothConnection.OnRecived += CurrentBluetoothConnection_OnRecived;
                                App.CurrentBluetoothConnection.OnError += CurrentBluetoothConnection_OnError;

                                model = new DigitViewModel();
                                model.PropertyChanged += Model_PropertyChanged;
                            }
                        }
                    }
                }
                else
                    await DisplayAlert("Notificación", "Debes activar el bluetooth para conectarte a tu dispositivo", "Aceptar");
            }
            else if(navigationBar.buttonBluetooth.BackgroundColor == Color.Green)
            {
                await DisconnectIfConnectedAsync();
            }
        }

        private void CurrentBluetoothConnection_OnStateChanged(object sender, StateChangedEventArgs stateChangedEventArgs)
        {
            Debug.WriteLine(stateChangedEventArgs.ConnectionState);
            if (model != null)
            {
                model.ConnectionState = stateChangedEventArgs.ConnectionState;
                switch (stateChangedEventArgs.ConnectionState)
                {
                    case ConnectionState.Connecting:
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            navigationBar.buttonBluetooth.BackgroundColor = Color.Yellow;
                        });
                        break;
                    case ConnectionState.Connected:
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            navigationBar.buttonBluetooth.BackgroundColor = Color.Green;
                        });
                        break;
                    case ConnectionState.ErrorOccured:
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            navigationBar.buttonBluetooth.BackgroundColor = Color.Red;
                            await DisconnectIfConnectedAsync();
                            await DisplayAlert("Notificación", "No se pudo realizar la conexión, revise que el dispositivo este encendido", "Aceptar");
                        });
                        break;
                    default:
                        break;
                }


            }
        }

        private void CurrentBluetoothConnection_OnRecived(object sender, Plugin.BluetoothClassic.Abstractions.RecivedEventArgs recivedEventArgs)
        {
            if (model != null)
            {
                model.SetReciving();

                for (int index = 0; index < recivedEventArgs.Buffer.Length; index++)
                {
                    byte value = recivedEventArgs.Buffer.ToArray()[index];
                    model.Digit = value;
                }

                model.SetRecived();
            }
        }

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == DigitViewModel.Properties.Digit.ToString())
            {
                TransmitCurrentDigit();
            }
        }

        private void CurrentBluetoothConnection_OnError(object sender, System.Threading.ThreadExceptionEventArgs errorEventArgs)
        {
            if (errorEventArgs.Exception is BluetoothDataTransferUnitException)
            {
                TransmitCurrentDigit();
            }
        }

        string texto = "";
        int contain = 0;
        private void TransmitCurrentDigit()
        {
            if (model != null)
            {
                if (contain == 1)
                {
                    texto = "";
                    contain = 0;
                }

                if (model.Digit != 10)
                    texto += Encoding.Default.GetString(new byte[] { model.Digit });

                Device.BeginInvokeOnMainThread(() =>
                {
                    if (model.Digit == 10)
                    {
                        if (contain == 0)
                        {
                            DateTime now = DateTime.Now;
                            DateTime utc = DateTime.UtcNow;

                            Messages_DB message = new Messages_DB { Message = texto, Date_Created = DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") };

                            viewMessages.messages.Insert(0, message);
                            data_access.InserMessage(message);
                        }
                        contain = 1;
                    }
                });
            }
        }
    }
}


