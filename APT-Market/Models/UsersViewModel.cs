using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace APT_Market.Models;

public class UsersViewModel
{
    public int Id { get; set; }
    public string UserName { get; set; }
    [Required]
    [StringLength(100, ErrorMessage = "Hasło musi mieć co najmniej {2} i maksymalnie {1} znaków.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Hasło")]
    public string Password { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    public string PhoneNumber { get; set; }
    public IdentityUser? User { get; set; }
    public string SelectedRole { get; set; }
}