using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateCommentView{
    private ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository){
        this.commentRepository = commentRepository;
    }

    public async Task<Comment> CreateCommentAsync(){
        Console.Write("Type comment: ");
        string commentBody = await MyCliUtils.ReadStringAsync();
        Console.Write("Type post ID: ");
        int postId = await MyCliUtils.ReadIntAsync();
        Console.Write("Type user ID: ");
        int userId = await MyCliUtils.ReadIntAsync();
        
        // TODO: Check if user and post ID exists. I assume this is done with "foreign keys" later, I feel as though it should not be handled in UI...
        
        return await commentRepository.AddAsync(new Comment(commentBody, postId, userId));
    }
}