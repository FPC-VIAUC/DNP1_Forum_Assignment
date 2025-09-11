using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class CreateCommentView : ConsoleView{
    private ICommentRepository commentRepository;

    public CreateCommentView(ICommentRepository commentRepository){
        this.commentRepository = commentRepository;
    }

    public override async Task ShowViewAsync(){
        string commentBody = await ReadStringAsync("Type comment: ");
        int postId = await ReadIntAsync("Type post ID: ");
        int userId = await ReadIntAsync("Type user ID: ");
        
        // TODO: Check if user and post ID exists. I assume this is done with "foreign keys" later, I feel as though it should not be handled in UI...
        
        await commentRepository.AddAsync(new Comment(commentBody, postId, userId));
    }
}