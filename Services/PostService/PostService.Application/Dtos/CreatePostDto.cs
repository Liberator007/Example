using System.ComponentModel.DataAnnotations;

namespace PostService.Application.Dtos;

public class CreatePostDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = null!;

    [Required]
    public string Text { get; set; } = null!;
}