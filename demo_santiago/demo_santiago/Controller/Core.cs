// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace demo_santiago
{
    public static class Core
    {
        static async Task<bool> HandleFunc()
        {
            return true;
        }

        public static ActivityIndicator aIndicator = new ActivityIndicator()
        {
            IsRunning = true,
            IsVisible = true,
        };

        public static TaskCompletionSource<bool> lastPoup;

        public static async void init()
        {
            initStaticVariables();
            gb.screenWidth = DependencyService.Get<IUtilities>().screenWidth();
            gb.screenHeight = DependencyService.Get<IUtilities>().screenHeight();
            gb.deviceCarrierSpacing = DependencyService.Get<IUtilities>().getStatusBarHeight();
            gb.permissions = DependencyService.Get<IPermissions>();

            Debug.WriteLine("Screen W: " + gb.screenWidth + " Height: " + gb.screenHeight);
            gb.initFonts();
            gb.initColors();
            gb.calculateMeasurements();
            gb.InitLocalStorage();
            //ImageSources.initImageSources();
            //APIServices.initAPIServices();
            Styles.initStyles();

            //api services
            APIServices.initAPIServices();

            Debug.WriteLine("Core Init Application");
            //Debug.WriteLine(DependencyService.Get<IFileManager>().getFullPath());


            // init when user is logged in
            if (isLoggedin() != 1) return;

        }

        public static void initStaticVariables()
        {

        }

        #region utilities

        public static void dumpObject(object obj)
        {
            if (obj == null) { Debug.WriteLine("NULL Object"); return; }
            Debug.WriteLine("Type: " + obj.GetType().FullName);
            foreach (var pair in JObject.Parse(JsonConvert.SerializeObject(obj)))
                Debug.WriteLine("\t{0}: {1}", pair.Key, pair.Value);
            Debug.WriteLine("-----------------------");
        }

        public static TapGestureRecognizer addClick(View view, EventHandler handler)
        {
            var tap = new TapGestureRecognizer();
            tap.Tapped += handler;
            view.GestureRecognizers.Add(tap);
            return tap;
        }
        public static TapGestureRecognizer addClick(View view, Action handler)
        {
            var tap = new TapGestureRecognizer();
            tap.Command = new Command(handler);
            view.GestureRecognizers.Add(tap);
            return tap;
        }

        public static async void fadeOnTap(View v)
        {
            await v.FadeTo(0.5, 150).ContinueWith(async (obj) => { await v.FadeTo(1, 150); });
        }

        public static async void scaleOnTap(View v)
        {
            await v.ScaleTo(0.9, 150).ContinueWith(async (obj) => { await v.ScaleTo(1, 150); });
        }

        public static void addAtivityIndicator(RelativeLayout _layout)
        {
            _layout.InputTransparent = false;
            _layout.Children.Add(aIndicator,
                                 Constraint.RelativeToParent((p) => { return gb.screenWidth / 2 - gb.elementRowH / 2; }),
                                 Constraint.RelativeToParent((p) => { return gb.screenHeight / 2 - gb.elementRowH / 2; }),
                                 Constraint.RelativeToParent((p) => { return gb.elementRowH; }),
                                 Constraint.RelativeToParent((p) => { return gb.elementRowH; }));
        }

        //public static async Task overlayVisible(bool visible)
        //{
        //    RelativeLayout _overlay = null;

        //    while (Application.Current.MainPage == null || ((NavigationPage)Application.Current.MainPage).CurrentPage == null)
        //        await Task.Delay(50);

        //    var page = ((NavigationPage)Application.Current.MainPage).CurrentPage;
        //    if (page is IOverlay)
        //    {
        //        _overlay = ((IOverlay)page).getOverlay();
        //    }
        //    else return;

        //    if (_overlay == null) return;
        //    if (visible)
        //    {
        //        _overlay.Opacity = 0;
        //        _overlay.IsVisible = true;
        //        await _overlay.FadeTo(1, 100);
        //    }
        //    else
        //    {
        //        await _overlay.FadeTo(0, 100);
        //        _overlay.IsVisible = false;
        //    }
        //    return;
        //}

        //public static async Task showPopup(string message)
        //{
        //    await showPopup(null, message);
        //    return;
        //}

        //public static async Task showPopup(string title, string message)
        //{
        //    RelativeLayout _overlay = null;
        //    while (Application.Current.MainPage == null || ((NavigationPage)Application.Current.MainPage).CurrentPage == null)
        //        await Task.Delay(20);

        //    var page = ((NavigationPage)Application.Current.MainPage).CurrentPage;
        //    if (page is IOverlay)
        //    {
        //        _overlay = ((IOverlay)page).getPupupOverlay();
        //    }
        //    else return;
        //    if (_overlay == null) return;


        //    var task = new TaskCompletionSource<bool>();
        //    if (lastPoup == null)
        //    {
        //        lastPoup = new TaskCompletionSource<bool>();
        //        lastPoup.SetResult(true);
        //    }

        //    lastPoup.Task.ContinueWith(
        //        (arg) => Xamarin.Forms.Device.BeginInvokeOnMainThread(() => showPopupTask(task, _overlay, message, title))
        //    );

        //    lastPoup = task;

        //    await task.Task;
        //    return;
        //}


        static async void showPopupTask(TaskCompletionSource<bool> task, RelativeLayout _overlay, string message, string title = null)
        {

        }

        public static async Task hideMenu(bool hide)
        {
            //Pages.MainPage mainpage = null;
            //var page = ((NavigationPage)Application.Current.MainPage).CurrentPage;
            //if (page is Pages.MainPage)
            //{
            //    mainpage = ((Pages.MainPage)page);
            //}
            //else return;

            //if (page == null) return;

            //await mainpage.hideMenu(hide);

            //return;
        }

        public static int isLoggedin()
        {
            var session = Storage.getSession();
            if (session == null || String.IsNullOrEmpty(session.Id))
            {
                Debug.WriteLine("Not logged in");
                return -1;
            }
            return 1;
        }

        public static int isSessionOk(Session session)
        {
            if (session == null || String.IsNullOrEmpty(session.Id))
            {
                Debug.WriteLine("Session is not set");
                return -1;
            }
            return 1;
        }

        public static async Task logOut()
        {
            //logout api

            //check any pending process

            //erase session
            Storage.deleteSession();
            //go to login
            var tempPage = ((NavigationPage)App.Current.MainPage).CurrentPage;
            //await ((NavigationPage)App.Current.MainPage).Navigation.PushAsync(new Views.Pages.Login.LoginPage.LoginPage());
            ((NavigationPage)App.Current.MainPage).Navigation.RemovePage(tempPage);
        }

        #endregion



        #region check inputs

        public static bool checkPlainEntry(string text)
        {
            if (string.IsNullOrEmpty(text) || text.Length < 1)
            {
                return false;
            }
            return true;
        }

        public static bool checkAlphaNumEntry(string text)
        {
            if (string.IsNullOrEmpty(text) || !Regex.Match(text, @"^[a-zA-Z0-9][a-zA-Z0-9]*$").Success)
            {
                return false;
            }
            return true;
        }

        public static bool checkTelEntry(string tel)
        {
            if (string.IsNullOrEmpty(tel) || tel.Length < 13)
            {
                return false;
            }
            return true;
        }

        public static bool checkEmailEntry(string email)
        {
            if (string.IsNullOrEmpty(email) || !Regex.Match(email, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$").Success)
            {
                return false;
            }
            return true;
        }

        public static bool checkPasswordEntry(string pass)
        {
            if (string.IsNullOrEmpty(pass) || pass.Length <= 3)
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
