using RepositoryContracts;

namespace BusinessLogic;
using Entities;

public class CommentsService{
    private readonly ICommentRepository commentRepository;
    private readonly IUserRepository userRepository;
    private readonly IPostRepository postRepository;

    public CommentsService(ICommentRepository commentRepository, IUserRepository userRepository, IPostRepository postRepository){
        this.commentRepository = commentRepository;
        this.userRepository = userRepository;
        this.postRepository = postRepository;
    }

    public IQueryable<Comment> GetMany(){
        return commentRepository.GetMany();
    }

    public async Task<Comment> GetSingleAsync(int id){
        return await commentRepository.GetSingleAsync(id);
    }
    
    public async Task<Comment> AddAsync(Comment comment){
        // Throws exception if ID does not exist.
        userRepository.GetSingleAsync(comment.UserId); 
        postRepository.GetSingleAsync(comment.PostId);

        if (comment.Body.Length == 0) throw new ArgumentException("Body of a comment cannot be empty.");
        
        return await commentRepository.AddAsync(comment);
    }

    public async Task UpdateAsync(Comment comment){
        if (comment.Body.Length == 0) throw new ArgumentException("Body of a comment cannot be empty.");
        Comment currentComment = await commentRepository.GetSingleAsync(comment.Id);
        comment.UserId = currentComment.UserId;
        comment.PostId = currentComment.PostId;
        await commentRepository.UpdateAsync(comment);
    }

    public async Task DeleteAsync(int id){
        await commentRepository.DeleteAsync(id);
    }
}