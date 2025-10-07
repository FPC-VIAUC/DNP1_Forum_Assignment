using RepositoryContracts;
using Entities;

namespace BusinessLogic;

public class PostsService{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;
    private readonly ICommentRepository commentRepository;

    public PostsService(IPostRepository postRepository, IUserRepository userRepository, ICommentRepository commentRepository){
        this.postRepository = postRepository;
        this.userRepository = userRepository;
        this.commentRepository = commentRepository;
    }

    public IQueryable<Post> GetMany(){
        return postRepository.GetMany();
    }

    public async Task<Post> GetSingleAsync(int id){
        return await postRepository.GetSingleAsync(id);
    }

    public async Task<Post> AddAsync(Post post){
        if (post.Title.Length == 0) throw new ArgumentException("The title of a post cannot be empty.");
        await userRepository.GetSingleAsync(post.UserId); // Throws an exception if the user doesn't exist
        return await postRepository.AddAsync(post);
    }

    public async Task UpdateAsync(Post post){
        if (post.Title.Length == 0) throw new ArgumentException("The title of a post cannot be empty.");
        Post currentPost = await postRepository.GetSingleAsync(post.Id);
        post.UserId = currentPost.UserId; //if (currentPost.UserId != post.UserId) throw new ArgumentException("You cannot update the user who posted the post.");
        await postRepository.UpdateAsync(post);
    }

    public async Task DeleteAsync(int id){
        await postRepository.DeleteAsync(id);
        commentRepository.GetMany().Where(c => c.PostId == id).ToList().
            ForEach(c => commentRepository.DeleteAsync(c.Id));
    }
}