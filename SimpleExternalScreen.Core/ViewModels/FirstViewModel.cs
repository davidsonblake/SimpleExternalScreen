using Cirrious.MvvmCross.ViewModels;

namespace SimpleExternalScreen.Core.ViewModels
{
    public class FullScreenViewModel
        :MvxViewModel
    {

        private string _displayText;
        public string DisplayText
        {
            get { return _displayText; }
            set
            {
                _displayText = value;
                RaisePropertyChanged(() => DisplayText);
            }
        }
        
    }

    public class FirstViewModel 
		: MvxViewModel
    {
        public FirstViewModel()
        {   
            _fullScreen = new FullScreenViewModel();
        }

        private FullScreenViewModel _fullScreen;
        public FullScreenViewModel FullScreen
        {
            get { return _fullScreen; }
            set
            {
                _fullScreen = value;
                RaisePropertyChanged(() => FullScreen);
            }
        }

        private string _displayText = "Hello";
        public string DisplayText
		{ 
			get { return _displayText; }
            set { _displayText = value; RaisePropertyChanged(() => DisplayText);
                FullScreen.DisplayText = DisplayText;
            }
		}
    }
}
