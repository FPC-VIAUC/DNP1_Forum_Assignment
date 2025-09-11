using CLI.UI.ManageUsers;
using CLI.UI.ManagePosts;
using CLI.UI.ManageComments;
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
        ManageUsersView manageUsersView = new ManageUsersView(userRepository);
        ManagePostsView managePostsView = new ManagePostsView(postRepository, commentRepository);
        ManageCommentsView manageCommentsView = new ManageCommentsView(commentRepository);

        await new ConsoleMenu([
            new ConsoleMenuItem("Users",manageUsersView),
            new ConsoleMenuItem("Posts",managePostsView),
            new ConsoleMenuItem("Comments",manageCommentsView)
        ]).ShowViewAsync();
    }
}