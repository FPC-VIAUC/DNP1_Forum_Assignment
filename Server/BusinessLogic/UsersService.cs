using RepositoryContracts;
using Entities;

namespace BusinessLogic;

public class UsersService{
    private readonly IUserRepository userRepository;

    public UsersService(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

    public IQueryable<User> GetMany(){
        return userRepository.GetMany();
    }

    public async Task<User> GetSingleAsync(int id){
        return await userRepository.GetSingleAsync(id);
    }

    public async Task<User> AddAsync(User user){
        if (userRepository.GetMany().Any(u => u.Username == user.Username))
            throw new InvalidOperationException($"User with username '{user.Username}' already exists.");
        return await userRepository.AddAsync(user);
    }

    public async Task UpdateAsync(User user){
        if (userRepository.GetMany().Any(u => u.Username == user.Username && u.Id != user.Id))
            throw new InvalidOperationException($"User with username '{user.Username}' already exists.");
        await userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id){
        await userRepository.DeleteAsync(id);
    }
}