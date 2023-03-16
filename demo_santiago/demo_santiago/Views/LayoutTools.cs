// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Xamarin.Forms;

namespace demo_santiago
{
    public static class LayoutTools
    {
        /// <summary>
        /// The width/height reference that is used for layout the initial mesuremets.
        /// </summary>
        public static double
        widthRef = 375,
        HeightRef = 667;
        /// <summary>
        /// convert horiozontal absolut value to a relative value
        /// </summary>
        public static double _w(double value)
        {
            return (value / widthRef) * gb.screenWidth;
        }
        /// <summary>
        /// convert vertical absolut value to a relative value
        /// </summary>
        public static double _h(double value)
        {
            return (value / HeightRef) * gb.screenHeight;
        }

        public static double _wr(double value)
        {
            return Math.Round(_w(value));
        }

        public static double _hr(double value)
        {
            return Math.Round(_h(value));
        }
        /// <summary>
        /// Adds the view to a RelativeLayout, using RelativeToParent constraint but independet coordinates.
        /// </summary>
        public static void addChild(RelativeLayout _layout, View view, double x, double y, double w, double h)
        {
            _layout.Children.Add(view,
                                 Constraint.RelativeToParent((p) => { return view.Width == -1 ? x : view.X; }),
                                 Constraint.RelativeToParent((p) => { return view.Height == -1 ? y : view.Y; }),
                                 Constraint.RelativeToParent((p) => { return view.Width == -1 ? w : view.Width; }),
                                 Constraint.RelativeToParent((p) => { return view.Height == -1 ? h : view.Height; })
            );
        }

        public static void addChild(RelativeLayout _layout, View view, double x, double y, double w)
        {
            _layout.Children.Add(view,
                                 Constraint.RelativeToParent((p) => { return view.Width == -1 ? x : view.X; }),
                                 Constraint.RelativeToParent((p) => { return view.Height == -1 ? y : view.Y; }),
                                 Constraint.RelativeToParent((p) => { return view.Width == -1 ? w : view.Width; })
            );
        }

        /// <summary>
        /// Adds the view to a RelativeLayout, using RelativeToParent constraint with optional scaling factor.
        /// </summary>
        public static void addChildFitParent(RelativeLayout _layout, View view, double ix = 0, double iy = 0, double dw = 1, double dh = 1)
        {
            _layout.Children.Add(view,
                                 Constraint.RelativeToParent((p) => { return 0 + ix; }),
                                 Constraint.RelativeToParent((p) => { return 0 + iy; }),
                                 Constraint.RelativeToParent((p) => { return p.Width * dw; }),
                                 Constraint.RelativeToParent((p) => { return p.Height * dh; })
            );
        }
        /// <summary>
        /// Creates layout intances on the page and put everything in the right order.
        /// </summary>
        /// <param name="page">Page target</param>
        /// <param name="_container">Main warp container</param>
        /// <param name="_layout">Layout with the content</param>
        /// <param name="_popupOverlay">Pop up layer</param>
        /// <param name="_overlay">Activity indicator layer</param>


        public static void initPageStructure(ContentPage page, out RelativeLayout _container, out RelativeLayout _layout, out RelativeLayout _popupOverlay, out RelativeLayout _overlay)
        {

            NavigationPage.SetHasNavigationBar(page, false);
            _container = new RelativeLayout()
            {
                HeightRequest = gb.screenHeight,
                WidthRequest = gb.screenWidth,
                IsClippedToBounds = true,
            };
            _layout = new RelativeLayout()
            {
                HeightRequest = gb.screenHeight,
                WidthRequest = gb.screenWidth,
            };

            _popupOverlay = new RelativeLayout()
            {
                HeightRequest = gb.screenHeight,
                WidthRequest = gb.screenWidth,
                IsVisible = false,
                InputTransparent = false,
                BackgroundColor = gb.mainColor.MultiplyAlpha(0.7),
            };
            _overlay = new RelativeLayout()
            {
                HeightRequest = gb.screenHeight,
                WidthRequest = gb.screenWidth,
                IsVisible = false,
                InputTransparent = false,
            };
            page.Content = new ScrollView
            {
                Content = _container,
                BackgroundColor = Color.DarkGray,
                HeightRequest = gb.screenHeight,
            };

            Core.addAtivityIndicator(_overlay);

            _container.Children.Add(_layout,
                                 Constraint.RelativeToParent((p) => { return 0; }),
                                 Constraint.RelativeToParent((p) => { return 0; }),
                                 Constraint.RelativeToParent((p) => { return p.Width; }),
                                 Constraint.RelativeToParent((p) => { return p.Height; })
                                                            );
            _container.Children.Add(_popupOverlay,
                 Constraint.RelativeToParent((p) => { return 0; }),
                 Constraint.RelativeToParent((p) => { return 0; }),
                 Constraint.RelativeToParent((p) => { return p.Width; }),
                 Constraint.RelativeToParent((p) => { return p.Height; })
                                    );
            _container.Children.Add(_overlay,
                     Constraint.RelativeToParent((p) => { return 0; }),
                     Constraint.RelativeToParent((p) => { return 0; }),
                     Constraint.RelativeToParent((p) => { return p.Width; }),
                     Constraint.RelativeToParent((p) => { return p.Height; })
                                                );

        }

