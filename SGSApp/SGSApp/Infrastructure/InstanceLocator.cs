using SGSApp.ViewModel;

namespace SGSApp.Infrastructure
{
    public class InstanceLocator
    {
        public InstanceLocator()
        {
            Main = new MainVM();
            Menu = new MenuOpcionesViewModel();
        }

        public MainVM Main { get; set; }

        public MenuOpcionesViewModel Menu { get; set; }
    }
}