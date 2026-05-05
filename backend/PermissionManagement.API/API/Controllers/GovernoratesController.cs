using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PermissionManagement.API.Application.Interfaces;

namespace PermissionManagement.API.API.Controllers;

[ApiController]
[Route("api/governorates")]
[Authorize]
public class GovernoratesController : ControllerBase
{
    private readonly IGovernorateService _service;

    public GovernoratesController(IGovernorateService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());
}
