using Entities;
using RepositoryContracts;

namespace InMemoryRepositories;

public class UserInMemoryRepository : IUserRepository{
    private List<User> users;

    public Task<User> AddAsync(User user){
        user.Id = users.Any()
            ? users.Max(u => u.Id) + 1
            : 1;
        users.Add(user);
        return Task.FromResult(user);
    }

    public Task UpdateAsync(User user){
        User existingUser = getUser(user.Id);
        
        users.Remove(existingUser);
        users.Add(user);
        
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id){
        User userToRemove = getUser(id);

        users.Remove(userToRemove);
        return Task.CompletedTask;
    }

    public Task<User> GetSingleAsync(int id){
        User user = getUser(id);
        
        return Task.FromResult(user);
    }

    public IQueryable<User> GetMany(){
        return users.AsQueryable();
    }

    private User getUser(int id){
        User? user = users.SingleOrDefault(u => u.Id == id);
        if (user is null){
            throw new InvalidOperationException(
                $"User with ID '{id}' not found");
        }
        return user;
    }

    public void AddDummyData(){
        AddAsync(new User("FPC", "FPCpass"));
        AddAsync(new User("VIA", "VIApass"));
        AddAsync(new User("admin", "password"));
    }
}