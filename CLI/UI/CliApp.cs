using CLI.UI.ManageUsers;
using RepositoryContracts;

namespace CLI.UI;

public class CliApp{
    private IUserRepository userRepository;
    private IPostRepository postRepository;
    private ICommentRepository commentRepository;

    public CliApp(IUserRepository userRepository,
        IPostRepository postRepository, ICommentRepository commentRepository){
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }
    
    public async Task StartAsync(){
        ManageUserView manageUserView = new ManageUserView(userRepository);
        await manageUserView.ManageUsersAsync();
    }
}