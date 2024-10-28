using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Counter.Models
{
    public class CounterDatabase
    {
        private readonly SQLiteAsyncConnection _connection;

        public CounterDatabase(string connectionString)
        {
            _connection = new SQLiteAsyncConnection(connectionString);
            _connection.CreateTableAsync<Counter>().Wait();
        }

        public Task<List<Counter>> GetCountersAsync()
        {
            return _connection.Table<Counter>().ToListAsync();
        }

        public Task<Counter> GetCounterByIdAsync(int id)
        {
            return _connection.Table<Counter>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCountersAsync(Counter counter)
        {
            if(counter.Id != 0)
            {
                return _connection.UpdateAsync(counter);
            } else
            {
                return _connection.InsertAsync(counter);
            }
        }

        public Task<int> DeleteCounterAsync(Counter counter)
        {
            return _connection.DeleteAsync(counter);
        }
       
    }
}
