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
        List<int?> choices = [0, 1, 2];
        
        int? choice = null;
        while (!choices.Contains(choice)){
            Console.WriteLine("0) Exit");
            Console.WriteLine("1) Create a user");
            Console.WriteLine("2) List all users");
            Console.Write("Choice: ");
            
            string? input = Console.ReadLine();
            if (input != null && int.TryParse(input, out int num)){
                choice = num;
                if (!choices.Contains(choice)){
                    Console.WriteLine("Not a valid choice");
                }
            } else{
                Console.WriteLine("Try again");
            }
        }

        switch (choice){
            case 0:
                return;
            case 1:
                await createUserView.CreateUserAsync();
                break;
            case 2:
                listUserView.ListUsers();
                break;
        }
    }
}