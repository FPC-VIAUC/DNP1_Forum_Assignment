using RepositoryContracts;
using Entities;

namespace CLI.UI.ManageComments;

public class ManageCommentsView{
    private CreateCommentView createCommentView;

    public ManageCommentsView(ICommentRepository commentRepository){
        createCommentView = new CreateCommentView(commentRepository);
    }

    public async Task ManageCommentsAsync(){
        int choice = -1;
        while (choice != 0){
            choice = await MyCliUtils.GetChoiceAsync([
                "Create a comment"
            ]);

            Console.WriteLine();
            switch (choice){
                case 1:
                    await createCommentView.CreateCommentAsync();
                    break;
            }
        }
    }
}