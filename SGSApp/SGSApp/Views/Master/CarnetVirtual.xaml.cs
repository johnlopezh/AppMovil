using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
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
            nombreUsuario.Text = GlobalVariables.Usuario;
            BindingContext = new CarnetViewModel();
            barcode = new ZXingBarcodeImageView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                AutomationId = "zxingBarcodeImageView"
            };
            barcode.BarcodeFormat = BarcodeFormat.QR_CODE;
            barcode.BarcodeOptions.Width = 250;
            barcode.BarcodeOptions.Height = 250;
            barcode.BarcodeOptions.Margin = 0;
            ConsultarInfoUsuario();
            barcode.BarcodeValue = numeroIdentificacion;
            //this.Content = barcode;
            qrcode.Children.Add(barcode);
        }
        private class CarnetViewModel : INotifyPropertyChanged
        {

            public CarnetViewModel()
            {
                ImageURL = ImageSource.FromStream(() => { return new MemoryStream(GlobalVariables.imagenUsuario); });

            }

            public ImageSource ImageURL { get; }
            #region INotifyPropertyChanged Implementation

            public event PropertyChangedEventHandler PropertyChanged;

            private void OnPropertyChanged([CallerMemberName] string propertyName = "")
            {
                if (PropertyChanged == null)
                    return;

                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            #endregion
        }

        public async Task ConsultarInfoUsuario()
        {
            numeroIdentificacion = await obj.ConsultarInfoUsuario(GlobalVariables.Email);
        }
    }
}