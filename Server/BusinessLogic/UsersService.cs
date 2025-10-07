using RepositoryContracts;
using Entities;

namespace BusinessLogic;

public class UsersService{
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;
    private readonly ICommentRepository commentRepository;

    public UsersService(IUserRepository userRepository, IPostRepository postRepository, ICommentRepository commentRepository){
        this.userRepository = userRepository;
        this.postRepository = postRepository;
        this.commentRepository = commentRepository;
    }

    public IQueryable<User> GetMany(){
        return userRepository.GetMany();
    }

    public async Task<User> GetSingleAsync(int id){
        return await userRepository.GetSingleAsync(id);
    }

    public async Task<User> AddAsync(User user){
        if (userRepository.GetMany().Any(u => u.Username == user.Username))
            throw new InvalidOperationException($"User with username '{user.Username}' already exists.");
        return await userRepository.AddAsync(user);
    }

    public async Task UpdateAsync(User user){
        if (userRepository.GetMany().Any(u => u.Username == user.Username && u.Id != user.Id))
            throw new InvalidOperationException($"User with username '{user.Username}' already exists.");
        await userRepository.UpdateAsync(user);
    }

    public async Task DeleteAsync(int id){
        await userRepository.DeleteAsync(id);
        postRepository.GetMany().Where(p => p.UserId == id).ToList().
            ForEach(p => postRepository.DeleteAsync(p.Id));
        commentRepository.GetMany().Where(c => c.UserId == id).ToList().
            ForEach(c => commentRepository.DeleteAsync(c.Id));
    }
}