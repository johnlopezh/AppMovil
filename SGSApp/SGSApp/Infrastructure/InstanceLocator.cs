using SGSApp.ViewModel;

namespace SGSApp.Infrastructure
{
    public class InstanceLocator
    {

        // 
        public InstanceLocator()
        {
            Menu = new MenuOpcionesViewModel();
        }

        public MenuOpcionesViewModel Menu { get; set; }
    }
}