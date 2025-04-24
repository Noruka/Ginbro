
using Ginbro.Shared;
using Ginbro.AI_Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Ginbro.AI_Data
{
    public class AIExerciseTemplateDao
    {
        private readonly SqliteConnectionFactory _connectionFactory;

        public AIExerciseTemplateDao(SqliteConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<AIExerciseTemplate>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Table<AIExerciseTemplate>().Include(x => x.Series).ToList();
        }

        public async Task<AIExerciseTemplate> GetByIdAsync(int id)
        {
            using var connection = _connectionFactory.CreateConnection();
            return connection.Table<AIExerciseTemplate>().Include(x => x.Series).FirstOrDefault(x => x.Id == id);
        }

        public async Task AddAsync(AIExerciseTemplate template)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Insert(template);
        }

        public async Task UpdateAsync(AIExerciseTemplate template)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Update(template);
        }

        public async Task DeleteAsync(AIExerciseTemplate template)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Delete(template);
        }
    }
}