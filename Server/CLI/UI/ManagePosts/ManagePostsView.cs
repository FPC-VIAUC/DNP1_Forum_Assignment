using RepositoryContracts;

namespace CLI.UI.ManagePosts;

public class ManagePostsView{
    private CreatePostView createPostView;
    private ViewPostView viewPostView;
    private ListPostsView listPostsView;

    public ManagePostsView(IPostRepository postRepository, ICommentRepository commentRepository){
        createPostView = new CreatePostView(postRepository);
        viewPostView = new ViewPostView(postRepository, commentRepository);
        listPostsView = new ListPostsView(postRepository);
    }

    public async Task ManagePostsAsync(){
        int choice = -1;
        while (choice != 0){
            choice = await MyCliUtils.GetChoiceAsync([
                "Create a post", 
                "View a post",
                "List all posts"
            ]);

            Console.WriteLine();
            switch (choice){
                case 1:
                    await createPostView.CreatePostAsync();
                    break;
                case 2:
                    viewPostView.ViewPostAsync();
                    break;
                case 3:
                    listPostsView.ListPosts();
                    break;
            }
        }
    }
}