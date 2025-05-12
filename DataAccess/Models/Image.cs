using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccess.Models
{
    public class Image
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string ContentType { get; set; }
        public required string Data { get; set; }

        public Guid? BookId { get; set; }
        public Book? Book { get; set; }

        public Guid? PublisherId { get; set; }
        public Publisher? Publisher { get; set; }

        public Guid? AuthorDetailId { get; set; }
        public AuthorDetail? AuthorDetail { get; set; }

        public Guid? UserId { get; set; }
        public User? User { get; set; }
    }
}
