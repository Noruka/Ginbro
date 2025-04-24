using Ginbro.AIModel;

namespace Ginbro.AI_Data;

public class AITemplateDao : IDisposable
{
    private readonly SqliteConnection _connection;

    public AITemplateDao(SqliteConnection connection)
    {
        _connection = connection;
    }

    public void Dispose()
    {
        _connection.Dispose();
    }

    public async Task Create(AITemplate template)
    {
        var sql = "INSERT INTO AITemplate (Name) VALUES (@Name); SELECT last_insert_rowid();";
        template.Id = await _connection.ExecuteScalarAsync<int>(sql, template);
    }

    public async Task<IEnumerable<AITemplate>> ReadAll()
    {
        var sql = "SELECT * FROM AITemplate";
        return await _connection.QueryAsync<AITemplate>(sql);
    }

    public async Task<AITemplate> ReadById(int id)
    {
        var sql = "SELECT * FROM AITemplate WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<AITemplate>(sql, new { Id = id });
    }

    public async Task Update(AITemplate template)
    {
        var sql = "UPDATE AITemplate SET Name = @Name WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, template);
    }

    public async Task Delete(int id)
    {
        var sql = "DELETE FROM AITemplate WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, new { Id = id });
    }
}