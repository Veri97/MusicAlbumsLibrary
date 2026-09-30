using Microsoft.AspNetCore.Mvc;
using MusicAlbumsLibrary.Api.ApiModelContracts.User;
using MusicAlbumsLibrary.Application.Abstractions.Features.Users;
using MusicAlbumsLibrary.Application.Features.Users.DTOs;

namespace MusicAlbumsLibrary.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult<CreatedUserDto>> Create([FromBody] CreateUserRequest request, CancellationToken cancellationToken)
    {
        var user = await _userService.CreateUserAsync(request.Name, cancellationToken);

        // We can also return Http 201 Created with the location of the new resource, but for simplicity, we return 200 OK.
        return Ok(user);
    }
}
