using RepositoryContracts;
using Entities;

namespace BusinessLogic;

public class PostsService{
    private readonly IPostRepository postRepository;
    private readonly IUserRepository userRepository;

    public PostsService(IPostRepository postRepository, IUserRepository userRepository){
        this.postRepository = postRepository;
        this.userRepository = userRepository;
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
        Post currentPost = await postRepository.GetSingleAsync(post.Id);
        if (post.Title.Length == 0) throw new ArgumentException("The title of a post cannot be empty.");
        //if (currentPost.UserId != post.UserId) throw new ArgumentException("You cannot update the user who posted the post.");
        post.UserId = currentPost.UserId;
        await postRepository.UpdateAsync(post);
    }

    public async Task DeleteAsync(int id){
        await postRepository.DeleteAsync(id);
    }
}