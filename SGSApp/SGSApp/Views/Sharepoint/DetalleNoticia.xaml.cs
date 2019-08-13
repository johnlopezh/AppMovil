using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Sharepoint
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetalleNoticia : ContentPage
    {
        public DetalleNoticia(string TituloNoticia, string Descripcion, ImageSource ImageURL)
        {
            var htmlText = Descripcion;
            var browser = new WebView();
            var html = new HtmlWebViewSource
            {
                Html = htmlText
            };
            browser.Source = html;

            InitializeComponent();

            Descrip.Html = htmlText;
            //Descrip = Descripcion;
            Titulo.Text = TituloNoticia;
            Imagen.Source = ImageURL;
            //DescripcionNot.Html = Descripcion;
        }

        public string Html { get; set; }
    }
}