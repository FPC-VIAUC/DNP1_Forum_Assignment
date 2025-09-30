using System.Text.Json;
using CustomExceptions;
using Entities;
using RepositoryContracts;

namespace FileRepositories;

public class UserFileRepository : IUserRepository{
    private readonly string filePath = "users.json";

    public UserFileRepository(){
        if (!File.Exists(filePath)){
            File.WriteAllText(filePath, "[]");
            AddDummyDataAsync();
        }
    }
    
    public async Task<User> AddAsync(User user){
        List<User> users = await getUsersAsync();
        
        user.Id = users.Any()
            ? users.Max(u => u.Id) + 1
            : 1;
        users.Add(user);

        await saveUsersAsync(users);
        return user;
    }

    public async Task UpdateAsync(User user){
        List<User> users = await getUsersAsync();
        
        User existingUser = getUser(users, user.Id);
        users.Remove(existingUser);
        users.Add(user);

        await saveUsersAsync(users);
    }

    public async Task DeleteAsync(int id){
        List<User> users = await getUsersAsync();

        User userToRemove = getUser(users, id);
        users.Remove(userToRemove);

        await saveUsersAsync(users);
    }

    public async Task<User> GetSingleAsync(int id){
        List<User> users = await getUsersAsync();
        User user = getUser(users, id);
        return user;
    }

    public IQueryable<User> GetMany(){
        List<User> users = getUsersAsync().Result;
        return users.AsQueryable();
    }

    private async Task<List<User>> getUsersAsync(){
        string usersJson = await File.ReadAllTextAsync(filePath);
        List<User> users = JsonSerializer.Deserialize<List<User>>(usersJson)!;
        return users;
    }
    
    private async Task saveUsersAsync(List<User> users){
        string usersJson = JsonSerializer.Serialize(users);
        await File.WriteAllTextAsync(filePath, usersJson);
    }

    private User getUser(List<User> users, int id){
        User? user = users.SingleOrDefault(u => u.Id == id);
        if (user is null){
            throw new NotFoundException(
                $"User with ID '{id}' not found");
        }
        return user;
    }

    public async Task AddDummyDataAsync(){
        await AddAsync(new User("FPC", "FPCpass"));
        await AddAsync(new User("VIA", "VIApass"));
        await AddAsync(new User("admin", "password"));
    }
}