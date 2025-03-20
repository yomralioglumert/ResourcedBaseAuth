using System.ComponentModel.DataAnnotations;

namespace Authentication.Database.Models;

public class UserPermission
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public int PermissionId { get; set; }
    
    public virtual User User { get; set; }
    public virtual Permission Permission { get; set; }
} 