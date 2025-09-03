using System.ComponentModel.DataAnnotations;

namespace PostService.Application.Dtos;

public class UpdatePostDto
{
    [Required]
    public PostId Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [Required]
    public string Text { get; set; }
}