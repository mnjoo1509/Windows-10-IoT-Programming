using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using Windows.UI;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace HeadedAppDesign
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Style coloredButtonStyle = new Style(typeof(Button));
        private const string pointerOverVisualStateName = "PointerOver";
        private const string normalVisualStateName = "Normal";

        public MainPage()
        {
            this.InitializeComponent();

            SetStylePropertySetters();

            GoToStateButton.Style = coloredButtonStyle;
        }

        private void SetStylePropertySetters()
        {
            coloredButtonStyle.Setters.Add(new Setter(BorderThicknessProperty, 0.5));
            coloredButtonStyle.Setters.Add(new Setter(BorderBrushProperty, Colors.Black));
            coloredButtonStyle.Setters.Add(new Setter(FontSizeProperty, 20));
            coloredButtonStyle.Setters.Add(new Setter(MarginProperty, new Thickness(10, 10, 0, 0)));
            coloredButtonStyle.Setters.Add(new Setter(BackgroundProperty, GenerateGradient()));
        }

        private LinearGradientBrush GenerateGradient()
        {
            var gradientStopCollection = new GradientStopCollection();

            gradientStopCollection.Add(new GradientStop()
            { Color = Colors.Yellow, Offset = 0 });

            gradientStopCollection.Add(new GradientStop()
            {
                Color = Colors.Orange,
                Offset = 0.5
            });

            gradientStopCollection.Add(new GradientStop()
            {
                Color = Colors.Red,
                Offset = 1.0
            });

            return new LinearGradientBrush(gradientStopCollection, 0.0);
        }
        private void GoToStateButton_Click(object sender, RoutedEventArgs e)
        {
            SwapButtonVisualState(IoTButton);
            SwapButtonVisualState(Windows10IoTCoreButton);
        }

        private void SwapButtonVisualState(Button button)
        {
            string newVisualState = pointerOverVisualStateName;

            if (button.Tag != null)
            {
                if (button.Tag.ToString().Contains(pointerOverVisualStateName))
                {
                    newVisualState = normalVisualStateName;
                }
                else
                {
                    newVisualState = pointerOverVisualStateName;
                }
            }

            VisualStateManager.GoToState(button, newVisualState, false);

            button.Tag = newVisualState;
        }
    }
}
