using ApiContracts;

namespace BlazorApp.Services;

public interface IUserService{
    IQueryable<UserDTO> GetManyUser();
    Task<UserDTO> GetSingleUserAsync(int id);
    Task<UserDTO> AddUserAsync(CreateUserDTO request);
    Task UpdateUserAsync(int id, CreateUserDTO request);
    Task DeleteUserAsync(int id);
}