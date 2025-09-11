using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class ViewPostView : ConsoleView{
    private IPostRepository postRepository;
    private ICommentRepository commentRepository;

    public ViewPostView(IPostRepository postRepository, ICommentRepository commentRepository){
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public override async Task ShowViewAsync(){
        int postId = await ReadIntAsync("Type post ID: ");
        Post post;
        try{
            post = await postRepository.GetSingleAsync(postId);
        } catch (InvalidOperationException e){
            Console.WriteLine(e.Message);
            Console.ReadLine();
            return;
        }
        List<Comment> comments = commentRepository.GetMany().Where(c => c.PostId == postId).ToList();

        Console.WriteLine();
        Console.WriteLine(post.Title);
        Console.WriteLine(post.Body);
        Console.WriteLine($"{comments.Count()} comments:");
        foreach (Comment comment in comments){
            Console.WriteLine($"({comment.UserId}) {comment.Body}");
        }
        Console.ReadLine();
    }
}