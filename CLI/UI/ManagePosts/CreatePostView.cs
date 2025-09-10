using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class CreatePostView{
    private IPostRepository postRepository;

    public CreatePostView(IPostRepository postRepository){
        this.postRepository = postRepository;
    }

    public async Task<Post> CreatePostAsync(){
        string title = await MyCliUtils.ReadStringAsync("Type title: ");
        string body = await MyCliUtils.ReadStringAsync("Type body: ");
        int id = await MyCliUtils.ReadIntAsync("Type user ID: ");
        
        return await postRepository.AddAsync(new Post(title, body, id));
    }
}