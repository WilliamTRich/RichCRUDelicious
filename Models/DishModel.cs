#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
namespace RichCRUDelicious.Models;
public class Dish
{
    [Key]
    public int DishId { get; set; }
    [Required(ErrorMessage = "Dish name is required.")]
    public string Name { get; set; }
    [Required(ErrorMessage = "Chef name is required.")]
    public string Chef { get; set; }
    [Required(ErrorMessage = "Tastiness is required.")]
    public int Tastiness { get; set; }
    [Required(ErrorMessage = "Calories is required.")]
    [Range(1.0, Double.MaxValue, ErrorMessage = "Needs to be above 1 Calorie.")]
    public int Calories { get; set; }
    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
