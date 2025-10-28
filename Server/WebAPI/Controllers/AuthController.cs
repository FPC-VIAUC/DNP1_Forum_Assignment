using ApiContracts;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase{
    private UsersService usersService;

    public AuthController(UsersService usersService){
        this.usersService = usersService;
    }

    [HttpPost("login", Name = "login")]
    public IActionResult Login([FromBody] LoginRequestDTO loginRequestDto){
        try{
            IQueryable<Entities.User> users = usersService.GetMany();
            Entities.User? user = users.FirstOrDefault(u => u.Username == loginRequestDto.Username);
            if (user == null || user.Password != loginRequestDto.Password) return Unauthorized();
            return Ok(new UserDTO(){Id = user.Id, Username = user.Username});
        } catch (Exception e){
            return Unauthorized();
        }
    }
}