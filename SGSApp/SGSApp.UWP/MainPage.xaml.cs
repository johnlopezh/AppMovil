namespace SGSApp.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            LoadApplication(new SGSApp.App());
        }
    }
}