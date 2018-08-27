using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Sharepoint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public class CarouselNoticias : CarouselPage
    {
        public class MainPageCS : CarouselPage
        {
            public MainPageCS()
            {
                var padding = new Thickness(0, Device.OnPlatform(40, 40, 0), 0, 0);
                var redContentPage = new ContentPage
                {
                    Padding = padding,
                    Content = new StackLayout
                    {
                        Children =
                        {
                            new Label
                            {
                                Text = "Red",
                                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                                HorizontalOptions = LayoutOptions.Center
                            },
                            new BoxView
                            {
                                Color = Color.Red,
                                WidthRequest = 200,
                                HeightRequest = 200,
                                HorizontalOptions = LayoutOptions.Center,
                                VerticalOptions = LayoutOptions.CenterAndExpand
                            }
                        }
                    }
                };
                var greenContentPage = new ContentPage
                {
                    Padding = padding,
                    Content = new StackLayout()
                };
                var blueContentPage = new ContentPage
                {
                    Padding = padding,
                    Content = new StackLayout()
                };

                Children.Add(redContentPage);
                Children.Add(greenContentPage);
                Children.Add(blueContentPage);
            }
        }
    }
}