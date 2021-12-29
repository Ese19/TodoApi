using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models;

public class Todo {
    public int Id { get; set; }
    
    [Required]
    public string? Name { get; set;}

    public string? Description { get; set; }
    
    public DateTime DueDate { get; set; }
}