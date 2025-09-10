using RepositoryContracts;

namespace CLI.UI.ManageUsers;

public class ManageUserView{
    private CreateUserView createUserView;
    private ListUserView listUserView;

    public ManageUserView(IUserRepository userRepository){
        createUserView = new CreateUserView(userRepository);
        listUserView = new ListUserView(userRepository);
    }

    public async Task ManageUsersAsync(){
        int choice = -1;
        while (choice != 0){
            choice = await MyCliUtils.GetChoiceAsync([
                "Create a user", 
                "List all users"
            ]);

            switch (choice){
                case 1:
                    await createUserView.CreateUserAsync();
                    break;
                case 2:
                    listUserView.ListUsers();
                    break;
            }
        }
    }
}