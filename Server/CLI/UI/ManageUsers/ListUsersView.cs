using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageUsers;

public class ListUsersView : ConsoleView{
    private IUserRepository userRepository;

    public ListUsersView(IUserRepository userRepository){
        this.userRepository = userRepository;
    }

    public override async Task ShowViewAsync(){
        IQueryable<User> users = userRepository.GetMany();
        Console.WriteLine($"{users.Count()} users:");
        foreach (User user in userRepository.GetMany()){
            Console.WriteLine($"({user.Id}) {user.Username}");
        }
        Console.ReadLine();
    }
}