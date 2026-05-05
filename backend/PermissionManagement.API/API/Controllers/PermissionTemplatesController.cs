using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.API.Application.DTOs.Template;
using PermissionManagement.API.Application.Interfaces;

namespace PermissionManagement.API.API.Controllers;

[ApiController]
[Route("api/permission-templates")]
[Authorize]
public class PermissionTemplatesController : ControllerBase
{
    private readonly IPermissionTemplateService _service;

    public PermissionTemplatesController(IPermissionTemplateService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id) => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTemplateRequest request)
    {
        var result = await _service.CreateAsync(request);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] CreateTemplateRequest request)
        => Ok(await _service.UpdateAsync(id, request));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
