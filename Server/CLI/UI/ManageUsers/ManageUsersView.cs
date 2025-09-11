using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView : ConsoleMenu{
    private CreateUserView createUserView;
    private ListUsersView listUsersView;

    public ManageUsersView(IUserRepository userRepository){
        createUserView = new CreateUserView(userRepository);
        listUsersView = new ListUsersView(userRepository);
        
        AddMenuItems([
            new ConsoleMenuItem("Create a user", createUserView),
            new ConsoleMenuItem("List all users", listUsersView)
        ]);
    }
}