using SQLite;

namespace Ginbro.Model;

public class ExerciseDto
{
    [PrimaryKey]
    [AutoIncrement]
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    // public List<Serie>? Series { get; set; }
}