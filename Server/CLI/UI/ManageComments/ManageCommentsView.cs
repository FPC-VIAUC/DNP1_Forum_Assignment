using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class ManageCommentsView : ConsoleMenu{
    private CreateCommentView createCommentView;

    public ManageCommentsView(ICommentRepository commentRepository){
        createCommentView = new CreateCommentView(commentRepository);
        
        AddMenuItems([
            new ConsoleMenuItem("Create a comment", createCommentView)
        ]);
    }
}