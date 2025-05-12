using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataAccess.Models
{
    public class Publisher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
        public required string Name { get; set; }
        public string Slug { get; set; } = string.Empty;
        public ICollection<Book> Books { get; set; } = new List<Book>();

        public ICollection<Image> Images { get; set; } = new List<Image>();

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
