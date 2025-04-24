using System.ComponentModel.DataAnnotations;

namespace Ginbro.AIModel;

public class AITemplate

{
    [Key] public int Id { get; set; }

    [Required] public string Name { get; set; }
}