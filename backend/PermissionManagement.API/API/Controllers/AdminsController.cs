using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.API.Application.DTOs.Permission;
using PermissionManagement.API.Application.Interfaces;

namespace PermissionManagement.API.API.Controllers;

[ApiController]
[Route("api/admins")]
[Authorize]
public class AdminsController : ControllerBase
{
    private readonly IAdminService _service;

    public AdminsController(IAdminService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

    [HttpGet("{id}/permissions")]
    public async Task<IActionResult> GetPermissions(int id) => Ok(await _service.GetPermissionsAsync(id));

    [HttpPut("{id}/permissions")]
    public async Task<IActionResult> UpdatePermissions(int id, [FromBody] UpdatePermissionsRequest request)
        => Ok(await _service.UpdatePermissionsAsync(id, request));

    [HttpPost("{id}/apply-template/{templateId}")]
    public async Task<IActionResult> ApplyTemplate(int id, int templateId)
        => Ok(await _service.ApplyTemplateAsync(id, templateId));
}
