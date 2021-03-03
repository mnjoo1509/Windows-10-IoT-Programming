using SenseHat.Helpers;
using SenseHatIO.SenseHatJoystick;
using SenseHatIO.SenseHatLedArray;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using SenseHatIO.SenseHatLedArray;

namespace SenseHatIO
{
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public Color DotColor
        {
            get { return dotColor; }
            set
            {
                dotColor = value;
                OnPropertyChanged();
            }
        }

        private Joystick joystick;
        private LedArray ledArray;

        private int x = 0;
        private int y = 0;

        private Color dotColor = Colors.Red;

        private double rectangleWidth;
        private double rectangleHeight;

        private TranslateTransform rectangleTransform = new TranslateTransform();

        public MainPage()
        {
            InitializeComponent();
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            await Initialize();

            AdjustRectangleToScreenSize();
        }

        private async Task Initialize()
        {
            const byte address = 0x46;
            var device = await I2cHelper.GetI2cDevice(address);

            if (device != null)
            {
                joystick = new Joystick(device);
                joystick.ButtonPressed += Joystick_ButtonPressed;

                ledArray = new LedArray(device);
                BeginRgbTest();
                //UpdateDevice();
            }
            else
            {
                MainPivot.IsEnabled = false;
            }
        }

        private void AdjustRectangleToScreenSize()
        {
            const int headerHeight = 50;

            rectangleWidth = Window.Current.Bounds.Width / LedArray.Length;
            rectangleHeight = (Window.Current.Bounds.Height - headerHeight) / LedArray.Length;
        }

        private void Joystick_ButtonPressed(object sender, JoystickEventArgs e)
        {
            SenseHatJoystickControl.UpdateView(e.Button, e.State);

            if (e.State == JoystickButtonState.Pressed)
            {
                UpdateDotPosition(e.Button);

                rectangleTransform.X = x * rectangleWidth;
                rectangleTransform.Y = y * rectangleHeight;
            }
        }

        private void BeginRgbTest()
        {
            const int msDelayTime = 1000;

            while (true)
            {
                ledArray.RgbTest(msDelayTime);
            }
        }

        private void UpdateDotPosition(JoystickButton button)
        {
            switch (button)
            {
                case JoystickButton.Up:
                    y -= 1;
                    break;

                case JoystickButton.Down:
                    y += 1;
                    break;

                case JoystickButton.Left:
                    x -= 1;
                    break;

                case JoystickButton.Right:
                    x += 1;
                    break;

                case JoystickButton.Enter:
                    InvertDotColor();
                    break;
            }

            UpdateDevice();
        }

        private void UpdateDevice()
        {
            x = CorrectLedCoordinate(x);
            y = CorrectLedCoordinate(y);

            ledArray.SetPixel(x, y, dotColor);
        }

        private static int CorrectLedCoordinate(int inputCoordinate)
        {
            inputCoordinate = Math.Min(inputCoordinate, LedArray.Length - 1);
            inputCoordinate = Math.Max(inputCoordinate, 0);

            return inputCoordinate;
        }

        private void InvertDotColor()
        {
            //dotColor = dotColor == Colors.Red ? Colors.Green : Colors.Red;
            DotColor = dotColor == Colors.Red ? Colors.Green : Colors.Red;
        }

        private void Rectangle_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {
            if (!e.IsInertial)
            {
                rectangleTransform.X += e.Delta.Translation.X;
                rectangleTransform.Y += e.Delta.Translation.Y;

                UpdateDotPosition();
            }

            e.Handled = true;
        }

        private void UpdateDotPosition()
        {
            x = Convert.ToInt32(rectangleTransform.X / rectangleWidth);
            y = Convert.ToInt32(rectangleTransform.Y / rectangleHeight);

            UpdateDevice();
        }

        private void Rectangle_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            InvertDotColor();

            UpdateDevice();
        }
    }
}
