using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BinaryRepresentation
{
    public class ShortToBinaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            string result = string.Empty;

            if (value != null)
            {
                short inputValue;
                if (short.TryParse(value.ToString(), out inputValue))
                {
                    const int bitcount = 16;

                    result = System.Convert.ToString(inputValue, 2);
                    result = result.PadLeft(bitcount, '0');
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;

namespace BinaryRepresentation
{
    public class ShortToBinaryConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            string result = string.Empty;

            if (value != null)
            {
                short inputValue;
                if (short.TryParse(value.ToString(), out inputValue))
                {
                    const int bitcount = 16;

                    result = System.Convert.ToString(inputValue, 2);
                    result = result.PadLeft(bitcount, '0');
                }
            }

            return result;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
