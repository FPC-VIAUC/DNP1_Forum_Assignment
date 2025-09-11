using RepositoryContracts;
using Entities;

namespace CLI.UI.ManagePosts;

public class ListPostsView : ConsoleView{
    private IPostRepository postRepository;

    public ListPostsView(IPostRepository postRepository){
        this.postRepository = postRepository;
    }

    public override async Task ShowViewAsync(){
        IQueryable<Post> posts = postRepository.GetMany();
        Console.WriteLine($"{posts.Count()} posts:");
        foreach (Post post in postRepository.GetMany()){
            Console.WriteLine($"({post.Id}) {post.Title}");
        }
        Console.ReadLine();
    }
}