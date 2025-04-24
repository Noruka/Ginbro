using Ginbro.AIModel;
using Ginbro.Shared;
using SQLite;

namespace Ginbro.AIData;

public class AIExerciseDao : IDisposable
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private bool disposed;

    public AIExerciseDao(SqliteConnectionFactory connection)
    {
        _connectionFactory = connection;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<int> Create(AIExercise exercise)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.InsertAsync(exercise);
    }

    public async Task<List<AIExercise>> GetAll()
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.Table<AIExercise>().ToListAsync();
    }

    public async Task<AIExercise> GetById(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await connection.FindAsync<AIExercise>(id);
    }

    public async Task Update(AIExercise exercise)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.UpdateAsync(exercise);
    }

    public async Task Delete(int id)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.DeleteAsync<AIExercise>(id);
    }
    
        public async Task Delete(AIExercise exercise)
    {
        using var connection = _connectionFactory.CreateConnection();
        await connection.DeleteAsync(exercise);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
          
            disposed = true;
        }
    }

    ~AIExerciseDao()
    {
        Dispose(false);
    }
}