using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using SGSApp.Helper;

namespace SGSApp.Models
{
    public class NotificacionesRepository
    {
        public NotificacionesRepository()
        {
            Task.Run(async () => NotificacionesP = await App.Database.GetItemsAsync()).Wait();
        }

        public IList<NotificacionesPush> NotificacionesP { get; set; }

        public IList<NotificacionesPush> GetAll()
        {
            return NotificacionesP;
        }

        public IList<NotificacionesPush> GetAllByFirstLetter(string letter)
        {
            var query = from q in NotificacionesP
                where q.TextoNotificacionPush.StartsWith(letter)
                select q;
            return query.ToList();
        }

        public ObservableCollection<Grouping<string, NotificacionesPush>> GetAllGrouped()
        {
            IEnumerable<Grouping<string, NotificacionesPush>> sorted = new Grouping<string, NotificacionesPush>[0];
            if (NotificacionesP != null)
                sorted =
                    from f in NotificacionesP
                    orderby f.TextoNotificacionPush
                    group f by f.TextoNotificacionPush[0].ToString()
                    into theGroup
                    select new Grouping<string, NotificacionesPush>(theGroup.Key, theGroup);
            return new ObservableCollection<Grouping<string, NotificacionesPush>>(sorted);
        }
    }
}