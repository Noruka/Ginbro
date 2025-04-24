csharp
--- a/Ginbro/AI-Data/AIExerciseDao.cs
+++ b/Ginbro/AI-Data/AIExerciseDao.cs


namespace Ginbro.AI_Data;

public class AIExerciseDao : IDisposable
{
    private readonly SqliteConnection _connection;
    private bool disposed = false;

    public AIExerciseDao(SqliteConnection connection)
    {
        _connection = connection;
    }

    public async Task<int> CreateAsync(AIExercise exercise)
    {
        var sql = @"INSERT INTO AIExercise (Name, Date, TimeElapsed) VALUES (@Name, @Date, @TimeElapsed);
                    SELECT last_insert_rowid();";
        return await _connection.ExecuteScalarAsync<int>(sql, exercise);
    }

    public async Task<List<AIExercise>> ReadAllAsync()
    {
        var sql = "SELECT * FROM AIExercise";
        return (await _connection.QueryAsync<AIExercise>(sql)).AsList();
    }

    public async Task<AIExercise> ReadByIdAsync(int id)
    {
        var sql = "SELECT * FROM AIExercise WHERE Id = @Id";
        return await _connection.QueryFirstOrDefaultAsync<AIExercise>(sql, new { Id = id });
    }

    public async Task UpdateAsync(AIExercise exercise)
    {
        var sql = @"UPDATE AIExercise 
                    SET Name = @Name, 
                        Date = @Date, 
                        TimeElapsed = @TimeElapsed 
                    WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, exercise);
    }

    public async Task DeleteAsync(int id)
    {
        var sql = "DELETE FROM AIExercise WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, new { Id = id });
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _connection.Close();
                _connection.Dispose();
            }
            disposed = true;
        }
    }

    ~AIExerciseDao()
    {
        Dispose(false);
    }

    public async Task DeleteAsync(AIExercise exercise)
    {
        var sql = "DELETE FROM AIExercise WHERE Id = @Id";
        await _connection.ExecuteAsync(sql, new { Id = exercise.Id });
    }
    
    public async Task<List<AIExercise>> ReadAllWithSeriesAsync()
    {
        var sql = "SELECT e.*, s.* FROM AIExercise e LEFT JOIN AISerie s ON e.Id = s.AIExerciseId";
        var exercises = await _connection.QueryAsync<AIExercise, AISerie, AIExercise>(
            sql, (exercise, serie) => { exercise.Series.Add(serie); return exercise; }, splitOn: "Id");
        return exercises.GroupBy(e => e.Id).Select(g => { var e = g.First(); e.Series = g.Select(x => x.Series.FirstOrDefault()).Where(s => s != null).ToList(); return e; }).ToList();
    }
}
