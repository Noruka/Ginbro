--- a/Ginbro/AI-Model/AITemplate.cs
+++ b/Ginbro/AI-Model/AITemplate.cs

+namespace Ginbro.AIModel;

+public class AITemplate
+{
+    [Key]
+    public int Id { get; set; }
+
+    [Required]
+    public string Name { get; set; }
+}