        //public static void initPageStructure(ContentPage page, out RelativeLayout _container, out RelativeLayout _layout, out RelativeLayout _popupOverlay, out RelativeLayout _overlay, out Pages.MainPage.MenuView _menuOverlay)
        //{

        //    NavigationPage.SetHasNavigationBar(page, false);
        //    _container = new RelativeLayout()
        //    {
        //        HeightRequest = gb.screenHeight,
        //        WidthRequest = gb.screenWidth,
        //        IsClippedToBounds = true,
        //    };
        //    _layout = new RelativeLayout()
        //    {
        //        HeightRequest = gb.screenHeight,
        //        WidthRequest = gb.screenWidth,
        //    };

        //    _menuOverlay = new Pages.MainPage.MenuView()
        //    {
        //        HeightRequest = gb.screenHeight,
        //        WidthRequest = gb.screenWidth,
        //        IsVisible = true,
        //        InputTransparent = false,
        //    };

        //    _popupOverlay = new RelativeLayout()
        //    {
        //        HeightRequest = gb.screenHeight,
        //        WidthRequest = gb.screenWidth,
        //        IsVisible = false,
        //        InputTransparent = false,
        //        BackgroundColor = gb.mainColor.MultiplyAlpha(0.7),
        //    };
        //    _overlay = new RelativeLayout()
        //    {
        //        HeightRequest = gb.screenHeight,
        //        WidthRequest = gb.screenWidth,
        //        IsVisible = false,
        //        InputTransparent = false,
        //    };
        //    page.Content = new ScrollView
        //    {
        //        Content = _container,
        //        BackgroundColor = Color.White,
        //        HeightRequest = gb.screenHeight,
        //    };

        //    Core.Core.addAtivityIndicator(_overlay);

        //    _container.Children.Add(_layout,
        //                         Constraint.RelativeToParent((p) => { return 0; }),
        //                         Constraint.RelativeToParent((p) => { return 0; }),
        //                         Constraint.RelativeToParent((p) => { return p.Width; }),
        //                         Constraint.RelativeToParent((p) => { return p.Height; })
        //                                                    );
        //    _container.Children.Add(_menuOverlay,
        //             Constraint.RelativeToParent((p) => { return 0; }),
        //             Constraint.RelativeToParent((p) => { return 0; }),
        //             Constraint.RelativeToParent((p) => { return p.Width; }),
        //             Constraint.RelativeToParent((p) => { return p.Height; })
        //                                        );
        //    _container.Children.Add(_popupOverlay,
        //         Constraint.RelativeToParent((p) => { return 0; }),
        //         Constraint.RelativeToParent((p) => { return 0; }),
        //         Constraint.RelativeToParent((p) => { return p.Width; }),
        //         Constraint.RelativeToParent((p) => { return p.Height; })
        //                            );
        //    _container.Children.Add(_overlay,
        //             Constraint.RelativeToParent((p) => { return 0; }),
        //             Constraint.RelativeToParent((p) => { return 0; }),
        //             Constraint.RelativeToParent((p) => { return p.Width; }),
        //             Constraint.RelativeToParent((p) => { return p.Height; })
        //                                        );

        //}
    }
}
