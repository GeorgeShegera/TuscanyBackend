using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace Tuscany.Models;

public partial class User : IdentityUser
{
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Avatar { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
}
