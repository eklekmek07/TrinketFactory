using System.ComponentModel.DataAnnotations.Schema;

namespace TrinketFactoryWeb.Models;

public class Cart
{
    public int Id { get; set; }
    [ForeignKey("ApplicationUserId")]
    public ApplicationUser ApplicationUser { get; set; }
    public string ApplicationUserId { get; set; }
}