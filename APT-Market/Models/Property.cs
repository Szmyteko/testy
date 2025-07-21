using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace APT_Market.Models;

public class Property : IValidatableObject
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Musisz podać adres lokalu.")]
    public string? Address { get; set; }
    [Required(ErrorMessage = "Należy podać cenę najmu.")]
    public int RentPrice { get; set; }
    [Required(ErrorMessage = "Należy podać metraż lokalu.")]
    public int Size { get; set; }
    public RentalAgreement? RentalAgreement { get; set; }
    public List<Payment> Payments { get; set; } = new List<Payment>();
    public string? Description { get; set; }
    public bool IsAvailable { get; set; } = true;
    public List<MaintenanceRequest>? ServiceRequests { get; set; } = new List<MaintenanceRequest>();
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (RentPrice < 0)
        {
            yield return new ValidationResult("Cena najmu nie może być ujemna.", new[] {"Cena najmu"});
        }

        if (Size < 0)
        {
            yield return new ValidationResult("Metraż musi być większy od zera", new[] { "Niewłaściwy metraż" });
        }
    }
}