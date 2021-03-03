using System;

namespace SenseHatIO.SenseHatJoystick
{
    public class JoystickEventArgs : EventArgs
    {
        public JoystickButton Button { get; private set; }

        public JoystickButtonState State { get; private set; }

        public JoystickEventArgs(JoystickButton button, JoystickButtonState state)
        {
            Button = button;
            State = state;
        }
    }

    public enum JoystickButton : byte
    {
        None = 0, Down = 1, Right = 2, Up = 4, Enter = 8, Left = 16
    }

    public enum JoystickButtonState : byte
    {
        None = 0, Pressed, Holding, Released
    }
}
