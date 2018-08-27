using System.Collections.Generic;
using System.Threading.Tasks;
using SGSApp.Models;
using SQLite;

namespace SGSApp.Data
{
    public class SGSDatabase
    {
        private readonly SQLiteAsyncConnection database;

        #region Constructor

        public SGSDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            //Metodo que crea tabla si esta no existe
            database.CreateTableAsync<NotificacionesPush>().Wait();
        }

        #endregion

        #region Consultas a la Base de Datos

        //Regresar todos los registros de la tabla NotificacionesPush
        public async Task<List<NotificacionesPush>> GetItemsAsync()
        {
            return await database.Table<NotificacionesPush>().ToListAsync();
        }

        //Obtener un solo item 
        public Task<NotificacionesPush> GetItemAsync(int id)
        {
            return database.Table<NotificacionesPush>()
                .Where(i => i.ID == id)
                .FirstOrDefaultAsync();
        }

        //Método para guardar elemento
        public Task<int> SaveItemAsync(NotificacionesPush item)
        {
            //Si exite el elemento se actualiza 
            if (item.ID != 0)
                return database.UpdateAsync(item);
            //Si no existe crea el elemento
            return database.InsertAsync(item);
        }

        //Método para borrar una elemento en este caso una notificación.
        public Task<int> DeleteItemAsync(NotificacionesPush item)
        {
            return database.DeleteAsync(item);
        }

        #endregion
    }
}