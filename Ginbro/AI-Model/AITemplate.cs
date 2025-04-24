csharp
using System.ComponentModel.DataAnnotations;

namespace Ginbro.AI_Model
{
    public class AITemplate
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}