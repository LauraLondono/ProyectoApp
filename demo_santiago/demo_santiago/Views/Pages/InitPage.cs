// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static demo_santiago.LayoutTools;
using Xamarin.Forms;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE;
using System.Diagnostics;
using Plugin.BLE.Abstractions;
using Plugin.BluetoothClassic.Abstractions;

namespace demo_santiago
{
    public class InitPage : ContentPage
    { 
        NavigationBar barraNavegacion;
        RelativeLayout vistaPaginaInicial;
        BLoading bLoading;
        bool click;
        ObservableCollection<BluetoothDeviceModel> deviceList;
        IAdapter Adapter;
        IBluetoothLE Ble;
        ListView listViewDevices;
        Button validatePermissions, bluetoothButton, connectButton;
        BluetoothDeviceModel device_selected;
        private IBluetoothAdapter _bluetoothAdapter;
        DigitViewModel model;
        Label labelText;
        Span span_text, span_complement;

        public InitPage()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            CreateViews();
            AddViews();
            AddEvents();
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
                            connectButton.Text = "Conectando...";
                            connectButton.BackgroundColor = Color.DarkBlue;
                        });
                        break;
                    case ConnectionState.Connected:
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            connectButton.Text = "Desconectar";
                            connectButton.BackgroundColor = Color.Red;
                        });
                        break;
                    case ConnectionState.ErrorOccured:
                        Device.BeginInvokeOnMainThread( async () =>
                        {
                            connectButton.Text = "Conectar";
                            connectButton.BackgroundColor = Color.Green;
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
            if (model != null /*&& !model.Reciving*/)
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
                    span_complement.Text = texto;
                    if (model.Digit == 10)
                        contain = 1;
                });

                
               
                //switch (texto)
                //{
                //    case "Quiero":
                //    case "Me gusta":
                //    case "Deseo":
                //        Device.BeginInvokeOnMainThread(() =>
                //        {
                //            labelText.Text = texto;
                //            texto = "";
                //        });
                //        break;
                //    default:
                        
                //        break;
                //}
                //App.CurrentBluetoothConnection.Transmit(new Memory<byte>(new byte[] { model.Digit }));
            }
        }

        private void RefreshUI()
        {
            if (_bluetoothAdapter.Enabled)
            {
                bluetoothButton.Text = "Inactivar bluetooth";
                bluetoothButton.BackgroundColor = Color.Red;
                connectButton.IsVisible = true;
                FillDevices();
            }
            else
            {
                bluetoothButton.Text = "Activar bluetooth";
                bluetoothButton.BackgroundColor = Color.Green;
                connectButton.IsVisible = false;
                deviceList.Clear();
            }
        }


        void CreateViews()
        {
            bLoading = new BLoading();

            deviceList = new ObservableCollection<BluetoothDeviceModel>();

            vistaPaginaInicial = new RelativeLayout();

            listViewDevices = new ListView
            {
                ItemsSource = deviceList,
                ItemTemplate = new DataTemplate(typeof(DevicesViewCell)),
                SeparatorColor = gb.mainColor,
                Footer = "",
                HasUnevenRows = true,
                BackgroundColor = Color.Yellow,
                IsPullToRefreshEnabled = true,
                RefreshControlColor = gb.mainColor.MultiplyAlpha(.6),
                IsVisible = false
            };

            bluetoothButton = new Button
            {
                Text = "Activar bluetooth",
                FontSize = gb.smallFontSize,
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = (int)_h(4)
            };

            connectButton = new Button
            {
                Text = "Conectar",
                FontSize = gb.smallFontSize,
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = (int)_h(4),
                IsVisible = false
            };

            FormattedString formatted = new FormattedString();

            span_text = new Span { Text = "Mensaje: ", FontSize = gb.smallFontSize };
            span_complement = new Span { FontSize = gb.smallFontSize };

            formatted.Spans.Add(span_text);
            formatted.Spans.Add(span_complement);

            labelText = new Label
            {
                TextColor = Color.Black,
                FormattedText = formatted
            };
        }

        void FillDevices()
        {
            var adapter = DependencyService.Resolve<IBluetoothAdapter>();
            foreach (BluetoothDeviceModel item in adapter.BondedDevices)
            {
                deviceList.Add(item);
            }
        }

        async void CreateViewsOld()
        {
            await gb.permissions.PermissionMethod("bluetooth");
            await gb.permissions.PermissionMethod("ACCESS_FINE_LOCATION");
            barraNavegacion = new NavigationBar(null, "", null);
            bLoading = new BLoading();

            deviceList = new ObservableCollection<BluetoothDeviceModel>();

            Ble = CrossBluetoothLE.Current;
            Adapter = CrossBluetoothLE.Current.Adapter;

            listViewDevices = new ListView
            {
                ItemsSource = deviceList,
                ItemTemplate = new DataTemplate(typeof(DevicesViewCell)),
                SeparatorColor = gb.mainColor,
                Footer = "",
                HasUnevenRows = true,
                BackgroundColor = Color.Yellow,
                IsPullToRefreshEnabled = true,
                RefreshControlColor = gb.mainColor.MultiplyAlpha(.6)
            };

            validatePermissions = new Button
            {
                Text = "VALIDAR PERMISOS",
                FontSize = gb.normalFontSize,
                TextColor = gb.titleColor,
                BackgroundColor = gb.mainColor,
                FontAttributes = FontAttributes.Bold,
                CornerRadius = (int)_h(4)
            };

            vistaPaginaInicial = new RelativeLayout();

        }

        void AddViews()
        {
            double spacing = 0;
            if (Device.RuntimePlatform == Device.iOS)
                spacing = gb.deviceCarrierSpacing;

            //addChild(vistaPaginaInicial, barraNavegacion, _w(0), _h(spacing), _w(375), _h(56));
            addChild(vistaPaginaInicial, bluetoothButton, _w(34), _h(spacing + 46), _w(132), _h(45));
            addChild(vistaPaginaInicial, connectButton, _w(209), _h(spacing + 46), _w(132), _h(45));
            addChild(vistaPaginaInicial, labelText, _w(34), _h(spacing + 142), _w(307));
            addChild(vistaPaginaInicial, listViewDevices, _w(0), _h(spacing + 216), _w(375), _h(431));
            addChild(vistaPaginaInicial, bLoading, _w(0), _h(spacing), _w(375), _h(667));

            Content = vistaPaginaInicial;
        }

        void AddEvents()
        {
            listViewDevices.ItemSelected += ListViewDevices_ItemSelected;
            bluetoothButton.Clicked += BluetoothButton_Clicked;
            connectButton.Clicked += ConnectButton_Clicked;
            //listViewDevices.ItemTapped += ListViewDevices_ItemTapped;
            //listViewDevices.Refreshing += ListViewDevices_Refreshing;
            //validatePermissions.Clicked += ValidatePermissions_Clicked;
        }

        private async void ConnectButton_Clicked(object sender, EventArgs e)
        {
            if (connectButton.Text.Equals("Conectar"))
            {
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
            {
                await DisconnectIfConnectedAsync();
                connectButton.Text = "Conectar";
                connectButton.BackgroundColor = Color.Green;
                span_complement.Text = String.Empty;
            }
        }

        protected override async void OnAppearing()
        {
            _bluetoothAdapter = DependencyService.Resolve<IBluetoothAdapter>();
            var answer = await gb.permissions.PermissionMethod("bluetooth");
            await gb.permissions.PermissionMethod("ACCESS_FINE_LOCATION");

            if (answer)
            {
                RefreshUI();
                await DisconnectIfConnectedAsync();
            }
        }

        private async Task DisconnectIfConnectedAsync()
        {
            if (App.CurrentBluetoothConnection != null)
            {
                try
                {
                    App.CurrentBluetoothConnection.Dispose();
                    span_complement.Text = string.Empty;
                    connectButton.Text = "Conectar";
                    connectButton.BackgroundColor = Color.Green;
                }
                catch (Exception exception)
                {
                    await DisplayAlert("Error", exception.Message, "Close");
                }
            }
        }

        private async void BluetoothButton_Clicked(object sender, EventArgs e)
        {
            if(bluetoothButton.Text.Equals("Activar bluetooth"))
                _bluetoothAdapter.Enable();
            else
            {
                await DisconnectIfConnectedAsync();
                _bluetoothAdapter.Disable();
            }
                
            RefreshUI();
        }

        private async void ValidatePermissions_Clicked(object sender, EventArgs e)
        {
            //var permission_storage = await gb.permissions.PermissionMethod("ACCESS_FINE_LOCATION"); //ES NECESARIO PARA QUE ENCUENTRE DISPOSITIVOS ANDROID !0
            //var permission_storage = await gb.permissions.PermissionMethod("ACCESS_BACKGROUND_LOCATION");
            //var permission_storage = await gb.permissions.PermissionMethod("storage");
            //var permission_connect = await gb.permissions.PermissionMethod("BLUETOOTH_CONNECT");
            //var permission_scan = await gb.permissions.PermissionMethod("BLUETOOTH_SCAN");
            //var permission_admin = await gb.permissions.PermissionMethod("BLUETOOTH_ADMIN");

            //Debug.WriteLine(permission_scan);
            //Debug.WriteLine(permission_connect);
        }

        bool pullRefresh = false;
        private async void ListViewDevices_Refreshing(object sender, EventArgs e)
        {
            if (Ble.State == BluetoothState.Off)
            {
                await DisplayAlert("Atención", "Bluetooth deshabilitado.", "Aceptar");
            }
            else
            {
                if (!pullRefresh)
                {
                    pullRefresh = true;
                    deviceList.Clear(); 

                    Adapter.ScanTimeout = 10000;
                    Adapter.ScanMode = ScanMode.Balanced;

                    Adapter.DeviceDiscovered += (obj, a) =>
                    {
                        //if (!deviceList.Contains(a.Device))
                        //    deviceList.Add(a.Device);
                    };

                    //var scanFilterOptions = new ScanFilterOptions();
                    //scanFilterOptions.DeviceAddresses = new[] { "30:C6:F7:2F:01:BE" };

                    await Adapter.StartScanningForDevicesAsync();
                    listViewDevices.EndRefresh();
                    pullRefresh = false;
                }
            }
        }


        private async void ListViewDevices_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //device_selected = (BluetoothDeviceModel)e.SelectedItem;
            //Received();
            //SendData(device_selected);

            BluetoothDeviceModel bluetoothDeviceModel = e.SelectedItem as BluetoothDeviceModel;
            deviceList.Clear();

            if (bluetoothDeviceModel != null)
            {
                var connected = await TryConnect(bluetoothDeviceModel);
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
                    //await Navigation.PushAsync(new DigitPage());

                }
            }

            ((ListView)sender).SelectedItem = null;
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

        async void SendData(BluetoothDeviceModel device)
        {
            const int bufferSize = 1;
            const int offsetDefault = 0;
            device_selected = device;

            if (device_selected != null)
            {
                var adapter = DependencyService.Resolve<IBluetoothAdapter>();
                using (var connection = adapter.CreateConnection(device_selected))
                {
                    if (await connection.RetryConnectAsync(retriesCount: 2))
                    {
                        byte[] buffer = new byte[bufferSize] { (byte)10 };
                        if (!await connection.RetryTransmitAsync(buffer, offsetDefault, buffer.Length))
                        {
                            await DisplayAlert("Error", "Cannot send data", "Ok");
                        }

                    }
                    else
                    {
                        await DisplayAlert("Error", "Cannot connect device ", "Ok");
                    }
                }
            }
        }

        async void Received()
        {
            const int bufferSize = 1;
            const int offsetDefault = 0;
            
            if (device_selected != null)
            {
                var adapter = DependencyService.Resolve<IBluetoothAdapter>();
                using (var connection = adapter.CreateConnection(device_selected))
                {
                    if (await connection.RetryConnectAsync(retriesCount: 2))
                    {
                        byte[] buffer = new byte[bufferSize];
                        if (!(await connection.RetryReciveAsync(buffer, offsetDefault, buffer.Length)).Succeeded)
                        {
                            await DisplayAlert("Error", "Cannot received data", "Ok");
                        }
                        else
                        {
                            Debug.WriteLine(buffer.FirstOrDefault());
                        }

                    }
                    else
                    {
                        await DisplayAlert("Mensaje recivido", "Cannot to connect", "Ok");
                    }
                }
            }
        }
    }
}