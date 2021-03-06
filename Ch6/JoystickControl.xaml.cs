using SenseHatIO.SenseHatJoystick;
using System.Collections.Generic;
using Windows.UI;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Shapes;

namespace SenseHatIO.Controls
{
    public sealed partial class JoystickControl : UserControl
    {
        private Dictionary<JoystickButton, Rectangle> buttonPads;

        private SolidColorBrush inactiveColorBrush = new SolidColorBrush(Colors.LightGray);
        private SolidColorBrush activeColorBrush = new SolidColorBrush(Colors.GreenYellow);

        public JoystickControl()
        {
            InitializeComponent();

            ConfigureButtonPadsDictionary();

            ClearAll();
        }

        public void UpdateView(JoystickButton button, JoystickButtonState buttonState)
        {
            ClearAll();

            if (button != JoystickButton.None)
            {
                var colorBrush = inactiveColorBrush;

                switch (buttonState)
                {
                    case JoystickButtonState.Pressed:
                    case JoystickButtonState.Holding:
                        colorBrush = activeColorBrush;
                        break;
                }

                buttonPads[button].Fill = colorBrush;
            }
        }

        private void ConfigureButtonPadsDictionary()
        {
            buttonPads = new Dictionary<JoystickButton, Rectangle>();

            buttonPads.Add(JoystickButton.Up, Up);
            buttonPads.Add(JoystickButton.Down, Down);
            buttonPads.Add(JoystickButton.Left, Left);
            buttonPads.Add(JoystickButton.Right, Right);
            buttonPads.Add(JoystickButton.Enter, Enter);            
        }

        private void ClearAll()
        {
            foreach (var buttonPad in buttonPads)
            {
                buttonPad.Value.Fill = inactiveColorBrush;
            }
        }        
    }
}
