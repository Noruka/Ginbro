using SQLite;

namespace Ginbro.AIModel;

public class AISerieTemplate
{
    [PrimaryKey] [AutoIncrement] public int Id { get; set; }

    [MaxLength(255)] public string Name { get; set; }

    public decimal KG { get; set; }

    public int Repetitions { get; set; }

    public bool MuscleFailure { get; set; }

    [Indexed] public int AITemplateId { get; set; }
}