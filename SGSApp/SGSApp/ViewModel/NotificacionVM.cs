using System.Threading.Tasks;
using SGSApp.Models;
using Xamarin.Forms;

namespace SGSApp.ViewModel
{
    public class NotificacionVM
    {
        #region Metodos

        public async Task GuardarNotificacion()
        {
            await Navigation.PopAsync();
        }

        #endregion

        #region Propiedades

        public Command AddNotificacionCommand { get; set; }

        public NotificacionesPush NewNotificacion { get; set; }

        //Permite hacer un Push y Pop a partir de la navegación
        private readonly INavigation Navigation;

        #endregion

        #region Constructores

        public NotificacionVM(INavigation navigation)
        {
            NewNotificacion = new NotificacionesPush();
            AddNotificacionCommand = new Command(async () => await GuardarNotificacion());
            Navigation = navigation;
        }

        public NotificacionVM(INavigation navigation, NotificacionesPush newNotificacion)
        {
            NewNotificacion = newNotificacion;
            AddNotificacionCommand = new Command(async () => await GuardarNotificacion());
            Navigation = navigation;
        }

        #endregion
    }
}