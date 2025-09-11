using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView : ConsoleMenu{
    private CreatePostView createPostView;
    private ViewPostView viewPostView;
    private ListPostsView listPostsView;

    public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository){
        createPostView = new CreatePostView(postRepository);
        viewPostView = new ViewPostView(postRepository, commentRepository);
        listPostsView = new ListPostsView(postRepository);
        
        AddMenuItems([
            new ConsoleMenuItem("Create a post", createPostView),
            new ConsoleMenuItem("View a post", viewPostView),
            new ConsoleMenuItem("List all posts", listPostsView)
        ]);
    }
}