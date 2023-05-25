using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace TrinketFactoryStore.Models;

public class Category
{
    public int Id { get; set; }
    [Required] 
    [DisplayName("Category Name")] 
    public string Name { get; set; }
    [DisplayName("Display Order")] 
    [Range(1,100), DefaultValue(1)]
    public int DisplayOrder { get; set; }
    
}
