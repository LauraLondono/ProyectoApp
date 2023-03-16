// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public static class gb
    {
        #region Session
        public static string sessionFileName = "sessionData";
        public static string accountsDataFileName = "accountsData";
        public static string storedPlayeridFileName = "storedPlayerid";
        public static string storedReceiptsFileName = "storedReceiptsData";
        public static string playerId = "";
        #endregion

        #region Storage
        public static string JsonFileName = "MessageLocal";
        #endregion


        #region terms
        public static string terms = "http://";
        public static string privacy = "http://";
        #endregion

        #region httpClient
        public static int timeOutSec = 60;
        #endregion
        public static IPermissions permissions;

        #region Theme Colors
        public static Color
        mainColor,
        mainColorBlack,
        secondaryColor,
        menuIconColor,
        titleColor,
        entryTextColor,
        entryPlaceHolderColor,
        entryBg,
        entryErrorColor,
        mainButtonTextColor,
        normalButtonTextColor,
        lightBg,
        navBarColor,
        textColorEntries,
        buttonsBg,
        textsColor,
        textsColorNoFoud;

        #endregion

        #region FontSize
        //fontSize base on deviceHeight = 667
        public static int
        normalFontSize = 14,
        smallFontSize = 10,
        entryFontSize = 14,
        titleFontSize = 18,
        mainButtonFontSize = 13,
        smallButtonFontSize = 11;
        #endregion

        #region fontFamily
        public static string
        regularFont,
        blackFont,
        semiBoldFont,
        boldFont,
        extraBoldFont,
        mediumFont;
        #endregion

        #region ui global messurements
        public static double
        referenceDeviceH = 667,//iphone 6
        elementRowH = 48.0,
        entryRowH = 39,
        iconButtonH = 44,
        iconMapH = 34,
        labelButtonH = 32,
        footerH = 49.0,
        borderRadius = 14,
        shadowStrokeThickness = 10;
        #endregion

        #region device
        public static double
        deviceCarrierSpacing = 20,
        paddingL = 15,
        paddingR = 16,
        screenWidth,
        screenHeight;
        #endregion

        public static IFileManager LocalStorage;

        public static void initFonts()
        {

            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    regularFont = "";
                    blackFont = "";
                    semiBoldFont = "";
                    boldFont = "";
                    extraBoldFont = "";
                    mediumFont = "";
                    break;
                case Device.Android:
                    regularFont = "";
                    blackFont = "";
                    semiBoldFont = "";
                    boldFont = "";
                    extraBoldFont = "";
                    mediumFont = "";
                    break;
            }
        }

        public static void initColors()
        {
            //mainColor = Color.FromHex("#6F86D6");
            secondaryColor = Color.FromHex("#48C6EF");
            menuIconColor = Color.FromHex("#00A9B0");
            entryTextColor = Color.FromHex("#6B6C67");
            entryPlaceHolderColor = Color.FromHex("#CDCDCD");
            entryBg = Color.FromHex("#f0f0f0");
            mainButtonTextColor = Color.FromHex("#E3E0DD");
            lightBg = Color.FromHex("#F6F5F9");
            entryErrorColor = Color.FromHex("#F73F52");

            navBarColor = Color.LightGray;
            textColorEntries = Color.Black;
            titleColor = Color.White;
            buttonsBg = Color.Gray.MultiplyAlpha(0.5);
            mainColor = Color.FromHex("#AFCDFF");
            mainColorBlack = Color.FromHex("#8FB7FF");
            textsColor = Color.FromHex("#042551");
            textsColorNoFoud = Color.FromHex("#ABABAB");
        }

        public static void InitLocalStorage()
        {
            LocalStorage = DependencyService.Get<IFileManager>();
        }

        public static void calculateMeasurements()
        {
            elementRowH = screenHeight * 0.0720;
            entryRowH = screenHeight * 0.0585;
            iconButtonH = screenHeight * 0.0660;
            iconMapH = Math.Round(screenHeight * 0.0510);
            borderRadius = LayoutTools._w(14);
            adjustFontSizeToDevice(screenHeight);
        }

        public static double CalculateHeighViewControl(View view)
        {
            return view.Height + view.Y;
        }

        public static void adjustFontSizeToDevice(double currentDeviceHeight)
        {
            normalFontSize = converFontSize(14, currentDeviceHeight);
            entryFontSize = converFontSize(14, currentDeviceHeight);
            titleFontSize = converFontSize(18, currentDeviceHeight);
            mainButtonFontSize = converFontSize(13, currentDeviceHeight);
            smallButtonFontSize = converFontSize(11, currentDeviceHeight);
            smallFontSize = converFontSize(9, currentDeviceHeight);
        }

        public static int converFontSize(int font, double currentDeviceHeight)
        {
            return (int)Math.Round((font / referenceDeviceH) * currentDeviceHeight);
        }
    }
}
