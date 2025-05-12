using DataAccess.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; init; }

    public required string Title { get; set; }
    public string? Description { get; set; }

    public string Slug { get; set; } = string.Empty; 

    public DateTime? PublishedDate { get; set; }
    public bool IsPublished { get; set; } = true;

    public ICollection<BookTag> BookTags { get; set; } = new List<BookTag>();
    public required ICollection<Author> Authors { get; set; }
    public required Guid PublisherId { get; set; }
    public required Publisher Publisher { get; set; }

    public ICollection<Image> Images { get; set; } = new List<Image>();

    public void SetSlug()
    {
        Slug = GenerateSlug(Title);
    }

    private string GenerateSlug(string title)
    {
        return Regex.Replace(title, "[^0-9A-Za-z _-]", string.Empty)
            .ToLower().Replace(" ", "-");
    }
}
