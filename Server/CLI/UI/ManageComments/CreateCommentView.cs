using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateCommentView{
    private ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository){
        this.commentRepository = commentRepository;
    }

    public async Task<Comment> CreateCommentAsync(){
        string commentBody = await MyCliUtils.ReadStringAsync("Type comment: ");
        int postId = await MyCliUtils.ReadIntAsync("Type post ID: ");
        int userId = await MyCliUtils.ReadIntAsync("Type user ID: ");
        
        // TODO: Check if user and post ID exists. I assume this is done with "foreign keys" later, I feel as though it should not be handled in UI...
        
        return await commentRepository.AddAsync(new Comment(commentBody, postId, userId));
    }
}