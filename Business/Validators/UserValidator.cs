using DataAccess.Models;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using FluentValidation;
using System.Text.RegularExpressions;

namespace Business.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private readonly IUserRepository _userRepository;
        public UserValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            RuleFor(user => user.Email).NotEmpty();
            RuleFor(user => user.Password).Must(ValidatePassword).NotEmpty().WithMessage("Invalid password format. The password should have 8 characters ");
            RuleFor(user => user.Email).Must(ValidateEmail).WithMessage("Invalid email format. The email should be in the format: example@domain.com");
            RuleFor(user => user.Slug).NotEmpty().MustAsync(ValidateSlug);
        }
        private bool ValidatePassword(string password)
        {
            return password.Length == 8 ?  true : false;
        }
        private bool ValidateEmail(string email)
        {
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern);
        }
        private async Task<bool> ValidateSlug(User user, string slug, CancellationToken token = default)
        {
            var existingUser = await _userRepository.GetUserBySlugAsync(slug, token);
            if (existingUser != null)
            {
                return existingUser.Id == user.Id;
            }
            return existingUser == null;
        }
    }
}
