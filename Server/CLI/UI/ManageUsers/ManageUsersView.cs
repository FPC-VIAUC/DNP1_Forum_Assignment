using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUsersView{
    private CreateUserView createUserView;
    private ListUsersView listUsersView;

    public ManageUsersView(IUserRepository userRepository){
        createUserView = new CreateUserView(userRepository);
        listUsersView = new ListUsersView(userRepository);
    }

    public async Task ManageUsersAsync(){
        int choice = -1;
        while (choice != 0){
            choice = await MyCliUtils.GetChoiceAsync([
                "Create a user", 
                "List all users"
            ]);

            Console.WriteLine();
            switch (choice){
                case 1:
                    await createUserView.CreateUserAsync();
                    break;
                case 2:
                    listUsersView.ListUsers();
                    break;
            }
        }
    }
}