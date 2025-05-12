using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace DataAccess.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; init; }
        public DateTime CreatedOn { get; init; } = DateTime.Now;
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string Slug { get; set; } = string.Empty;

        public Image? Image { get; set; }

        public void SetSlug()
        {
            Slug = GenerateSlug(UserName);
        }
        private string GenerateSlug(string userName)
        {
            return Regex.Replace(userName, "[^0-9A-Za-z _-]", string.Empty)
                .ToLower().Replace(" ", "-");
        }
    }
}
