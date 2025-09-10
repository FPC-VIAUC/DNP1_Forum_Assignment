using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class ViewPostView{
    private IPostRepository postRepository;
    private ICommentRepository commentRepository;

    public ViewPostView(IPostRepository postRepository, ICommentRepository commentRepository){
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public async Task ViewPostAsync(){
        Console.Write("Type post ID: ");
        int postId = await MyCliUtils.ReadIntAsync();
        Post post = await postRepository.GetSingleAsync(postId);
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