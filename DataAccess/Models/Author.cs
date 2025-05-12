using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataAccess.Models
{
    public class Author
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public required string Name { get; set; }
        public  DateTime CreatedOn { get; set; } = DateTime.Now;
        public required ICollection<Book> Books { get; set; } = new List<Book>();
        public AuthorDetail? AuthorDetail { get; set; }
        public string Slug { get; set; } = string.Empty;
        public void SetSlug()
        {
            Slug = GenerateSlug(Name);
        }
        private string GenerateSlug(string name)
        {
            return Regex.Replace(name, "[^0-9A-Za-z _-]", string.Empty)
                .ToLower().Replace(" ", "-");
        }
    }
}
