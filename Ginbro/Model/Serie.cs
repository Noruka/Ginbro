namespace Ginbro.Model;

public class Serie
{
    public int ParentId { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public double Weight { get; set; }
    public int Repetitions { get; set; }
    public bool MuscleFailure { get; set; }
}