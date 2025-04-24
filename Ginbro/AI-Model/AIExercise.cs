csharp
using System;
using SQLite;

namespace Ginbro.AIModel;

public class AIExercise {
  [PrimaryKey, AutoIncrement]
  public int Id { get; set; }
  public string Name { get; set; }
  public DateTime Date { get; set; }
  public TimeSpan TimeElapsed { get; set; }

}