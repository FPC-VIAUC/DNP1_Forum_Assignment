using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class CreateUserView{
    private IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(){
        Console.Write("Type username: ");
        string username = await MyCliUtils.ReadStringAsync();
        Console.Write("Type password: ");
        string password = await MyCliUtils.ReadStringAsync();
        
        return await userRepository.AddAsync(new User(username, password));
    }
}