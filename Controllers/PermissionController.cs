using Microsoft.AspNetCore.Mvc;
using Authentication.Database;
using Authentication.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PermissionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public PermissionController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var permissions = await _context.Permissions.ToListAsync();
        return Ok(permissions);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission == null)
            return NotFound();

        return Ok(permission);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionRequest request)
    {
        var permission = new Permission
        {
            Name = request.Name,
            Description = request.Description,
            Endpoint = request.Endpoint,
            Method = request.Method
        };

        _context.Permissions.Add(permission);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = permission.Id }, permission);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] UpdatePermissionRequest request)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission == null)
            return NotFound();

        permission.Name = request.Name;
        permission.Description = request.Description;
        permission.Endpoint = request.Endpoint;
        permission.Method = request.Method;

        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var permission = await _context.Permissions.FindAsync(id);
        if (permission == null)
            return NotFound();

        _context.Permissions.Remove(permission);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}

public class CreatePermissionRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Endpoint { get; set; }
    public string Method { get; set; }
}

public class UpdatePermissionRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string Endpoint { get; set; }
    public string Method { get; set; }
} 