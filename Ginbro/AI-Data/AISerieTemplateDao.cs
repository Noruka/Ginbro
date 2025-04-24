
using Dapper;
using Ginbro.AI_Model;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ginbro.AIModel;

namespace Ginbro.AIData;

public class AISerieTemplateDao : IDisposable
{
    private readonly SqliteConnection _connection;

    public AISerieTemplateDao(SqliteConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> Create(AISerieTemplate serieTemplate)
    {
        var sql =
            "INSERT INTO AISerieTemplate (Name, KG, Repetitions, MuscleFailure, AITemplateId) VALUES (@Name, @KG, @Repetitions, @MuscleFailure, @AITemplateId); SELECT last_insert_rowid();";
        return await _connection.ExecuteScalarAsync<int>(sql, serieTemplate);
    }

    public async Task<IEnumerable<AISerieTemplate>> ReadAll()
    {
        var sql = "SELECT * FROM AISerieTemplate";
        return await _connection.QueryAsync<AISerieTemplate>(sql);
    }

    public async Task<AISerieTemplate> ReadById(int id)
    {
        var sql = "SELECT * FROM AISerieTemplate WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<AISerieTemplate>(sql, new { Id = id });
    }

    public async Task Update(AISerieTemplate serieTemplate)
    {
        var sql =
            "UPDATE AISerieTemplate SET Name = @Name, KG = @KG, Repetitions = @Repetitions, MuscleFailure = @MuscleFailure, AITemplateId = @AITemplateId WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, serieTemplate);
    }

    public async Task Delete(int id)
    {
        var sql = "DELETE FROM AISerieTemplate WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, new { Id = id });
    }

    public async Task<IEnumerable<AISerieTemplate>> GetByTemplateId(int templateId)
    {
        var sql = "SELECT * FROM AISerieTemplate WHERE AITemplateId = @AITemplateId";
        return await _connection.QueryAsync<AISerieTemplate>(sql, new { AITemplateId = templateId });
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}