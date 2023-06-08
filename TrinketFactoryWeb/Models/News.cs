namespace TrinketFactoryWeb.Models;

public class News
{
    public int Id { get; set; }
    
    public string? Title { get; set; }
    
    public string? Context { get; set; }
    
    public DateTime CreationTime { get; set; }
}