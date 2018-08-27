using Xamarin.Forms;

namespace SGSApp.Controls
{
    public class CustomPicker : Picker
    {
        public static readonly BindableProperty ImageProperty =
            BindableProperty.Create(nameof(Image), typeof(string), typeof(CustomPicker), string.Empty);

        public string Image
        {
            get => (string) GetValue(ImageProperty);
            set => SetValue(ImageProperty, value);
        }
    }
}