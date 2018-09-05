using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SGSApp.Controls;
using SGSApp.Helper;
using SGSApp.Models;
using SGSApp.ViewModel;
using SGSApp.Views.Home;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Acumen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardConsultaTransporte : ContentPage
    {
        private string[] arr4;

        private readonly TransporteVM obj = new TransporteVM();
        private Estudiante est;
        private int inc;
        private bool mensajeNoK = false;
        private List<TipoSolicitudTransporte> pp = new List<TipoSolicitudTransporte>();
        private TipoSolicitudTransporte slt;
        private TipoSolicitudTransporte[] TiposSol;

        public DashboardConsultaTransporte()
        {
            InitializeComponent();
            BindingContext = new TransporteVM();

            listView.ItemTapped += (sender, args) =>
            {
                if (listView.SelectedItem == null)
                    return;

                listView.SelectedItem = null;
            };
        }

        public List<TipoSolicitudTransporte> items { get; set; }

        public List<TipoSolicitudTransporte> Items
        {
            get => items;
            set => items = value;
        }


        private TransporteVM ViewModel => BindingContext as TransporteVM;

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

            await Navigation.PushModalAsync(new MainPage(GlobalVariables.Usuario));
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (ViewModel == null || ViewModel.IsBusy || ViewModel.EstudianteItems.Count > 0)
                //mensajeNoK = true;
                return;
            //else
            //if(ViewModel.Error = true)
            //   this.ShowMessage(MessageSource.messageNoticias, MessageSource.titleGeneral, MessageSource.buttonTextOk, async () => { });
            //return;
            //ViewModel.ConsultarTiposSolicitud("EstudiantePadre");
            ViewModel.LoadItemsCommand.Execute(null);
        }

        private void listEstudiantes_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as Estudiante;
            if (item != null)
            {
                est = item;
                if (arr4 == null)
                {
                    EjecutaTareaAsincrona1(item.TipoPaciente);
                    PickerSolicitudes.ItemsSource = arr4;
                }
                else { 
                PickerSolicitudes.ItemsSource = arr4;
                }
            }

            overlay.IsVisible = true;


            // Navigation.PushAsync(new NavigationPage(new TipoSolicitudTransporte()));
        }


        private async Task EjecutaTareaAsincrona1(string tipoPaciente)
        {
            //bindableRadioGroupPaises.CheckedChanged += BindableRadioGroupPaises_CheckedChanged;
            TiposSol = await obj.ConsultarTiposSolicitud(tipoPaciente);
            arr4 = new string[TiposSol.Length];

            foreach (var item in TiposSol)
                armarTiposSol(item.NombreTipo);
            overlay.IsVisible = true;
        }

        private void armarTiposSol(string sol)
        {
            arr4[inc] = sol;
            inc++;
        }

        private void Inicializar()
        {
        }

        //private void BindableRadioGroupPaises_CheckedChanged(object sender, int e)
        //{
        //    var radio = sender as CustomRadioButton;

        //    if (radio == null || radio.Id == -1)
        //    {
        //        return;
        //    }
        //    foreach (var item in TiposSol)
        //    {
        //        if (item.NombreTipo == radio.Text)
        //        {
        //            slt = item;
        //            /*Mertodo que navega a la pagina de tipo de solicitud que se ha solcitada*/
        //        }
        //    }

        //}


        /// <summary>
        ///     Nombre: Héctor Arias
        ///     Empresa: Colegio San Jorge de Inglaterra
        ///     Fecha: 20/02/2018
        ///     Metodo que se encarga del cierre de la ventana modal
        ///     del tipo de la solicitud.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnCancelButtonClicked(object sender, EventArgs args)
        {
            PickerSolicitudes.SelectedItem = -1;
            slt = null;
            ErrorTiposSol.IsVisible = false;
            overlay.IsVisible = false;
        }

        private async void OnRegistrarButtonClicked(object sender, EventArgs e)
        {
            if (slt != null)
                await Navigation.PushAsync(new CrearSolicitudTransporte(slt, est));
            else
                ErrorTiposSol.IsVisible = true;
        }

        private void PickerSolictudes_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radio = sender as CustomPicker;

            if (radio.SelectedItem == null) return;
            foreach (var item in TiposSol)
                if (item.NombreTipo == radio.SelectedItem.ToString())
                {
                    ErrorTiposSol.IsVisible = false;
                    slt = item;
                    /*Mertodo que navega a la pagina de tipo de solicitud que se ha solcitada*/
                }
        }
    }
}