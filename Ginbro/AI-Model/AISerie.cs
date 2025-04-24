csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ginbro.AI_Model;

public class AISerie
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    public decimal KG { get; set; }
    public int Repetitions { get; set; }
    public bool MuscleFailure { get; set; }
    [ForeignKey("AIExercise")]
    public int AIExerciseId { get; set; }
    public AIExercise AIExercise {get; set;}
}
