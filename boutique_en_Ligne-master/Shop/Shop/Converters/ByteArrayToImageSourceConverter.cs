using System;
using System.Globalization;
using System.IO;
using Xamarin.Forms;

namespace Shop.Converters
{
    public class ByteArrayToImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is byte[] byteArray)
            {
                // Add debugging
                Console.WriteLine($"Converter: Byte array length = {byteArray.Length}");

                var imageSource = ImageSource.FromStream(() => new System.IO.MemoryStream(byteArray));

                // Add debugging
                Console.WriteLine($"Converter: Image source = {imageSource}");

                return imageSource;
            }

            // Add debugging
            Console.WriteLine($"Converter: Value is not a byte array");

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}
