namespace CustomXamarinStepper.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {

        public MainPageViewModel()
        {
            Title = "Stepper with Entry";
            MaximumValue = 10;
        }

        private int _maximumValue;
        public int MaximumValue
        {
            get { return _maximumValue; }
            set { SetProperty(ref _maximumValue, value); }
        }

        private int _quantity;
        public int Quantity
        {
            get { return _quantity; }
            set { SetProperty(ref _quantity, value); OnPropertyChanged(nameof(Quantity)); }
        }
    }
}
