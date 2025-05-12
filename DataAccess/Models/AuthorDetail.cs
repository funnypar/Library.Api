using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Models
{
    public class AuthorDetail
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }

        public  DateTime CreatedOn { get; init; } =  DateTime.Now;
        public Guid? AuthorId { get; set; }
        public Author? Author { get; set; }
        public int? Age { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Website { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();


    }
}
