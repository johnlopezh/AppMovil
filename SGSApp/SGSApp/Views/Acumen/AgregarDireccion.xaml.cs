using System;
using System.Threading.Tasks;
using SGSApp.Controls;
using SGSApp.Helper;
using SGSApp.Models;
using SGSApp.Services;
using SGSApp.ViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Acumen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AgregarDireccion : ContentPage
    {
        private readonly string[] arr4 = new string[8];
        private readonly int codigoFamiliarDireccion;
        private readonly ComunVM objComun = new ComunVM();

        private readonly DireccionVM objDireccion = new DireccionVM();
        private Dominio ciudad;
        private DialogService dialogService;
        private Dominio[] dominioCiudades;
        private int inc;
        private int resultadoDireccion;

        public AgregarDireccion(int codigoFamiliar)
        {
            InitializeComponent();
            codigoFamiliarDireccion = codigoFamiliar;
            CargarCiudades();
            BindingContext = new ComunVM();
        }

        private ComunVM ViewModel => BindingContext as ComunVM;

        private async Task CargarCiudades()
        {
            dominioCiudades = await objComun.GetDominios("LugarResidencia");
            foreach (var item in dominioCiudades) armarCiudades(item.Descripcion);
            PickerCiudades.ItemsSource = arr4;
        }

        private void armarCiudades(string sol)
        {
            arr4[inc] = sol;
            inc++;
        }

        private async void BtnGuardarDireccion_Clicked(object sender, EventArgs e)
        {
            //if (string.IsNullOrEmpty(ciudad.Valor.ToString()))
            //{
            //    this.ShowMessage("Ingrese una ciudad",
            //         "Campo requerido",
            //         "Aceptar", async () => { });
            //    return;
            //}

            if (string.IsNullOrEmpty(EntDireccion.Text))
            {
                ShowMessage("Ingrese una dirección",
                    "Campo requerido",
                    "Aceptar", async () => { });
                return;
            }

            if (string.IsNullOrEmpty(EntBarrio.Text))
            {
                ShowMessage("Ingrese un barrio",
                    "Campo requerido",
                    "Aceptar", async () => { });
                return;
            }

            if (string.IsNullOrEmpty(EntTelefono.Text))
            {
                ShowMessage("Ingrese un teléfono",
                    "Campo requerido",
                    "Aceptar", async () => { });
                return;
            }

            if (string.IsNullOrEmpty(EntTelefono.Text))
            {
                ShowMessage("Ingrese una descripción",
                    "Campo requerido",
                    "Aceptar", async () => { });
                return;
            }

            var nuevaDireccion = new Direccion
            {
                IdGrupoFamiliar = codigoFamiliarDireccion,
                IdDepartamento = Convert.ToInt32(ciudad.Valor.Substring(0, 2)),
                IdCiudad = Convert.ToInt32(ciudad.Valor),
                Direccion1 = EntDireccion.Text,
                Barrio = EntBarrio.Text,
                TelefonoDireccion = EntTelefono.Text,
                DescripcionDireccion = entDescripcion.Text,
                DireccionPrincipal = false,
                UsuarioLog = GlobalVariables.Email,
                Sector = null,
                Latitud = null,
                Longitud = null,
                Zona = null,
                PuntoReferente = null,
                RestriccionBus = null,
                Estrato = null
            };

            resultadoDireccion = await objDireccion.RegistrarNuevaDireccion(nuevaDireccion);
            if (resultadoDireccion == 1) Navigation.PopAsync();
        }

        private void PickerCiudades_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radio = sender as CustomPicker;

            if (radio.SelectedItem == null) return;
            foreach (var item in dominioCiudades)
                if (item.Descripcion == radio.SelectedItem.ToString())
                    ciudad = item;
        }

        /// <summary>
        ///     Metodo que renderiza los mensajes de alerta de la aplicación.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="title"></param>
        /// <param name="buttonText"></param>
        /// <param name="afterHideCallback"></param>
        /// <returns></returns>
        public async Task ShowMessage(string message,
            string title,
            string buttonText,
            Action afterHideCallback)
        {
            await DisplayAlert(
                title,
                message,
                buttonText);

            afterHideCallback?.Invoke();
        }
    }
}