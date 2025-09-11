using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class CreateUserView : ConsoleView{
    private IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

    public override async Task ShowViewAsync(){
        string username = await ReadStringAsync("Type username: ");
        string password = await ReadStringAsync("Type password: ");
        
        await userRepository.AddAsync(new User(username, password));
    }
}