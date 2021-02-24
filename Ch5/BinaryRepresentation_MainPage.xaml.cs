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

using System.ComponentModel;
using System.Runtime.CompilerServices;

// 빈 페이지 항목 템플릿에 대한 설명은 https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x412에 나와 있습니다.

namespace BinaryRepresentation
{
    /// <summary>
    /// 자체적으로 사용하거나 프레임 내에서 탐색할 수 있는 빈 페이지입니다.
    /// </summary>
    public sealed partial class MainPage : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public short BitToSet
        {
            get { return bitToSet; }
            set { SetBit(value); }
        }

        public short BitToClear
        {
            get { return bitToClear; }
            set { ClearBit(value); }
        }

        public short InputValue
        {
            get { return inputValue; }
            set { OutputValue = inputValue = value; }
        }

        public short OutputValue
        {
            get { return outputValue; }
            set 
            { 
                outputValue = value;
                OnPropertyChanged();
            }
        }

        private const int shortBitLength = 16;

        private short bitToSet;
        private short bitToClear;
        private short inputValue;
        private short outputValue;

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SetBit(short position)
        {
            if(position >= 0 && position <= shortBitLength)
            {
                OutputValue |= (short)(1 << position);
                bitToSet = position;
            }
        }

        private void ClearBit(short position)
        {
            if (position >= 0 && position <= shortBitLength)
            {
                OutputValue &= (short)~(1 << position);
                bitToClear = position;
            }
        }
    }
}
