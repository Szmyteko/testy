using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using ErrorEventArgs = Microsoft.AspNetCore.Components.Web.ErrorEventArgs;

namespace APT_Market.Models;

public class Tenant
{
    public string Id { get; set; }
    [Required(ErrorMessage = "Imię i nazwisko są wymagane.")]
    public string FullName { get; set; }
    [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
    [RegularExpression(@"^\+?[0-9]{9,11}$", ErrorMessage = "Nieprawidłowy numer telefonu.")]
    public string PhoneNumber { get; set; }
    [Required(ErrorMessage = "Adres email jest wymagany.")]
    [RegularExpression(@"^[^@\s]+@[^@\s].[^@\s]+$", ErrorMessage = "Nieprawidłowy format adresu email.")]
    public string Email { get; set; }
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }
}