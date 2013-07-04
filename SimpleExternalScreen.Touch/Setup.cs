using MonoTouch.UIKit;
using Cirrious.MvvmCross.Touch.Platform;

namespace SimpleExternalScreen.Touch
{
	public class Setup : MvxTouchSetup
	{
		public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
		{
		}

		protected override Cirrious.MvvmCross.ViewModels.IMvxApplication CreateApp ()
		{
			return new Core.App();
		}
	}
}