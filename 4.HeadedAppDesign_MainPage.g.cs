#pragma checksum "C:\Users\user\OneDrive\바탕 화면\새 폴더\programing\Chapter04\HeadedAppDesign\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1900940610D4CEE4B9AD35B3D1F136BE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace HeadedAppDesign
{
    partial class MainPage : 
        global::Windows.UI.Xaml.Controls.Page, 
        global::Windows.UI.Xaml.Markup.IComponentConnector,
        global::Windows.UI.Xaml.Markup.IComponentConnector2
    {
        /// <summary>
        /// Connect()
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 2: // MainPage.xaml line 105
                {
                    this.GoToStateButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                    ((global::Windows.UI.Xaml.Controls.Button)this.GoToStateButton).Click += this.GoToStateButton_Click;
                }
                break;
            case 3: // MainPage.xaml line 114
                {
                    this.IoTButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            case 4: // MainPage.xaml line 118
                {
                    this.Windows10IoTCoreButton = (global::Windows.UI.Xaml.Controls.Button)(target);
                }
                break;
            default:
                break;
            }
            this._contentLoaded = true;
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

        /// <summary>
        /// GetBindingConnector(int connectionId, object target)
        /// </summary>
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 10.0.17.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public global::Windows.UI.Xaml.Markup.IComponentConnector GetBindingConnector(int connectionId, object target)
        {
            global::Windows.UI.Xaml.Markup.IComponentConnector returnValue = null;
            return returnValue;
        }
    }
}
