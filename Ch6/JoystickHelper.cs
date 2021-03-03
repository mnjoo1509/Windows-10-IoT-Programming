using System;
using System.Collections;

namespace SenseHatIO.SenseHatJoystick
{
    public static class JoystickHelper
    {
        public static JoystickButton GetJoystickButton(byte buttonInput)
        {
            var bitArray = new BitArray(new byte[] { buttonInput });

            var joystickButton = JoystickButton.None;

            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i])
                {
                    var numValue = Convert.ToByte(Math.Pow(2, i));

                    if (Enum.IsDefined(typeof(JoystickButton), numValue))
                    {
                        joystickButton = (JoystickButton)numValue;
                    }

                    break;
                }
            }

            return joystickButton;
        }

        public static JoystickButtonState GetJoystickButtonState(JoystickButton currentButton, JoystickButton previousButton,
            JoystickButtonState previousButtonState = JoystickButtonState.None)
        {
            var buttonState = previousButtonState;

            if (currentButton != previousButton)
            {
                switch (previousButtonState)
                {
                    case JoystickButtonState.None:
                        buttonState = JoystickButtonState.Pressed;
                        break;

                    case JoystickButtonState.Holding:
                        buttonState = JoystickButtonState.Released;
                        break;
                }
            }
            else
            {
                if (currentButton == JoystickButton.None)
                {
                    buttonState = JoystickButtonState.None;
                }
                else
                {
                    buttonState = JoystickButtonState.Holding;
                }
            }

            return buttonState;
        }
    }
}
