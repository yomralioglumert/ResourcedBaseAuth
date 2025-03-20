using Microsoft.AspNetCore.Mvc;
using Authentication.Database;
using Authentication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserPermissionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public UserPermissionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetUserPermissions(Guid userId)
    {
        var userPermissions = await _context.UserPermissions
            .Include(up => up.Permission)
            .Where(up => up.UserId == userId)
            .Select(up => new { up.Permission.Id, up.Permission.Name, up.Permission.Description, up.Permission.Endpoint, up.Permission.Method })
            .ToListAsync();

        return Ok(userPermissions);
    }

    [HttpPost]
    public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionRequest request)
    {
        var userPermission = new UserPermission
        {
            UserId = request.UserId,
            PermissionId = request.PermissionId
        };

        _context.UserPermissions.Add(userPermission);
        await _context.SaveChangesAsync();

        return Ok(userPermission);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> RemovePermission(int id)
    {
        var userPermission = await _context.UserPermissions.FindAsync(id);
        if (userPermission == null)
            return NotFound();

        _context.UserPermissions.Remove(userPermission);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

public class AssignPermissionRequest
{
    public Guid UserId { get; set; }
    public int PermissionId { get; set; }
} 