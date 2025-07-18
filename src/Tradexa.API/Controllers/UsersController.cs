using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tradexa.Application.DTOs;
using Tradexa.Application.Interfaces;
namespace Tradexa.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpPost]
    //public async Task<IActionResult> Create(UserDto dto) => Ok(await _service.CreateAsync(dto));

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, UserUpdateDto dto) => Ok(await _service.UpdateAsync(id, dto));

    [HttpPost("reset-password/{id}")]
    public async Task<IActionResult> ResetPassword(string id, ResetPasswordDto dto) => Ok(await _service.ResetPasswordAsync(id, dto.NewPassword));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id) => Ok(await _service.DeleteAsync(id));
}
