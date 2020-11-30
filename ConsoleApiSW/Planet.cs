using System.ComponentModel.DataAnnotations;
public class Planet
{
    // planet ID
    public int Id { get; set; }
    // planet name
    [Required]
    public string Name { get; set; }
    [Required]
    public long? Population { get; set; }
}