using CommunityToolkit.Mvvm.ComponentModel;

namespace Ginbro.Model;

public class Exercise : ObservableObject
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public DateTime Date { get; set; }
    public List<Serie>? Series { get; set; }
}