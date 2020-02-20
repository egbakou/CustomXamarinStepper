using System;
using Xamarin.Forms;

namespace CustomXamarinStepper.Controls
{
    // Current stepper accept only int value.
    public class StepperWithEntry : StackLayout
    {
        Button PlusBtn;
        Button MinusBtn;
        Entry Entry;

        public static readonly BindableProperty TextProperty =
          BindableProperty.Create(
             propertyName: "Text",
              returnType: typeof(int),
              declaringType: typeof(StepperWithEntry),
              defaultValue: 1,
              defaultBindingMode: BindingMode.TwoWay);

        public static readonly BindableProperty MinimumValueProperty =
            BindableProperty.Create("MinimumValue", typeof(int), typeof(StepperWithEntry), defaultValue: 0);

        public static readonly BindableProperty MaximumValueProperty =
            BindableProperty.Create("MaximumValue", typeof(int), typeof(StepperWithEntry), defaultValue: 10);

        public int Text
        {
            get { return (int)GetValue(TextProperty); }
            set { SetValue(TextProperty, value);  OnPropertyChanged(nameof(Text)); }
        }

        public int MinimumValue
        {
            get { return (int)GetValue(MinimumValueProperty); }
            set { SetValue(MinimumValueProperty, value); }
        }

        public int MaximumValue
        {
            get { return (int)GetValue(MaximumValueProperty); }
            set { SetValue(MaximumValueProperty, value); }
        }
        public StepperWithEntry()
        {
            PlusBtn = new Button { Text = "+", WidthRequest = 40, FontAttributes = FontAttributes.Bold, FontSize = 16 };
            MinusBtn = new Button { Text = "-", WidthRequest = 40, FontAttributes = FontAttributes.Bold, FontSize = 16 };
            switch (Device.RuntimePlatform)
            {

                case Device.UWP:
                case Device.Android:
                    {
                        PlusBtn.BackgroundColor = Color.Transparent;
                        MinusBtn.BackgroundColor = Color.Transparent;
                        break;
                    }
                case Device.iOS:
                    {
                        PlusBtn.BackgroundColor = Color.DarkGray;
                        MinusBtn.BackgroundColor = Color.DarkGray;
                        break;
                    }
            }

            Orientation = StackOrientation.Horizontal;
            PlusBtn.Clicked += PlusBtn_Clicked;
            MinusBtn.Clicked += MinusBtn_Clicked;
            Entry = new Entry
            {
                PlaceholderColor = Color.Gray,               
                Keyboard = Keyboard.Numeric,
                HorizontalTextAlignment = TextAlignment.Center,
                TextColor = Color.Green,
                WidthRequest = 50,
                BackgroundColor = Color.FromHex("#3FFF")
            };
            Entry.SetBinding(Entry.TextProperty, new Binding(nameof(Text), BindingMode.TwoWay, source: this));
            Entry.TextChanged += Entry_TextChanged;
            Entry.Unfocused += Entry_Unfocused;
            Children.Add(MinusBtn);
            Children.Add(Entry);
            Children.Add(PlusBtn);
        }

        private void Entry_Unfocused(object sender, FocusEventArgs e)
        {
           var text =  ((Entry)sender).Text;
            if (string.IsNullOrEmpty(text) || text.Contains(","))
                this.Text = 0;
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                try
                {
                    var entryValue = int.Parse(e.NewTextValue);
                    if (entryValue > MaximumValue)
                        this.Text = MaximumValue;
                    else
                        this.Text = entryValue;
                }
              catch(Exception)
                {
                    this.Text = 0;
                }               
            }
                
        }

        private void MinusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text > MinimumValue)
                Text--;
            else if (Text == MinimumValue)
                Text = MinimumValue;
        }

        private void PlusBtn_Clicked(object sender, EventArgs e)
        {
            if (Text < MaximumValue)
                Text++;
        }

    }
}
