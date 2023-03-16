// Mateo 6:33: Mas buscad primeramente el reino de Dios y su justicia, y todas estas cosas os serán añadidas.
using System;
using Android.App;
using Android.Widget;
using Android.Views;
using Android.Graphics;
using Android.OS;
using Android.Util;

namespace demo_santiago.Droid
{
	public class AndroidBug5497WorkaroundForXamarinAndroid
	{
		private readonly View mChildOfContent;
		private int usableHeightPrevious;
		private FrameLayout.LayoutParams frameLayoutParams;
		static int deviceCarrierH;
		static bool hasSoftbuttonsbar;

		public static AndroidBug5497WorkaroundForXamarinAndroid assistActivity(Activity activity, IWindowManager windowManager)
		{
			deviceCarrierH = ((MainActivity)activity).getStatusBarHeight();
			return new AndroidBug5497WorkaroundForXamarinAndroid(activity, windowManager);
		}

		private AndroidBug5497WorkaroundForXamarinAndroid(Activity activity, IWindowManager windowManager)
		{

			var softButtonsHeight = getSoftbuttonsbarHeight(windowManager);

			var content = (FrameLayout)activity.FindViewById(Android.Resource.Id.Content);
			mChildOfContent = content.GetChildAt(0);
			var vto = mChildOfContent.ViewTreeObserver;

			vto.GlobalLayout += (sender, e) => possiblyResizeChildOfContent(softButtonsHeight);

			frameLayoutParams = (FrameLayout.LayoutParams)mChildOfContent.LayoutParameters;
		}

		private void possiblyResizeChildOfContent(int softButtonsHeight)
		{
			var usableHeightNow = computeUsableHeight();
			if (usableHeightNow != usableHeightPrevious)
			{
				var usableHeightSansKeyboard = mChildOfContent.RootView.Height - softButtonsHeight;
				var heightDifference = usableHeightSansKeyboard - usableHeightNow;
                Xamarin.Forms.NavigationPage pageCurrent = (Xamarin.Forms.NavigationPage)Xamarin.Forms.Application.Current.MainPage;
                if (heightDifference > (usableHeightSansKeyboard * 0.2 )
                    )
				{
					// keyboard probably just became visible
					frameLayoutParams.Height = usableHeightSansKeyboard - heightDifference + (softButtonsHeight / 2);
				}
				else
				{
					// keyboard probably just became hidden
					frameLayoutParams.Height = usableHeightSansKeyboard;
				}
				mChildOfContent.RequestLayout();
				usableHeightPrevious = usableHeightNow;
			}
		}

		private int computeUsableHeight()
		{
			var r = new Rect();
			mChildOfContent.GetWindowVisibleDisplayFrame(r);
			return (r.Bottom - r.Top + (hasSoftbuttonsbar?0:deviceCarrierH));
		}

		private int getSoftbuttonsbarHeight(IWindowManager windowManager)
		{
			if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
			{
				var metrics = new DisplayMetrics();
				windowManager.DefaultDisplay.GetMetrics(metrics);
				int usableHeight = metrics.HeightPixels;
				windowManager.DefaultDisplay.GetRealMetrics(metrics);
				int realHeight = metrics.HeightPixels;
				if (realHeight > usableHeight)
				{
					hasSoftbuttonsbar = true;
					return realHeight - usableHeight;
				}
				else
					return 0;
			}
			return 0;
		}
	}
}
