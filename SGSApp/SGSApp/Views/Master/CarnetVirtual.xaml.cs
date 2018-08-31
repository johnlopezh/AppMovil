using System.Threading.Tasks;
using SGSApp.Helper;
using SGSApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.Net.Mobile.Forms;

namespace SGSApp.Views.Master
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CarnetVirtual : ContentPage
    {
        private readonly UserVM obj = new UserVM();
        private readonly ZXingBarcodeImageView barcode;

        private string numeroIdentificacion;

        public CarnetVirtual()
        {
            InitializeComponent();
            barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingBarcodeImageView"
            };
            barcode.BarcodeFormat = BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 300;
            barcode.BarcodeOptions.Height = 300;
            barcode.BarcodeOptions.Margin = 10;
            ConsultarInfoUsuario();
            barcode.BarcodeValue = numeroIdentificacion;

            //this.Content = barcode;
            qrcode.Children.Add(barcode);
        }
        public void OnClicked (object sender){
            button1.ScaleTo(2, 2000, Easing.BounceOut);

        }
        public async Task ConsultarInfoUsuario()
        {
            numeroIdentificacion = await obj.ConsultarInfoUsuario(GlobalVariables.Email);
        }
    }
}