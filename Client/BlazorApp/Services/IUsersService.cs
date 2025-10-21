using ApiContracts;

namespace BlazorApp.Services;

public interface IUsersService{
    IQueryable<UserDTO> GetManyUsers();
    Task<UserDTO> GetSingleUserAsync(int id);
    Task<UserDTO> AddUserAsync(CreateUserDTO request);
    Task UpdateUserAsync(int id, CreateUserDTO request);
    Task DeleteUserAsync(int id);
}