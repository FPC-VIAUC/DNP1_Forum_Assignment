using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class CreatePostView : ConsoleView{
    private IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository){
        this.postRepository = postRepository;
    }

    public override async Task ShowViewAsync(){
        string title = await ReadStringAsync("Type title: ");
        string body = await ReadStringAsync("Type body: ");
        int id = await ReadIntAsync("Type user ID: ");
        
        await postRepository.AddAsync(new Post(title, body, id));
    }
}