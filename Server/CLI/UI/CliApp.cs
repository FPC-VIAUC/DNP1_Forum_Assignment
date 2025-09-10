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
        
        int choice = -1;
        while (choice != 0){
            choice = await MyCliUtils.GetChoiceAsync([
                "Users", 
                "Posts",
                "Comments"
            ]);

            Console.WriteLine();
            switch (choice){
                case 1:
                    await manageUsersView.ManageUsersAsync();
                    break;
                case 2:
                    await managePostsView.ManagePostsAsync();
                    break;
                case 3:
                    await manageCommentsView.ManageCommentsAsync();
                    break;
            }
        }
    }
}