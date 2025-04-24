using Ginbro.AIModel;
using SQLite;

namespace Ginbro.AI_Model
{
    public class AIExerciseTemplate
    {
        [PrimaryKey] [AutoIncrement] public int Id { get; set; }

        public string Name { get; set; }

        [Ignore] public List<AISerie> Series { get; set; } = new List<AISerie>();
    }
}