using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace APT_Market.Models;

public class Payment : IValidatableObject
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Proszę wprowadzić ID umowy najmu")]
    public int RentalAgreementId { get; set; }
    public RentalAgreement RentalAgreement { get; set; }
    [Required(ErrorMessage = "Należy wprowadzić kwotę płatności.")]
    public int Amount { get; set; }
    [Required(ErrorMessage = "Musisz wprowadzić datę płatności.")]
    public DateOnly Date { get; set; }
    public string Status { get; set; }
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (Amount < 0)
        {
            yield return new ValidationResult("Kwota musi być dodatnia", new[] {"Kwota ujemna."});
        }
    }
}