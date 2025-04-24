using Ginbro.AIModel;
using Ginbro.Shared;
using SQLite;

namespace Ginbro.AIData
{
    public class AISerieDao : IDisposable
    {
        private readonly SqliteConnectionFactory _connectionFactory;
        private SQLiteAsyncConnection _connection;

        public AISerieDao(SqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
            _connection = _connectionFactory.CreateConnection();
        }

        public void Dispose()
        {
            _connection?.CloseAsync();
        }

        public async Task<int> CreateAsync(AISerie serie)
        {
            await _connection.InsertAsync(serie);
            return serie.Id;
        }

        public async Task<List<AISerie>> ReadAllAsync()
        {
            return await _connection.Table<AISerie>().ToListAsync();
        }

        public async Task<AISerie> ReadByIdAsync(int id)
        {
            return await _connection.FindAsync<AISerie>(id);
        }

        public async Task<List<AISerie>> ReadByExerciseIdAsync(int exerciseId)
        {
            return await _connection.Table<AISerie>().Where(x => x.AIExerciseId == exerciseId).ToListAsync();
        }

        public async Task UpdateAsync(AISerie serie)
        {
            await _connection.UpdateAsync(serie);
        }

        public async Task DeleteAsync(int id)
        {
            await _connection.DeleteAsync<AISerie>(id);
        }

        public async Task DeleteAsync(AISerie serie)
        {
            await _connection.DeleteAsync(serie);
        }
        
        public async Task<List<AISerie>> GetAllByExerciseId(int exerciseId)
        {
            return await _connection.Table<AISerie>().Where(s => s.AIExerciseId == exerciseId).ToListAsync();
        }
    }
}
