csharp
using Dapper;
using Microsoft.Data.Sqlite;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ginbro.AI_Model;
using System;

namespace Ginbro.AIData;

public class AISerieDao : IDisposable {
    private readonly SqliteConnection _connection;

    public AISerieDao(SqliteConnection connection)
    {
        _connection = connection;
    }
    public async Task<int> CreateAsync(AISerie serie)
    {
        const string sql = "INSERT INTO AISerie (Name, KG, Repetitions, MuscleFailure, AIExerciseId) VALUES (@Name, @KG, @Repetitions, @MuscleFailure, @AIExerciseId); SELECT last_insert_rowid();";
        return await _connection.ExecuteScalarAsync<int>(sql, serie);
    }
    public async Task<IEnumerable<AISerie>> ReadAllAsync()
    {
        const string sql = "SELECT Id, Name, KG, Repetitions, MuscleFailure, AIExerciseId FROM AISerie";
        return await _connection.QueryAsync<AISerie>(sql);
    }
    
    public async Task<AISerie> ReadByIdAsync(int id)
    {
        const string sql = "SELECT Id, Name, KG, Repetitions, MuscleFailure, AIExerciseId FROM AISerie WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<AISerie>(sql, new { Id = id });
    }
    
    public async Task<IEnumerable<AISerie>> ReadByExerciseIdAsync(int exerciseId)
    {
        const string sql = "SELECT Id, Name, KG, Repetitions, MuscleFailure, AIExerciseId FROM AISerie WHERE AIExerciseId = @AIExerciseId";
        return await _connection.QueryAsync<AISerie>(sql, new { AIExerciseId = exerciseId });
    }
    
    public async Task<int> UpdateAsync(AISerie serie)
    {
        const string sql = @"
            UPDATE AISerie SET 
                Name = @Name, 
                KG = @KG, 
                Repetitions = @Repetitions, 
                MuscleFailure = @MuscleFailure, 
                AIExerciseId = @AIExerciseId
            WHERE Id = @Id";
        return await _connection.ExecuteAsync(sql, serie);
    }
    
    public async Task<int> DeleteAsync(int id)
    {
        const string sql = "DELETE FROM AISerie WHERE Id = @Id";
        return await _connection.ExecuteAsync(sql, new { Id = id });
    }

    public void Dispose()
    {
        _connection.Dispose();
    }
}
