using System.ComponentModel.DataAnnotations;

namespace Authentication.Database.Models;

public class Permission
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Description { get; set; }
    
    [Required]
    [MaxLength(200)]
    public string Endpoint { get; set; }
    
    [Required]
    [MaxLength(10)]
    public string Method { get; set; }
    
    public virtual ICollection<UserPermission> UserPermissions { get; set; }
} 