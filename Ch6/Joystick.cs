using SenseHat.Helpers;
using System;
using Windows.Devices.I2c;
using Windows.UI.Xaml;

namespace SenseHatIO.SenseHatJoystick
{
    public class Joystick
    {
        public event EventHandler<JoystickEventArgs> ButtonPressed = delegate { };

        private const int commandId = 0xF2;
        private const int msUpdateInterval = 12;

        private I2cDevice device;

        private DispatcherTimer joystickTimer;

        private JoystickButton previousButton = JoystickButton.None;
        private JoystickButtonState previousButtonState = JoystickButtonState.None;

        public Joystick(I2cDevice device)
        {
            Check.IsNull(device);

            this.device = device;

            ConfigureTimer();
        }

        private void ConfigureTimer()
        {
            joystickTimer = new DispatcherTimer();

            joystickTimer.Interval = TimeSpan.FromMilliseconds(msUpdateInterval);
            joystickTimer.Tick += JoystickTimer_Tick;

            joystickTimer.Start();
        }

        private void JoystickTimer_Tick(object sender, object e)
        {
            var rawInput = RegisterHelper.ReadByte(device, commandId);

            var buttonInfo = GetButtonInfo(rawInput);

            ButtonPressed(this, buttonInfo);
        }

        private JoystickEventArgs GetButtonInfo(byte rawInput)
        {
            var currentJoystickButton = JoystickHelper.GetJoystickButton(rawInput);
            var currentJoystickButtonState = JoystickHelper.GetJoystickButtonState(currentJoystickButton,
                previousButton, previousButtonState);

            // Store the button value and its state
            previousButton = currentJoystickButton;
            previousButtonState = currentJoystickButtonState;

            return new JoystickEventArgs(currentJoystickButton, currentJoystickButtonState);
        }
    }
}
