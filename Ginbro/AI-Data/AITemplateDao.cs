using Ginbro.AIModel;
using Ginbro.Shared;
using SQLite;

namespace Ginbro.AIData;

public class AITemplateDao : IDisposable
{
    private readonly SqliteConnectionFactory _connectionFactory;
    private SQLiteAsyncConnection _connection;

    public AITemplateDao(SqliteConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
        _connection = _connectionFactory.CreateConnection();
    }

    public async Task Create(AITemplate template)
    {
        await _connection.InsertAsync(template);
    }

    public async Task<List<AITemplate>> ReadAll()
    {
        return await _connection.Table<AITemplate>().ToListAsync();
    }

    public async Task<AITemplate> ReadById(int id)
    {
        return await _connection.FindAsync<AITemplate>(id);
    }

    public async Task Update(AITemplate template)
    {
        await _connection.UpdateAsync(template);
    }

    public async Task Delete(int id)
    {
        await _connection.DeleteAsync<AITemplate>(id);
    }


    public void Dispose()
    {
        _connection.CloseAsync();
        _connection = null;
    }

}
