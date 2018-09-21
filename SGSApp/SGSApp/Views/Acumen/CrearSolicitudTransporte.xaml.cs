using System;
using System.Threading.Tasks;
using System.Windows.Input;
using SGSApp.Controls;
using SGSApp.Helper;
using SGSApp.Interfaces;
using SGSApp.Models;
using SGSApp.ViewModel;
using SGSApp.Views.Master;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SGSApp.Views.Acumen
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CrearSolicitudTransporte : ContentPage
    {
        private string[] arrDirecciones;
        /*************/

        private readonly int codigoFamiliar;
        private readonly string EstadoSolicitud;
        private readonly string[] estCompañeros = new string[60];
        private readonly DateTime FechaSolicitud;
        private readonly string[] FechasSeleccionadas = new string[60];
        private readonly TimeSpan Hora;
        private readonly int? IdDireccion;
        private readonly string IdentificacionAutorizado;
        private readonly string IdentificacionSolicitante;

        /* Objeto de Solicitud Transporte */
        private readonly long IdTipoSolicitudTransporte;
        private readonly bool? JornadaExtenHabilitada;
        private readonly bool? JornadaExtraHabilitada;
        private readonly bool? JornadaMananaHabilitada;
        private readonly bool? JornadaTardeHabilitada;
        private readonly int MotivoRechazo;
        private readonly string NombreAutorizado;
        private readonly CalendarioVM obj = new CalendarioVM();
        private readonly DireccionVM objDireccion = new DireccionVM();
        private readonly TransporteVM objTransporte = new TransporteVM();
        private readonly int PeriodoLectivo;
        private readonly bool? Permanente;
        private readonly long? Telefono;
        private readonly bool? Temporal;
        private readonly string TipoIdentSolicitante;
        private readonly string UsuarioLog;
        private string[] arrFechas;

        private int clickTotal = 0;
        private Direccion[] direccionesGrupoFamiliar;
        private Direccion dir;
        private bool enableMultiSelect;
        private Estudiante[] estudiantes;
        private CalendarioAcademico[] fechasCalendario;
        private string IdentificacionCompanero;
        private int inc;
        private Label label;
        private INavigationService navigationService;
        private string Observaciones;
        private string[] pp;
        private int resultadoTransporte;
        private string TipoIdentCompanero;

        public bool? FechaMultipleHabilitada;
        public bool? CampoHoraHabilitada;
        public bool? CampoDireccionHabilitado;
        public bool? BotonDireccionHabilitado;
        public bool? CampoCompaneroHabilitado;
        public bool? CampoNombrePHabilitado;
        public bool? CampoIdentPHabilitado;
        public bool? CampoTelefonoHabilitado;
        public bool? CampoObsHabilitado;

        private TransporteVM ViewModel => BindingContext as TransporteVM;

        public CrearSolicitudTransporte(TipoSolicitudTransporte tp, Estudiante est)
        {
            InitializeComponent();

            FechaMultipleHabilitada = tp.FechaMultipleHabilitada;
            CampoHoraHabilitada = tp.CampoHoraHabilitado;
            CampoDireccionHabilitado = tp.CampoDireccionHabilitado;
            BotonDireccionHabilitado = tp.BotonDireccionHabilitado;
            CampoCompaneroHabilitado = tp.CampoCompaneroHabilitado;
            CampoNombrePHabilitado = tp.CampoNombrePHabilitado;
            CampoIdentPHabilitado = tp.CampoIdentPHabilitado;
            CampoTelefonoHabilitado = tp.CampoTelefonoHabilitado;
            CampoObsHabilitado = tp.CampoObsHabilitado;

            SwitchDireccion.IsToggled = true;
            autocompleteCompañero.IsEnabled = false;

            codigoFamiliar = Convert.ToInt16(est.IdFamilia);
            IdTipoSolicitudTransporte = tp.IdTipoSolicitudTransporte;
            TipoIdentSolicitante = est.TipoIdentificacion;
            IdentificacionSolicitante = est.NumeroIdentificacion;
            Temporal = tp.Temporal;
            Permanente = tp.Permanente;
            Hora = TimePickerHora.Time;
            IdDireccion = null; //IdDireccion;
            IdentificacionCompanero = "";
            TipoIdentCompanero = "";
            NombreAutorizado = "";
            IdentificacionAutorizado = "";
            Telefono = null;
            JornadaMananaHabilitada = tp.JornadaMananaHabilitada;
            JornadaTardeHabilitada = tp.JornadaTardeHabilitada;
            JornadaExtraHabilitada = tp.JornadaExtraHabilitada;
            JornadaExtenHabilitada = tp.JornadaExtenHabilitada;
            Observaciones = MotivoEntry.Text;
            EstadoSolicitud = "P";
            UsuarioLog = GlobalVariables.Email;
            FechaSolicitud = FechaSolicitud;
            PeriodoLectivo = PeriodoLectivo;
            MotivoRechazo = MotivoRechazo;

            LblNombreSolicitud.Text = tp.NombreTipo;
            //TableSec.Title = tp.NombreTipo;
            //LblnombreTipoSolicitud.Text = tp.NombreTipo;
            //TblSectituloSol.Title = tp.NombreTipo;
            NombreEstudiante.Text = "Nombre: " + est.NombreCompleto;
            CursoEstudiante.Text = "Nombre: " + est.NombreCurso;
            FotoEstudiante.Source = est.UrlFoto;
            // NombreEstudiante.Detail = "Curso: " + est.NombreCurso;

            if (tp.FechaMultipleHabilitada == true)
            {
                FechaM.Text = tp.CampoFecha + " (*):";
                FechaMultiple.IsVisible = true;
                Fecha.IsVisible = false;
            }
            else
            {
                FechaM.Text = tp.CampoFecha + " (*):";
                FechaMultiple.IsVisible = false;
                Fecha.IsVisible = true;
            }

            if (tp.CampoHoraHabilitado == true)
            {
                LblHora.IsVisible = true;
                LblHora.Text = tp.CampoHora + " (*):";
                TimePickerHora.IsVisible = true;
            }
            else
            {
                TableSec.Remove(cHora);
                LblHora.IsVisible = false;
                TimePickerHora.IsVisible = false;
            }

            if (tp.CampoTelefonoHabilitado == true)
            {
                TelefonoContactoEntry.Label = tp.CampoTelefono + " (*):";
            }

            if (tp.CampoObsHabilitado == true)
            {
                MotivoEntry.Label = tp.CampoObservaciones + " (*):";
            }

            if (tp.BotonDireccionHabilitado == true)
            {
                TableSectionEntregaEstudiante.Title = tp.CampoDestino;
            }
            else
            {
                TableSectionEntregaEstudiante.Remove(LugarEntregarDireccion);
                TableSectionEntregaEstudiante.Remove(LugarEntregarCompañero);
            }

            if (tp.CampoNombrePHabilitado == true)
            {
                TableSectionPersonaAutorizada.Title = tp.CampoPersonaAutorizada;
            }
            else
            {
                FormularioSolicitud.Root.Remove(TableSectionPersonaAutorizada);
            }

            if (tp.CampoObsHabilitado == true)
                TableSectionInformacion.Title = "Información";
            else
                FormularioSolicitud.Root.Remove(TableSectionInformacion);

            EjecutaTareaAsincrona1();
            ConsultarDireccionesGrupo();
            EjecutaTareaAsincronaEstudiantes();

            //TimePickerHora.SetBinding(TimePicker.TimeProperty, new Binding("StartDate", BindingMode.TwoWay));

            Items = new SelectableObservableCollection<string>(arrFechas);

            RemoveSelectedCommand = new Command(OnRemoveSelected);
            SeleccionarFecha = new Command(OnFechaSelected);


            BindingContext = this;
            // bindableRadioGroupFechas.ItemsSource = arr4;
        }

        public SelectableObservableCollection<string> Items { get; }

        public ICommand AddItemCommand { get; }

        public ICommand RemoveSelectedCommand { get; }

        public ICommand SeleccionarFecha { get; }

        public ICommand NavigateCommand => new Command(async () => await NavigateAsyn());

        private void OnFechaSelected()
        {
            overlayFecha.IsVisible = false;
        }

        private void OnRemoveSelected()
        {
            inc = 0;
            FechasSeleccionadasEntry.Text = string.Empty;
            foreach (var item in Items)
            {
                var fechas = item.IsSelected;
                if (fechas)
                {
                    FechasSeleccionadas[inc] = item.Data;
                    //FechasSeleccionadasEntry.Text = "2018-01-29, 2018-02-01,";
                    FechasSeleccionadasEntry.Text = string.Concat(FechasSeleccionadasEntry.Text, item.Data, ",");
                    FechasSeleccionadasEntry.Text = FechasSeleccionadasEntry.Text.TrimEnd(',');
                    inc++;
                }
            }

            overlayFechaM.IsVisible = false;
        }

        private async Task EjecutaTareaAsincrona1()
        {
            //bindableRadioGroupFechas.CheckedChanged += BindableRadioGroupFechas_CheckedChanged;
            fechasCalendario = await obj.ConsultarFechasHabilesCalendario(IdTipoSolicitudTransporte);
            arrFechas = new string[fechasCalendario.Length];
            foreach (var item in fechasCalendario)
            {
                armarFechasHabiles(item.FechaCalendario);
                Task.Delay(5000);
            }

            //pruebas23.CheckedText = arr4;
            //lvDetails.ItemsSource = TiposSol;
            PickerFechas.ItemsSource = arrFechas;
            //lvDetails.ItemsSource = arr4;
        }

        private async Task EjecutaTareaAsincronaEstudiantes()
        {
            PickerDireccion.IsVisible = false;
            PickerDireccion.IsVisible = true;

            PickerDireccion.ItemsSource = arrDirecciones;
            PickerFechas.ItemsSource = arrFechas;
            var pp = new string[1];
            pp[0] = "we";
            PickerDireccion.ItemsSource.Add(pp);
            PickerDireccion.ItemsSource.Add(objDireccion);
            //bindableRadioGroupFechas.ItemsSource = arrFechas;

            //estudiantes = await objTransporte.ConsultarEstudiantesCompañeroTransporte();
            ////List<Estudiante> listaNombres = new List<Estudiante>();
            //foreach (var item in estudiantes)
            //{
            //    armarCompañeros(item.NombreCompleto);
            //    autocompleteCompañero.DataSource = estCompañeros;
            //}

            //autocompleteCompañero.AutoCompleteMode = Syncfusion.SfAutoComplete.XForms.AutoCompleteMode.Suggest;
            //autocompleteCompañero.SuggestionMode = Syncfusion.SfAutoComplete.XForms.SuggestionMode.Contains;
        }

        private void SwitchToggledDireccion(object sender, ToggledEventArgs e)
        {
            var DirreccionHabilitada = e.Value;
            if (DirreccionHabilitada)
            {
                SwitchCompañero.IsToggled = false;
                PickerDireccion.IsEnabled = true;
            }
            else
            {
                SwitchCompañero.IsToggled = true;
                PickerDireccion.IsEnabled = false;
            }
        }

        private void SwitchToggledCompanero(object sender, ToggledEventArgs e)
        {
            var CompaneroHabilitada = e.Value;
            if (CompaneroHabilitada)
            {
                SwitchDireccion.IsToggled = false;
                autocompleteCompañero.IsEnabled = true;
            }
            else
            {
                SwitchDireccion.IsToggled = true;
                autocompleteCompañero.IsEnabled = false;
            }
        }

        private void armarFechasHabiles(string fecha)
        {
            arrFechas[inc] = fecha;
            inc++;
        }

        private void armarDirecciones(string direccion)
        {
            arrDirecciones[inc] = direccion;
            inc++;
        }

        private void armarCompañeros(string sol)
        {
            estCompañeros[inc] = sol;
            inc++;
        }

        //private void BindableRadioGroupFechas_CheckedChanged(object sender, int e)
        //{
        //    var radio = sender as Controls.CustomRadioButton;

        //    if (radio == null || radio.Id == -1)
        //    {
        //        return;
        //    }
        //    foreach (var item in arrFechas)
        //    {
        //        if (item == radio.Text)
        //        {
        //            FechasSeleccionadasEntry.Text = item;
        //            /*Mertodo que navega a la pagina de tipo de solicitud que se ha solcitada*/
        //        }
        //    }

        //}

        private void OnCancelFechaButtonClicked(object sender, EventArgs e)
        {
            overlayFecha.IsVisible = false;
        }

        private void OnCancelFechaMButtonClicked(object sender, EventArgs e)
        {
            overlayFechaM.IsVisible = false;
        }

        private async void OnCancelConfirmacionButtonClicked(object sender, EventArgs e)
        {
            overlayConfirmacion.IsVisible = false;
            await Navigation.PopModalAsync();
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            //this.EjecutaTareaAsincrona1();
            overlayFecha.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            overlayFechaM.IsVisible = true;
        }

        private async Task ConsultarDireccionesGrupo()
        {
            PickerDireccion.ItemsSource = pp;
            direccionesGrupoFamiliar = await objDireccion.ConsultarDireccionesGrupo(codigoFamiliar);
            inc = 0;
            arrDirecciones = new string[direccionesGrupoFamiliar.Length];
            foreach (var item in direccionesGrupoFamiliar)
            {
                armarDirecciones(item.DireccionGrupo);
                Task.Delay(15000);
            }

            PickerDireccion.ItemsSource = arrDirecciones;
        }

        private async Task GuardarSolicitud()
        {
            try
            {
                var solicitudTranspote = new SolicitudTransporteApp
                {
                    IdTipoSolicitudTransporte = IdTipoSolicitudTransporte,
                    TipoIdentSolicitante = TipoIdentSolicitante,
                    IdentificacionSolicitante = IdentificacionSolicitante,
                    Temporal = Temporal,
                    Permanente = Permanente,
                    Hora = Hora,
                    IdDireccion = (dir != null) ? dir.IdDireccion  : 0,
                    IdentificacionCompanero = null,
                    TipoIdentCompanero = null,
                    NombreAutorizado = NombrePersonaAutorizadaEntry.Text,
                    IdentificacionAutorizado = IdentificacionPersonaAutorizadaEntry.Text,
                    Telefono = Convert.ToInt32(TelefonoContactoEntry.Text),
                    JornadaMananaHabilitada = JornadaMananaHabilitada,
                    JornadaTardeHabilitada = JornadaTardeHabilitada,
                    JornadaExtraHabilitada = JornadaExtraHabilitada,
                    JornadaExtenHabilitada = JornadaExtenHabilitada,
                    Observaciones = MotivoEntry.Text,
                    EstadoSolicitud = EstadoSolicitud,
                    UsuarioLog = UsuarioLog,
                    FechaSolicitud = DateTime.Now,
                    PeriodoLectivo = 13,
                    fechas = FechasSeleccionadasEntry.Text = FechasSeleccionadasEntry.Text.TrimEnd(','),
                    rol = 6
                };
                resultadoTransporte = await objTransporte.GuardarSolicitudTransporte(solicitudTranspote);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
       
        public async Task GuardarBtn_Clicked(object sender, EventArgs e)
        {
            if (await validarFormulario())
            {
                await GuardarSolicitud();
                
                if (resultadoTransporte == 1)
                    overlayConfirmacion.IsVisible = true;
            }
        }

        private async Task<bool> validarFormulario()
        {
            //Valida si el valor en el Entry se encuentra vacio o es igual a Null
            if (String.IsNullOrWhiteSpace(FechasSeleccionadasEntry.Text))
            {
                await this.DisplayAlert("Información incompleta.", "Los campos señalados con asterisco (*) son obligatorios.", "OK");
                return false;
            }
            if (String.IsNullOrWhiteSpace(TimePickerHora.Time.ToString()) && Temporal == true && LblHora.IsVisible == true)
            {
                await this.DisplayAlert("Información incompleta.", "Los campos señalados con asterisco (*) son obligatorios.", "OK");
                return false;

            }
            else
            {
                if ((Convert.ToInt32(TimePickerHora.Time.ToString().Substring(0, 2)) < 7 || Convert.ToInt32(TimePickerHora.Time.ToString().Substring(0, 2)) > 19) && Temporal == true && LblHora.IsVisible)
                {
                    await this.DisplayAlert("Información incompleta.", "La hora permitida es de las 07:00 a.m hasta las 07:00 p.m", "OK");
                    return false;
                }
                else if (Convert.ToInt32(TimePickerHora.Time.ToString().Substring(0, 2)) == 19 && Convert.ToInt32(TimePickerHora.Time.ToString().Substring(3, 2)) >= 01 && LblHora.IsVisible)
                {
                    await this.DisplayAlert("Información incompleta.", "La hora permitida es de las 07:00 a.m hasta las 07:00 p.m", "OK");
                    return false;
                }
            }

            if ((String.IsNullOrWhiteSpace(NombrePersonaAutorizadaEntry.Text) && Temporal == true && CampoNombrePHabilitado == true))
            {
                await this.DisplayAlert("Información incompleta.", "Los campos señalados con asterisco (*) son obligatorios.", "OK");
                return false;
            }
            if ((String.IsNullOrWhiteSpace(IdentificacionPersonaAutorizadaEntry.Text)) && Temporal == true && CampoIdentPHabilitado == true)
            {
                await this.DisplayAlert("Información incompleta.", "Los campos señalados con asterisco (*) son obligatorios.", "OK");
                return false;
            }
            if ((String.IsNullOrWhiteSpace(MotivoEntry.Text)) && CampoObsHabilitado == true)
            {
                await this.DisplayAlert("Información incompleta.", "Los campos señalados con asterisco (*) son obligatorios.", "OK");
                return false;
            }
            if ((String.IsNullOrWhiteSpace(TelefonoContactoEntry.Text)) && CampoTelefonoHabilitado == true)
            {
                await this.DisplayAlert("Información incompleta.", "Los campos señalados con asterisco (*) son obligatorios.", "OK");
                return false;
            }
            if (dir != null) {
                if ((String.IsNullOrWhiteSpace(dir.DireccionGrupo)) && CampoDireccionHabilitado == true)
                {
                    await this.DisplayAlert("Información incompleta.", "Los campos señalados con asterisco (*) son obligatorios.", "OK");
                    return false;
                }
            }
            return true;
        }

        private void PickerFechas_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radio = sender as CustomPicker;

            if (radio == null || radio.SelectedIndex == -1) return;
            foreach (var item in arrFechas)
                if (item == radio.SelectedItem)
                    FechasSeleccionadasEntry.Text = item;
            FechasSeleccionadasEntry.Text = FechasSeleccionadasEntry.Text.TrimEnd(',');
        }

        private async Task NavigateAsyn()
        {
            await Navigation.PushAsync(new AgregarDireccion(codigoFamiliar));
        }

        private void PickerDireccion_Focused(object sender, FocusEventArgs e)
        {
            ConsultarDireccionesGrupo();
            EjecutaTareaAsincronaEstudiantes();
        }

        private void PickerDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            var radio = sender as CustomPicker;

            if (radio.SelectedItem == null) return;
            foreach (var item in direccionesGrupoFamiliar)
                if (item.DireccionGrupo == radio.SelectedItem.ToString())
                {
                    dir = item;
                    /*Mertodo que navega a la pagina de tipo de solicitud que se ha solcitada*/
                }
        }

        private void LimpiarBtn_Clicked(object sender, EventArgs e)
        {
            FechasSeleccionadasEntry.Text = "";
            IdentificacionPersonaAutorizadaEntry.Text = "";
            MotivoEntry.Text = "";
            NombrePersonaAutorizadaEntry.Text = "";
            TelefonoContactoEntry.Text = "";
            
        }

       
    }
}