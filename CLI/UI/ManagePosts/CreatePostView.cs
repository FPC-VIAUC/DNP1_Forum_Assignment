using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class CreatePostView{
    private IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository){
        this.postRepository = postRepository;
    }

    public async Task<Post> CreatePostAsync(){
        Console.Write("Type title: ");
        string title = await MyCliUtils.ReadStringAsync();
        Console.Write("Type body: ");
        string body = await MyCliUtils.ReadStringAsync();
        Console.Write("Type user ID: ");
        int id = await MyCliUtils.ReadIntAsync();
        
        return await postRepository.AddAsync(new Post(title, body, id));
    }
}