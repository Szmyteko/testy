namespace APT_Market.Models;

public class UsersViewModel
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    public string PhoneNumber { get; set; }
}