namespace APT_Market.Models;

public class UsersViewModel
{
    public string UserId { get; set; }
    public string Email { get; set; }
    public IList<string> Roles { get; set; }
    public string Password { get; set; }
}