using ApiContracts;
using BusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Entities;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase{
    private readonly UsersService usersService;

    public UsersController(UsersService usersService){
        this.usersService = usersService;
    }

    [HttpGet(Name = "GetUsers")]
    public ActionResult<IEnumerable<UserDTO>> GetUsers([FromQuery] string? filter){
        IQueryable<User> users = usersService.GetMany();
        if (filter != null) users = users.Where(u => u.Username.ToLower().Contains(filter.ToLower()));
        IQueryable<UserDTO> userDTOs = users.Select(u => CreateUserDTOFromUser(u));
        return Ok(userDTOs);
    }

    [HttpGet("{id:int}", Name = "GetUser")]
    public async Task<IActionResult> GetUser([FromRoute] int id){
        User user = await usersService.GetSingleAsync(id);
        return Ok(CreateUserDTOFromUser(user));
    }

    [HttpPost(Name = "CreateUser")]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO newUser){
        User user = new User(newUser.Username, newUser.Password);
        User createdUser = await usersService.AddAsync(user);

        return Created(
            Url.Link("GetUser", new{ id = createdUser.Id }),
            CreateUserDTOFromUser(createdUser)
        );
    }

    [HttpPut("{id:int}", Name = "UpdateUser")]
    public async Task<IActionResult> UpdateUser([FromRoute] int id, [FromBody] CreateUserDTO updatedUser){
        User user = new User(updatedUser.Username, updatedUser.Password){ Id = id };
        await usersService.UpdateAsync(user);

        return Ok(CreateUserDTOFromUser(user));
    }

    [HttpDelete("{id:int}", Name = "DeleteUser")]
    public async Task<IActionResult> DeleteUser([FromRoute] int id){
        await usersService.DeleteAsync(id);
        return NoContent();
    }

    private UserDTO CreateUserDTOFromUser(User user){
        return new UserDTO(){ Id = user.Id, Username = user.Username };
    }
}