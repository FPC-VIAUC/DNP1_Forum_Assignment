using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class CreateUserView{
    private IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(){
        string username = await MyCliUtils.ReadStringAsync("Type username: ");
        string password = await MyCliUtils.ReadStringAsync("Type password: ");
        
        return await userRepository.AddAsync(new User(username, password));
    }
}