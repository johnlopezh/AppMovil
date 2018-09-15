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
        public ImageSource ImageURL { get; set; }
        public CarnetVirtual()
        {
            InitializeComponent();
            nombreUsuario.Text = GlobalVariables.Usuario;
            barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingBarcodeImageView"
            };
            barcode.BarcodeFormat = BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 500;
            barcode.BarcodeOptions.Height = 500;
            barcode.BarcodeOptions.Margin = 2;
            ConsultarInfoUsuario();
            barcode.BarcodeValue = numeroIdentificacion;

            //this.Content = barcode;
            qrcode.Children.Add(barcode);
        }

        public async Task ConsultarInfoUsuario()
        {
            numeroIdentificacion = await obj.ConsultarInfoUsuario(GlobalVariables.Email);
        }
    }
}