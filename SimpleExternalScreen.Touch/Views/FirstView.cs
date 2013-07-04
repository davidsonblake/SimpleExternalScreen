using System.Drawing;
using System.Linq;
using Cirrious.CrossCore;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.Binding.BindingContext;
using Cirrious.MvvmCross.Binding.Touch.Views;
using Cirrious.MvvmCross.Touch.Views;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using SimpleExternalScreen.Core.ViewModels;

namespace SimpleExternalScreen.Touch.Views
{
    public sealed class FullScreenView : MvxView
    {
        public UILabel Label;
        public FullScreenView(RectangleF frame)
            :base(frame)
        {
            BackgroundColor = UIColor.White;

            Label = new UILabel
            {
                AdjustsFontSizeToFitWidth = true,
                Lines = 1,
                LineBreakMode = UILineBreakMode.TailTruncation,
                BackgroundColor = UIColor.Clear,
                PreferredMaxLayoutWidth = Frame.Width - 10,
                Frame = new RectangleF(0, 0, Frame.Width - 10, Frame.Height / 7),
                TextColor = UIColor.Black,
                TextAlignment = UITextAlignment.Center,
                AutoresizingMask = UIViewAutoresizing.All
            };

            this.DelayBind(() =>
                {
                    var set = this.CreateBindingSet<FullScreenView, FullScreenViewModel>();
                    set.Bind(Label).To(vm => vm.DisplayText);
                    set.Apply();
                } );
        }
    }

    [Register("FirstView")]
    public class FirstView : MvxViewController
    {
        protected FirstViewModel FirstViewModel
        {
            get { return ViewModel as FirstViewModel; }
        }

        private FullScreenView _fullScreenView;

        public override void ViewDidLoad()
        {
            View = new UIView(){ BackgroundColor = UIColor.White};
            
            if (!CheckForExternalDisplay())
            {
                _fullScreenView = new FullScreenView(new RectangleF(0, 0, 0, 0));
            } 
            
            base.ViewDidLoad();
            var label = new UILabel(new RectangleF(10, 10, 300, 40));
            Add(label);
            var textField = new UITextField(new RectangleF(10, 50, 300, 40));
            Add(textField);

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(label).To(vm => vm.DisplayText);
            set.Bind(textField).To(vm => vm.DisplayText);
            set.Bind(_fullScreenView).For(add => add.DataContext).To(vm => vm.FullScreen);
            set.Apply();
        }

        public bool CheckForExternalDisplay()
        {
            if (UIScreen.Screens.Length > 1)
            {
                Mvx.Trace(MvxTraceLevel.Diagnostic, "Multiple screens found");
                var externalScreen = UIScreen.Screens[1];
                var rect = new RectangleF(new PointF(0, 0), externalScreen.AvailableModes.Max(m => m).Size);
                var window = new UIWindow(rect)
                {
                    Screen = UIScreen.Screens[1],
                    ClipsToBounds = true,
                };
                _fullScreenView = new FullScreenView(rect);
                window.AddSubview(_fullScreenView);
                window.Hidden = false;

                return true;
            }

            return false;

        }
    }
}