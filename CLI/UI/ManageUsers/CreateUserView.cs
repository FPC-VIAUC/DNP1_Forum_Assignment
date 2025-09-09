using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class CreateUserView{
    private IUserRepository userRepository;

    public CreateUserView(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

    public async Task<User> CreateUserAsync(){
        string? username = null;
        while (username == null){
            Console.Write("Type username: ");
            username = Console.ReadLine();
            if (username == null){
                Console.WriteLine("Try again.");
            }
        }
        string? password = null;
        while (password == null){
            Console.Write("Type password: ");
            password = Console.ReadLine();
            if (password == null){
                Console.WriteLine("Try again.");
            }
        }
        return await userRepository.AddAsync(new User(username, password));
    }
}