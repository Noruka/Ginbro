
using System.ComponentModel.DataAnnotations.Schema;
using SQLite;

namespace Ginbro.AIModel;

public class AISerie
{
    [PrimaryKey] [AutoIncrement] public int Id { get; set; }

    public string Name { get; set; }
    public decimal KG { get; set; }
    public int Repetitions { get; set; }
    public bool MuscleFailure { get; set; }
    public int AIExerciseId { get; set; }
}