using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.WebAPI.Controllers.Entities.Users.Entities;

public class CreateUserRequest : IValidatableObject
{
    [Required]
    [MinLength(2)]
    public string UserName { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return new List<ValidationResult>();
    }
}
