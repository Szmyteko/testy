using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace APT_Market.Models;

public class MaintenanceRequest
{
    public int Id { get; set; }
    public int PropertyId { get; set; }
    public string? Description { get; set; }
    public SelectList StatusList { get; set; }
    public string? UserId { get; set; }
    public IdentityUser? User { get; set; }
}