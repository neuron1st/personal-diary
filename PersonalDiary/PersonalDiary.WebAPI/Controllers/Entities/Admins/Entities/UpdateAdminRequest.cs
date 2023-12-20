using System.ComponentModel.DataAnnotations;

namespace PersonalDiary.WebAPI.Controllers.Entities.Admins.Entities;

public class UpdateAdminRequest : IValidatableObject
{
    [Required]
    [MinLength(2)]
    public string Login { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        return new List<ValidationResult>();
    }
}
