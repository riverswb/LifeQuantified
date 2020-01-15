using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;

namespace LifeQuantified
{
    class LifeQuantifiedDB
    {
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => 
        {
            return new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        });

        static SQLiteAsyncConnection Database => lazyInitializer.Value;
        static bool initialized = false;

 
        public LifeQuantifiedDB()
        {
            InitializeAsync().SafeFireAndForget(false);
        }  

        async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!Database.TableMappings.Any(m => m.MappedType == typeof(Quantity)))
                {
                    await Database.CreateTableAsync(typeof(Quantity), CreateFlags.None).ConfigureAwait(false);
                    initialized = true;
                }
            }
        }

        public Task<List<Quantity>> GetQuantitiesAsync()
        {
            return Database.Table<Quantity>().ToListAsync();
        }

        public Task<Quantity> GetQuantityAsync(int id)
        {
            return Database.Table<Quantity>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveQuantityAsync(Quantity quantity)
        {
            if (quantity.ID != 0)
            {
                return Database.UpdateAsync(quantity);
            }
            else
            {
                return Database.DeleteAsync(quantity);
            }
        }

        public Task<int> DeleteQuantityAsync(Quantity quantity)
        {
            return Database.DeleteAsync(quantity);
        }

    }

}