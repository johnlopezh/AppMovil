using XLabs.Forms.Services;

namespace SGSApp.ViewModel
{
    public class MainVM
    {
        #region Services

        private NavigationService navigationService;

        #endregion

        public MainVM()
        {
            instance = this;
        }

        #region Properties

        public DireccionVM NewCustomer { get; set; }

        #endregion

        #region Sigleton

        private static MainVM instance;

        public static MainVM GetInstance()
        {
            if (instance == null) return new MainVM();

            return instance;
        }

        #endregion
    }
}