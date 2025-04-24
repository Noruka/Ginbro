using Ginbro.AIModel;
using Ginbro.Shared;
using SQLite;

namespace Ginbro.AIData
{
    public class AISerieTemplateDao : IDisposable
    {
        private readonly SqliteConnectionFactory _connectionFactory;

        public AISerieTemplateDao(SqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Dispose()
        {
            
        }

        public async Task<int> Create(AISerieTemplate serieTemplate)
        {
            using SQLiteAsyncConnection connection = _connectionFactory.CreateConnection();
            return await connection.InsertAsync(serieTemplate);
        }

        public async Task<List<AISerieTemplate>> ReadAll()
        {
            using SQLiteAsyncConnection connection = _connectionFactory.CreateConnection();
            return await connection.Table<AISerieTemplate>().ToListAsync();
        }

        public async Task<AISerieTemplate> ReadById(int id)
        {
            using SQLiteAsyncConnection connection = _connectionFactory.CreateConnection();
            return await connection.FindAsync<AISerieTemplate>(id);
        }

        public async Task Update(AISerieTemplate serieTemplate)
        {
            using SQLiteAsyncConnection connection = _connectionFactory.CreateConnection();
            await connection.UpdateAsync(serieTemplate);
        }

        public async Task Delete(int id)
        {
            using SQLiteAsyncConnection connection = _connectionFactory.CreateConnection();
            await connection.DeleteAsync<AISerieTemplate>(id);
        }

        public async Task<List<AISerieTemplate>> GetByTemplateId(int templateId)
        {
            using SQLiteAsyncConnection connection = _connectionFactory.CreateConnection();
            return await connection.Table<AISerieTemplate>()
                .Where(st => st.AITemplateId == templateId)
                .ToListAsync();
        }

        ~AISerieTemplateDao()
        {
            Dispose();
        }
    }
}
